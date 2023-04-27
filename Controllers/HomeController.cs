using FINALNIPROJEKTBOOP.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using FINALNIPROJEKTBOOP.AppData;
using Microsoft.Data.SqlClient;

namespace FINALNIPROJEKTBOOP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<HomeController> _logger;
        public MovieModel moviemodel = new MovieModel();
        private string _connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = DatabazeFilmu; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False");

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index(MovieModel moviemodel)
        {

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
                    moviemodel.movies.Add(movie);
                }
            }
            ViewBag.movies = moviemodel.movies;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddMovie()
        {
            return View();
        }

        public IActionResult AddActor()
        {
            return View();
        }
        public IActionResult AddDirector()
        {
            return View();
        }

        public IActionResult EditMovie()
        {
            return View();
        }





        [HttpPost]
        public IActionResult AddMovie(MovieModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO MOVIES (NameOfMovie, Genre, ReleaseDate) VALUES (@NameOfMovie, @Genre, @ReleaseDate)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NameOfMovie", NameOfMovie);
                    command.Parameters.AddWithValue("@Genre", Genre);
                    command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditMovie(MovieModel model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (var movie in model.movies)
                {
                    var command = new SqlCommand("UPDATE Movies SET Title = @Title, Description = @Description, ReleaseDate = @ReleaseDate WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", movie.Id);
                    command.Parameters.AddWithValue("@Title", movie.Title);
                    command.Parameters.AddWithValue("@Description", movie.Description);
                    command.Parameters.AddWithValue("@ReleaseDate", movie.ReleaseDate);

                    command.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}