using Microsoft.Extensions.Options;
using MovieApp.Services;
using MovieApp.Settings;
using Moq;
using Moq.Protected;
using Microsoft.Extensions.Logging;

namespace MovieApp.Tests
{
    public class MovieServiceTests : IDisposable
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly ILogger<MovieService> _loggerMock;
        private readonly IOptions<ApiSetting> _apiSettings;

        public MovieServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _loggerMock = Mock.Of<ILogger<MovieService>>();

            _apiSettings = Options.Create(new ApiSetting
            {
                ApiKey = "0b9039e8f36225ed42ba8522aaa2657d",
                BaseUrl = "https://api.themoviedb.org/3"
            });
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        [Fact]
        public async Task GetMoviesAsync_ReturnsListOfMovies()
        {
            // Arrange
            SetupHttpResponseMessage("{ \"results\": [ { \"id\": 1, \"title\": \"Filme Teste\" } ] }");
            var movieService = new MovieService(_httpClient, _apiSettings, _loggerMock);

            // Act
            var result = await movieService.GetMoviesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("Filme Teste", result[0].Title);
        }

        [Fact]
        public async Task GetMovieDetailsAsync_ReturnsMovieDetails()
        {
            // Arrange
            SetupHttpResponseMessage("{ \"id\": 1022789, \"title\": \"Filme Teste\", \"overview\": \"Detalhe Teste\" }");
            var movieService = new MovieService(_httpClient, _apiSettings, _loggerMock);

            // Act
            var result = await movieService.GetMovieDetailsAsync(1022789);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1022789, result.Id);
            Assert.Equal("Filme Teste", result.Title);
            Assert.Equal("Detalhe Teste", result.Overview);
        }

        private void SetupHttpResponseMessage(string content)
        {
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(content)
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(responseMessage);
        }
    }
}
