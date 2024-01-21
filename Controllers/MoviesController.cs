using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.Services;

namespace Backend.Controllers
{
    [ApiController]
  [Route("api/[controller]")]
  public class MoviesController : ControllerBase
  {
    private readonly IOmdbService _omdbService;
    private readonly ApplicationDbContext _context;

    public MoviesController(IOmdbService omdbService, ApplicationDbContext context)
    {
        _omdbService = omdbService;
        _context = context;
    }

    /*
        The method below handles the reteireval of last 5 searches
        If record is found, it returns the records array object
        with the list of found item(s)
        if no records, it simple returns empty array object
        if the process fails we output 500 error with a message
    */ 

    [HttpGet("recent-searches")]
    public async Task<IActionResult> GetRecentSearches()
    {
        try
        {
            var recentSearches = await _context.SearchQueries
                .OrderByDescending(sq => sq.Timestamp)
                .Take(5)
                .ToListAsync();

            return Ok(new ApiResponse<List<SearchQuery>>(false, "Success", recentSearches));
        }
        catch (System.Exception ex)
        {
            var errorMessage = $"An error occurred: {ex.Message} | StackTrace: {ex.StackTrace}";
            return StatusCode(500, new ApiResponse<object>(true, errorMessage, null));
        }
    }

    /*
        The method below performs the search operation 
        
        This method simply takes in aither an ID or a 
        Title and makes a service request
        if result is found for any of id or title, 
        it adds the query string to the database and return
        the result

        Before adding the query to the database, 
        it checks to see if the items in database exceed 6
        if it does, it deletes the oldest item and add the newest
        Otherwise it just adds the newest

        All methods use the API standard response object ApiResponse 
    */

    [HttpGet("search")]
    public async Task<IActionResult> SearchMovie(string? title = null, string? id = null)
      {
        var notitleid   = "A title or id must be provided for search.";
        var nomovie     = "Movie not found.";
        if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(id))
        {
            return BadRequest(new ApiResponse<object>(true, notitleid, null));
        }

        try
        {
        Movie movie = string.IsNullOrEmpty(title)
            ? await _omdbService.GetMovieByIdAsync(id)
            : await _omdbService.GetMovieByTitleAsync(title);
        if (movie == null)
        {
            return NotFound(new ApiResponse<object>(true, nomovie, null));
        }
        var searchQuery = new SearchQuery { Query = title ?? id };
        _context.SearchQueries.Add(searchQuery);

        if (_context.SearchQueries.Count() >= 6)
        {
        var oldestSearch = await _context.SearchQueries.OrderBy(sq => sq.Timestamp).FirstAsync();
        _context.SearchQueries.Remove(oldestSearch);
        }
        await _context.SaveChangesAsync();

        return Ok(new ApiResponse<Movie>(false, "Success", movie));
        }
        catch (System.Exception ex)
        {
        var errorMessage = $"An error occurred: {ex.Message} | StackTrace: {ex.StackTrace}";
        return StatusCode(500, new ApiResponse<object>(true, errorMessage, null));
        }
      }
  }
}
