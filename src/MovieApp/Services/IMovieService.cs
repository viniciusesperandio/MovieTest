using MovieApp.Models;

namespace MovieApp.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMoviesAsync();
        Task<Movie> GetMovieDetailsAsync(int id);
    }
}
