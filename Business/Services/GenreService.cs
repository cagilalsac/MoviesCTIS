using Business.Models;
using Business.Results;
using Business.Results.Bases;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class GenreService : IGenreService
    {
        private readonly Db _db;

        public GenreService(Db db)
        {
            _db = db;
        }

        public Result Add(GenreModel model)
        {
            if (_db.Genres.Any(g => g.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return new ErrorResult("Genre could not be added because genre with the same name exists!");
            var entity = new Genre()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = model.Name.Trim()
            };
            _db.Genres.Add(entity);
            _db.SaveChanges();
            return new SuccessResult("Genre added successfully.");
        }

        public Result Delete(int id)
        {
            var entity = _db.Genres.Include(g => g.MovieGenres).SingleOrDefault(g => g.Id == id);
            if (entity == null)
                return new ErrorResult("Genre could not be found!");
            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            _db.Genres.Remove(entity);
            _db.SaveChanges();
            return new SuccessResult("Genre deleted successfully.");
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.Include(g => g.MovieGenres).ThenInclude(mg => mg.Movie).OrderBy(g => g.Name).Select(g => new GenreModel()
            {
                Guid = g.Guid,
                Id = g.Id,
                Name = g.Name,
                MoviesCountOutput = g.MovieGenres.Count,
                MoviesOutput = string.Join("<br />", g.MovieGenres.OrderBy(mg => mg.Movie.Name).Select(mg => mg.Movie.Name).ToList())
            });
        }

        public Result Update(GenreModel model)
        {
            if (_db.Genres.Any(g => g.Name.ToUpper() == model.Name.ToUpper().Trim() && g.Id != model.Id))
                return new ErrorResult("Genre could not be updated because genre with the same name exists!");
            var entity = new Genre()
            {
                Id = model.Id,
                Name = model.Name.Trim()
            };
            _db.Genres.Update(entity);
            _db.SaveChanges();
            return new SuccessResult("Genre updated successfully.");
        }

        public GenreModel GetItem(int id) => Query().SingleOrDefault(g => g.Id == id);

        public List<GenreModel> GetList() => Query().ToList();
    }
}
