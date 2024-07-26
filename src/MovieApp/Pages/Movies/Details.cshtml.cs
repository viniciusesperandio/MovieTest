using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieApp.Models;
using MovieApp.Services;

namespace MovieApp.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly IMovieService _movieService;

        public DetailsModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movie = await _movieService.GetMovieDetailsAsync(id);

            if (Movie == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
