using Newtonsoft.Json;

namespace MovieApp.Models
{
    public class Movie
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; } = string.Empty;

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; } = string.Empty;

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; } = string.Empty;

        [JsonProperty("overview")]
        public string Overview { get; set; } = string.Empty;

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; } = string.Empty;

        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("genre_ids")]
        public List<int> GenreIds { get; set; } = new List<int>();

        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; } = new List<Genre>();

        #region ReadOnly

        [JsonIgnore]
        public string FullPosterPath => $"https://image.tmdb.org/t/p/w500{PosterPath}";

        [JsonIgnore]
        public string AdultFormatted => Adult ? "Sim" : "Não";

        [JsonIgnore]
        public string LanguageName => GetLanguageName(OriginalLanguage);

        #endregion

        private string GetLanguageName(string languageCode)
        {
            return languageCode switch
            {
                "en" => "Inglês",
                "es" => "Espanhol",
                "fr" => "Francês",
                "de" => "Alemão",
                "it" => "Italiano",
                "pt" => "Português",
                _ => "Desconhecido"
            };
        }
    }
}
