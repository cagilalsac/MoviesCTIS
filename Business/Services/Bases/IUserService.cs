using Business.Models;
using DataAccess.Results.Bases;

namespace Business.Services.Bases
{
    public interface IUserService
    {
        IQueryable<UserModel> Query();
        Result Add(UserModel model);
        Result Update(UserModel model);
        Result Delete(int id);
    }
}
