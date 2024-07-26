using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using MovieApp.Settings;
using MovieApp.Models;

namespace MovieApp.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSetting _apiSetting;
        private readonly ILogger<MovieService> _logger;

        public MovieService(HttpClient httpClient, IOptions<ApiSetting> apiSettings, ILogger<MovieService> logger)
        {
            _httpClient = httpClient;
            _apiSetting = apiSettings.Value;
            _logger = logger;
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            _logger.LogInformation("Buscando filmes da API TMDb.");
            var response = await _httpClient.GetAsync($"{_apiSetting.BaseUrl}/movie/popular?api_key={_apiSetting.ApiKey}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Erro ao buscar filmes: {StatusCode}", response.StatusCode);
                throw new HttpRequestException($"Erro ao buscar filmes: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<MovieList>(content) ?? new MovieList();

            _logger.LogInformation("Foram encontrados {Count} filmes.", movies.Results.Count);
            return movies.Results;
        }

        public async Task<Movie> GetMovieDetailsAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("ID do filme inválido: {Id}", id);
                throw new ArgumentException("O ID do filme deve ser maior que zero.");
            }

            _logger.LogInformation("Buscando detalhes para o filme com ID: {Id}", id);
            var response = await _httpClient.GetAsync($"{_apiSetting.BaseUrl}/movie/{id}?api_key={_apiSetting.ApiKey}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Erro ao buscar detalhes do filme: {StatusCode}", response.StatusCode);
                throw new HttpRequestException($"Erro ao buscar detalhes do filme: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var movie = JsonConvert.DeserializeObject<Movie>(content);

            _logger.LogInformation("Detalhes encontrados para o filme com ID: {Id}", id);

            return movie ?? new Movie();
        }
    }
}
