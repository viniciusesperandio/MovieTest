namespace MovieApp.Models
{
    public class MovieList
    {
        public int Page { get; set; }
        public List<Movie> Results { get; set; } = new List<Movie>();
    }
}
