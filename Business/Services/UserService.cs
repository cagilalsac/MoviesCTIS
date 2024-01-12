using Business.Models;
using Business.Results;
using Business.Results.Bases;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly Db _db;

        public UserService(Db db)
        {
            _db = db;
        }

        public Result Add(UserModel model)
        {
            if (_db.Users.Any(u => u.UserName == model.UserName.Trim()))
                return new ErrorResult("User could not be added because user with the same user name exists!");
            var entity = new User()
            {
                UserName = model.UserName.Trim(),
                Password = model.Password.Trim(),
                Guid = Guid.NewGuid().ToString(),
                IsActive = model.IsActive,
                RoleId = model.RoleId.Value
            };
            _db.Users.Add(entity);
            _db.SaveChanges();
            model.Id = entity.Id;
            return new SuccessResult("User added successfully.");
        }

        public Result Delete(int id)
        {
            var entity = _db.Users.Find(id);
            if (entity is null)
                return new ErrorResult("User could not be found!");
            _db.Users.Remove(entity);
            _db.SaveChanges();
            return new SuccessResult("User deleted successfully.");
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users.OrderByDescending(u => u.IsActive).ThenBy(u => u.UserName).Select(u => new UserModel()
            {
                Guid = u.Guid,
                Id = u.Id,
                UserName = u.UserName,
                Password = u.Password,
                IsActive = u.IsActive,
                RoleId = u.RoleId,
                IsActiveOutput = u.IsActive ? "Yes" : "No",
                PasswordOutput = new string('*', u.Password.Length),
                RoleOutput = new RoleModel()
                {
                    Name = u.Role.Name
                }
            });
        }

        public Result Update(UserModel model)
        {
            if (_db.Users.Any(u => u.UserName == model.UserName.Trim() && u.Id != model.Id))
                return new ErrorResult("User could not be updated because user with the same user name exists!");
            var entity = new User()
            {
                Id = model.Id,
                UserName = model.UserName.Trim(),
                Password = model.Password.Trim(),
                Guid = Guid.NewGuid().ToString(),
                IsActive = model.IsActive,
                RoleId = model.RoleId.Value
            };
            _db.Users.Update(entity);
            _db.SaveChanges();
            return new SuccessResult("User updated successfully.");
        }
    }
}
