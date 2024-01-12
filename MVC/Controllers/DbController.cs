using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace MVC.Controllers
{
    public class DbController : Controller
    {
        private readonly Db _db;

        public DbController(Db db)
        {
            _db = db;
        }

        public IActionResult Seed()
        {
            // deleting all table data
            var movieGenres = _db.MovieGenres.ToList();
            _db.MovieGenres.RemoveRange(movieGenres);
            var genres = _db.Genres.ToList();
            _db.Genres.RemoveRange(genres);
            var movies = _db.Movies.ToList();
            _db.Movies.RemoveRange(movies);
            var directors = _db.Directors.ToList();
            _db.Directors.RemoveRange(directors);
            _db.Users.RemoveRange(_db.Users.ToList());
            _db.Roles.RemoveRange(_db.Roles.ToList());

            // inserting data
            _db.Genres.Add(new Genre()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = "Science Fiction"
            });
            _db.Genres.Add(new Genre()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = "Action"
            });
            _db.Genres.Add(new Genre()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = "Crime"
            });
            _db.Genres.Add(new Genre()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = "Horror"
            });

            _db.SaveChanges();

            _db.Directors.Add(new Director()
            {
                BirthDate = DateTime.Parse("08/16/1954", new CultureInfo("en-US")),
                Guid = Guid.NewGuid().ToString(),
                IsRetired = true,
                Name = "James",
                Surname = "Cameron",
                Movies = new List<Movie>()
                {
                    new Movie()
                    {
                        Guid = Guid.NewGuid().ToString(),
                        Name = "Avatar",
                        Year = 2009,
                        Revenue = 9000000.25,
                        MovieGenres = new List<MovieGenre>()
                        {
                            new MovieGenre()
                            {
                                GenreId = _db.Genres.SingleOrDefault(g => g.Name == "Science Fiction").Id
                            },
                            new MovieGenre()
                            {
                                GenreId = _db.Genres.SingleOrDefault(g => g.Name == "Action").Id
                            }
                        }
                    },
                    new Movie()
                    {
                        Guid = Guid.NewGuid().ToString(),
                        Name = "Aliens",
                        Year = 1986,
                        Revenue = 8000000.75,
                        MovieGenres = new List<MovieGenre>()
                        {
                            new MovieGenre()
                            {
                                GenreId = _db.Genres.SingleOrDefault(g => g.Name == "Science Fiction").Id
                            },
                            new MovieGenre()
                            {
                                GenreId = _db.Genres.SingleOrDefault(g => g.Name == "Action").Id
                            },
                            new MovieGenre()
                            {
                                GenreId = _db.Genres.SingleOrDefault(g => g.Name == "Horror").Id
                            }
                        }
                    }
                }
            });
            _db.Directors.Add(new Director()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = "Guy",
                Surname = "Ritchie",
                Movies = new List<Movie>()
                {
                    new Movie()
                    {
                        Guid = Guid.NewGuid().ToString(),
                        Name = "Sherlock Holmes",
                        Year = 2009,
                        Revenue = 1000000,
                        MovieGenres = new List<MovieGenre>()
                        {
                            new MovieGenre()
                            {
                                GenreId = _db.Genres.SingleOrDefault(g => g.Name == "Action").Id
                            },
                            new MovieGenre()
                            {
                                GenreId = _db.Genres.SingleOrDefault(g => g.Name == "Crime").Id
                            }
                        }
                    }
                }
            });

            _db.Roles.Add(new Role()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = "Admin",
                Users = new List<User>()
                {
                    new User()
                    {
                        Guid = Guid.NewGuid().ToString(),
                        IsActive = true,
                        Password = "cagil",
                        UserName = "cagil"
                    }
                }
            });
            _db.Roles.Add(new Role()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = "User",
                Users = new List<User>()
                {
                    new User()
                    {
                        Guid = Guid.NewGuid().ToString(),
                        IsActive = true,
                        Password = "leo",
                        UserName = "leo"
                    }
                }
            });

            _db.SaveChanges();

            return Content("<label style=\"color:red;\">Database seed successful.</label>", "text/html", Encoding.UTF8);
        }
    }
}
