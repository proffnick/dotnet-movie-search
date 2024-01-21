using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Backend.Models;

namespace Backend.Services
{
    public class OmdbService: IOmdbService
  {
      private readonly HttpClient _httpClient;
      private readonly string _apiKey; 
      public OmdbService(HttpClient httpClient, IOptions<OmdbApiSettings> settings)
      {
          _httpClient = httpClient;
          _httpClient.BaseAddress = new Uri(settings.Value.BaseUrl);
          _apiKey = settings.Value.ApiKey;
      }

      /*
        Declare method to get movie by title if title is valid, from omdbapi
      */
      public async Task<Movie> GetMovieByTitleAsync(string title)
      {
        var response = await _httpClient.GetAsync($"?t={title}&apikey={_apiKey}");
        if (response.IsSuccessStatusCode)
        {
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Movie>(content);
        }
        return null;
      }
      
      /*
        Declare method for getting movie by ID from omdbapi
      */ 
      public async Task<Movie> GetMovieByIdAsync(string id)
      {
        var response = await _httpClient.GetAsync($"?i={id}&apikey={_apiKey}");
        if (response.IsSuccessStatusCode)
        {
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Movie>(content);
        }
        return null;
      }
  }
}