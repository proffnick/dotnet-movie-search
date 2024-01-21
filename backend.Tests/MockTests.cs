using Xunit;
using Moq;
using Backend.Controllers;
using Backend.Services;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


/*
    In this test file, we are testing for 5 cases 
    for the purpose of the assessment only
    test cases could run into hundreds of lines
    please note these cases can be tested individually

    For the purpose of demostration and 
    avoiding disruptions of the main databse, 
    we are using inmemory database to simulate
    the actions that require database mocking

    These cases are 
    1. if a valid title is provided, try to retrieve a movie
    2. if a valid ID is provided, try to retrieve a movie
    3. if no records in database, return empty list
    4. if records in database are up to 5 items return them
    5  if valid ID is provided, fetch movie full details
    the cases are named after what they are expected to do.
*/


public class MockTests
{

  // First case
  
    [Fact]
    public async Task SearchMovie_WithValidTitle_ReturnsMovie()
    {
      var mockService = new Mock<IOmdbService>();
      mockService.Setup(service => service.GetMovieByTitleAsync(It.IsAny<string>()))
          .ReturnsAsync(new Movie
          {
              Title = "Sample",
              Year = "2021",
              Released = "1989",
              Genre = "Blues",
              Director = "Unknown",
              Actors = "",
              Language = "",
              Type = "",
              Production = "",
              Response = ""
          });
      var options = new DbContextOptionsBuilder<ApplicationDbContext>()
          .UseInMemoryDatabase(databaseName: "TestDbForTitle")
          .Options;
      using var context = new ApplicationDbContext(options);
      var controller = new MoviesController(mockService.Object, context);
      var result = await controller.SearchMovie("Rush Hour", null);
      Assert.IsType<OkObjectResult>(result);
    }

    // second case

    [Fact]
    public async Task SearchMovie_WithValidId_ReturnsMovie()
    {
        var mockService = new Mock<IOmdbService>();
        mockService.Setup(service => service.GetMovieByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(new Movie
            {
                Title = "Sample",
                Year = "2021",
                Released = "1989",
                Genre = "Blues",
                Director = "Unknown",
                Actors = "",
                Language = "",
                Type = "",
                Production = "",
                Response = ""
            });

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDbForId")
            .Options;

        using var context = new ApplicationDbContext(options);
        var controller = new MoviesController(mockService.Object, context);
        var result = await controller.SearchMovie(null, "tt0120812");
        Assert.IsType<OkObjectResult>(result);
    }

    // third case

    [Fact]
    public async Task GetRecentSearches_WhenNoData_ReturnsEmptyList()
    {
        var mockService = new Mock<IOmdbService>();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDbNoData")
            .Options;

        using var context = new ApplicationDbContext(options);
        var controller = new MoviesController(mockService.Object, context);


        var result = await controller.GetRecentSearches();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var apiResponse = Assert.IsType<ApiResponse<List<SearchQuery>>>(okResult.Value);
        Assert.False(apiResponse.Error);
        Assert.Equal("Success", apiResponse.Message);
        Assert.NotNull(apiResponse.Data);
        Assert.Empty(apiResponse.Data);
    }

    // forth case

    [Fact]
    public async Task GetRecentSearches_WhenFiveItems_ReturnsFiveItems()
    {
        var mockService = new Mock<IOmdbService>();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDbForFiveItems")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            for (int i = 0; i < 5; i++)
            {
                context.SearchQueries.Add(new SearchQuery { Query = $"Query{i}", Timestamp = DateTime.Now });
            }
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            var controller = new MoviesController(mockService.Object, context);

            var result = await controller.GetRecentSearches();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<List<SearchQuery>>>(okResult.Value);
            Assert.False(apiResponse.Error);
            Assert.Equal("Success", apiResponse.Message);
            Assert.NotNull(apiResponse.Data);
            Assert.Equal(5, apiResponse.Data.Count);
        }
    }

    // fifth case

    [Fact]
    public async Task SearchMovie_WithValidId_ReturnsMovieDetails()
    {
        var mockService = new Mock<IOmdbService>();
        mockService.Setup(service => service.GetMovieByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(new Movie
            {
                Title = "Sample",
                Year = "2021",
                Released = "1989",
                Genre = "Blues",
                Director = "Unknown",
                Actors = "",
                Language = "",
                Type = "",
                Production = "",
                Response = ""
            });

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDbForMovieDetails")
            .Options;

        using var context = new ApplicationDbContext(options);
        var controller = new MoviesController(mockService.Object, context);
        var result = await controller.SearchMovie(null, "tt0120812");
        Assert.IsType<OkObjectResult>(result);
    }
}
