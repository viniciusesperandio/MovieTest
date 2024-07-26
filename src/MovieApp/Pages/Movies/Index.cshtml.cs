using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieApp.Models;
using MovieApp.Services;

namespace MovieApp.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly IMovieService _movieService;

        public IndexModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IList<Movie> Movies { get; set; }

        public async Task OnGetAsync()
        {
            Movies = await _movieService.GetMoviesAsync();
        }
    }
}
