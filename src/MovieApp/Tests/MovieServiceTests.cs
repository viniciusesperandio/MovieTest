using Microsoft.Extensions.Options;
using MovieApp.Services;
using MovieApp.Settings;
using Xunit;
using Moq;
using Moq.Protected;

namespace MovieApp.Tests
{
    public class MovieServiceTests
    {
        private readonly string _apiKey = "0b9039e8f36225ed42ba8522aaa2657d";

        private readonly string _baseUrl = "https://api.themoviedb.org/3";

        [Fact]
        public async Task GetMoviesAsync_ReturnsListOfMovies()
        {
            // Arrange
            var apiSettings = Options.Create(new ApiSetting { 
                ApiKey = _apiKey,
                BaseUrl = _baseUrl
            });
            var logger = Mock.Of<ILogger<MovieService>>();
            var movieService = new MovieService(new HttpClient(), apiSettings, logger);

            // Mock HTTP response
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("{ \"results\": [ { \"id\": 1, \"title\": \"Filme Teste\" } ] }")
            };
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(httpMessageHandlerMock.Object);

            // Act
            var result = await movieService.GetMoviesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("Test Movie", result[0].Title);
        }

        [Fact]
        public async Task GetMovieDetailsAsync_ReturnsMovieDetails()
        {
            // Arrange
            var apiSettings = Options.Create(new ApiSetting { 
                ApiKey = _apiKey,
                BaseUrl = _baseUrl
            });
            var logger = Mock.Of<ILogger<MovieService>>();
            var movieService = new MovieService(new HttpClient(), apiSettings, logger);

            // Mock HTTP response
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("{ \"id\": 1, \"title\": \"Test Movie\", \"overview\": \"Test Overview\" }")
            };
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(httpMessageHandlerMock.Object);

            // Act
            var result = await movieService.GetMovieDetailsAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Movie", result.Title);
            Assert.Equal("Test Overview", result.Overview);
        }
    }
}
