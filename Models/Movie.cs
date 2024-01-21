
namespace Backend.Models
{
  public class Movie
  {
      public required string Title { get; set; }
      public required string Year { get; set; }
      public string? Rated { get; set; }
      public required string Released { get; set; }
      public string? Runtime { get; set; }
      public required string Genre { get; set; }
      public required string Director { get; set; }
      public string? Writer { get; set; }
      public required string Actors { get; set; }
      public string? Plot { get; set; }
      public required string Language { get; set; }
      public string? Country { get; set; }
      public string? Awards { get; set; }
      public string? Poster { get; set; }
      public List<Rating>? Ratings { get; set; }
      public string? Metascore { get; set; }
      public string? ImdbRating { get; set; }
      public string? ImdbVotes { get; set; }
      public string? ImdbID { get; set; }
      public required string Type { get; set; }
      public string? DVD { get; set; }
      public string? BoxOffice { get; set; }
      public required string Production { get; set; }
      public string? Website { get; set; }
      public required string Response { get; set; }
  }

  public class Rating
  {
      public string? Source { get; set; }
      public string? Value { get; set; }
  }

  // standard query structure
  public class SearchQuery
    {
      public int Id { get; set; }
      public required string Query { get; set; }
      public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

  // standard API response
  public class ApiResponse<T>
  {
      public bool Error { get; set; }
      public string Message { get; set; }
      public T Data { get; set; }

      public ApiResponse(bool error, string message, T data)
      {
        Error = error;
        Message = message;
        Data = data;
      }
  }

}