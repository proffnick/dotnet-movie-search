using Backend.Models;
namespace Backend.Services
{
    public interface IOmdbService
    {
        Task<Movie> GetMovieByTitleAsync(string title);
        Task<Movie> GetMovieByIdAsync(string id);
    }
}
