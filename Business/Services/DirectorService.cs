using Business.Models;
using Business.Results;
using Business.Results.Bases;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly Db _db;

        public DirectorService(Db db)
        {
            _db = db;
        }

        public Result Add(DirectorModel model)
        {
            if (_db.Directors.Any(d => d.Name.ToUpper() == model.Name.ToUpper().Trim() 
                && d.Surname.ToUpper() == model.Surname.ToUpper().Trim()))
                return new ErrorResult("Director could not be added because director with the same name and surname exists!");
            var entity = new Director()
            {
                BirthDate = model.BirthDate,
                Guid = Guid.NewGuid().ToString(),
                IsRetired = model.IsRetired,
                Name = model.Name.Trim(),
                Surname = model.Surname.Trim()
            };
            _db.Directors.Add(entity);
            _db.SaveChanges();
            return new SuccessResult("Director added successfully.");
        }

        public Result Delete(int id)
        {
            var entity = _db.Directors.Include(d => d.Movies).SingleOrDefault(d => d.Id == id);
            if (entity is null)
                return new ErrorResult("Director could not be found!");
            if (entity.Movies.Any())
                return new ErrorResult("Director could not be deleted because director has movies!");
            _db.Directors.Remove(entity);
            _db.SaveChanges();
            return new SuccessResult("Director deleted successfully.");
        }

        public IQueryable<DirectorModel> Query()
        {
            return _db.Directors.Include(d => d.Movies).OrderBy(d => d.Name).ThenBy(d => d.Surname)
                .Select(d => new DirectorModel()
                {
                    BirthDate = d.BirthDate,
                    BirthDateOutput = d.BirthDate.HasValue ? d.BirthDate.Value.ToString("MM/dd/yyyy") : "",
                    FullNameOutput = d.Name + " " + d.Surname,
                    Guid = d.Guid,
                    Id = d.Id,
                    IsRetired = d.IsRetired,
                    MoviesOutput = d.Movies.OrderByDescending(m => m.Year).ThenBy(m => m.Name).Select(m => new MovieModel()
                    {
                        Name = m.Name,
                        Year = m.Year
                    }).ToList(),
                    Name = d.Name,
                    Surname = d.Surname,
                    IsRetiredOutput = d.IsRetired ? "Retired" : "Not retired"
                });
        }

        public Result Update(DirectorModel model)
        {
            if (_db.Directors.Any(d => d.Name.ToUpper() == model.Name.ToUpper().Trim()
               && d.Surname.ToUpper() == model.Surname.ToUpper().Trim() && d.Id != model.Id))
                return new ErrorResult("Director could not be added because director with the same name and surname exists!");
            var entity = new Director()
            {
                Id = model.Id,
                BirthDate = model.BirthDate,
                Guid = Guid.NewGuid().ToString(),
                IsRetired = model.IsRetired,
                Name = model.Name.Trim(),
                Surname = model.Surname.Trim()
            };
            _db.Directors.Update(entity);
            _db.SaveChanges();
            return new SuccessResult("Director updated successfully.");
        }
    }
}
