using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FINALNIPROJEKTBOOP.Models
{
    public class Movie
    {
        public string NameOfMovie { get; set; }
        public int ReleaseDate { get; set; }
        public string Genre { get; set; }
    }
    public class IndexModel
    {
        private readonly string _connectionString;
        
        public IndexModel(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<Movie> GetAllMovies()
        {
            var movies = new List<Movie>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM dbo.MOVIES", connection);
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var movie = new Movie
                    {
                        NameOfMovie = reader.GetString(0),
                        ReleaseDate = reader.GetInt32(1),
                        Genre = reader.GetString(2)
                    };
                    movies.Add(movie);
                }
            }
            return movies;
        }
    }
}
