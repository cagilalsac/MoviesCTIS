using Business.Models;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class MovieService : IMovieService
    {
        private readonly Db _db;

        public MovieService(Db db)
        {
            _db = db;
        }

        public Result Add(MovieModel model)
        {
            if (_db.Movies.Any(m => m.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return new ErrorResult("Movie could not be added because movie with the same name exists!");
            var entity = new Movie()
            {
                DirectorId = model.DirectorId,
                Guid = Guid.NewGuid().ToString(),
                Name = model.Name.Trim(),
                Revenue = model.Revenue ?? 0,
                Year = model.Year,
                MovieGenres = model.GenreIdsInput?.Select(gId => new MovieGenre()
                {
                    GenreId = gId
                }).ToList()
            };
            _db.Movies.Add(entity);
            _db.SaveChanges();
            return new SuccessResult("Movie added successfully.");
        }

        public Result Delete(int id)
        {
            var entity = _db.Movies.Include(m => m.MovieGenres).SingleOrDefault(m => m.Id == id);
            if (entity is null)
                return new ErrorResult("Movie could not be found!");
            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            _db.Movies.Remove(entity);
            _db.SaveChanges();
            return new SuccessResult("Movie deleted successfully.");
        }

        public IQueryable<MovieModel> Query()
        {
            return _db.Movies.Include(m => m.Director).Include(m => m.MovieGenres).ThenInclude(mg => mg.Movie).OrderByDescending(m => m.Year)
                .ThenByDescending(m => m.Revenue).ThenBy(m => m.Name)
                .Select(m => new MovieModel()
                {
                    DirectorId = m.DirectorId,
                    DirectorOutput = m.Director.Name + " " + m.Director.Surname,
                    Guid = m.Guid,
                    Id = m.Id,
                    Name = m.Name,
                    Revenue = m.Revenue,
                    RevenueOutput = m.Revenue.ToString("C2"),
                    Year = m.Year,
                    GenreIdsInput = m.MovieGenres.Select(mg => mg.GenreId).ToList(),
                    GenresOutput = string.Join("<br />", m.MovieGenres.OrderBy(mg => mg.Genre.Name).Select(mg => mg.Genre.Name))
                });
        }

        public Result Update(MovieModel model)
        {
            if (_db.Movies.Any(m => m.Name.ToUpper() == model.Name.ToUpper().Trim() && m.Id != model.Id))
                return new ErrorResult("Movie could not be updated because movie with the same name exists!");
            var entity = _db.Movies.Include(m => m.MovieGenres).SingleOrDefault(m => m.Id == model.Id);
            if (entity is null)
                return new ErrorResult("Movie could not be found!");
            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            entity.DirectorId = model.DirectorId;
            entity.Name = model.Name.Trim();
            entity.Revenue = model.Revenue ?? 0;
            entity.Year = model.Year;
            entity.MovieGenres = model.GenreIdsInput?.Select(gId => new MovieGenre()
            {
                GenreId = gId
            }).ToList();
            _db.Movies.Update(entity);
            _db.SaveChanges();
            return new SuccessResult("Movie updated successfully.");
        }
    }
}
