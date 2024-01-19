using Business.Models;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class RoleService : IRoleService
    {
        private readonly Db _db;

        public RoleService(Db db)
        {
            _db = db;
        }

        public Result Add(RoleModel model)
        {
            if (_db.Roles.Any(r => r.Name.ToUpper() == model.Name.ToUpper().Trim())) 
                return new ErrorResult("Role could not be added because role with the same name exists!");
            var entity = new Role()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = model.Name.Trim()
            };
            _db.Roles.Add(entity);
            _db.SaveChanges();
            model.Id = entity.Id;
            return new SuccessResult("Role added successfully.");
        }

        public Result Delete(int id)
        {
            var entity = _db.Roles.Include(r => r.Users).SingleOrDefault(r => r.Id == id);
            if (entity is null)
                return new ErrorResult("Role could not be found!");
            if (entity.Users.Any())
                return new ErrorResult("Role could not be deleted because role has users!");
            _db.Roles.Remove(entity);
            _db.SaveChanges();
            return new SuccessResult("Role deleted successfully.");
        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.OrderBy(r => r.Name).Select(r => new RoleModel()
            {
                Guid = r.Guid,
                Id = r.Id,
                Name = r.Name,
                UsersOutput = r.Users.Select(u => new UserModel()
                {
                    UserName = u.UserName,
                    IsActiveOutput = u.IsActive ? "Active" : "Not Active"
                }).ToList()
            });
        }

        public Result Update(RoleModel model)
        {
            if (_db.Roles.Any(r => r.Name.ToUpper() == model.Name.ToUpper().Trim() && r.Id != model.Id))
                return new ErrorResult("Role could not be updated because role with the same name exists!");
            var entity = new Role()
            {
                Id = model.Id,
                Guid = Guid.NewGuid().ToString(),
                Name = model.Name.Trim()
            };
            _db.Roles.Update(entity);
            _db.SaveChanges();
            return new SuccessResult("Role updated successfully.");
        }
    }
}
