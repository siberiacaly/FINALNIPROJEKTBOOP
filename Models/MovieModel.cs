using FINALNIPROJEKTBOOP.AppData;
using Microsoft.Data.SqlClient;

namespace FINALNIPROJEKTBOOP.Models
{
    public class MovieModel
    {
        public string NameOfMovie { get; set; }
        public string Genre { get; set; }
        public int ReleaseDate { get; set; }

        public List<Movie> movies { get; set; }
        public MovieModel() { this.movies = new List<Movie>(); }


        public IList<Movie> GetAllMovies(string connectionString)
        {
        
        }
    }
}
