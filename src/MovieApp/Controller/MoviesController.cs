using Microsoft.AspNetCore.Mvc;
using MovieApp.Services;

namespace MovieApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMovieDetails(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid movie ID.");
            }

            var movie = await _movieService.GetMovieDetailsAsync(id);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            return Ok(movie);
        }
    }
}
