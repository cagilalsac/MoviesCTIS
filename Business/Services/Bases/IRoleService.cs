using Business.Models;
using DataAccess.Results.Bases;

namespace Business.Services.Bases
{
    public interface IRoleService
    {
        IQueryable<RoleModel> Query();
        Result Add(RoleModel model);
        Result Update(RoleModel model);
        Result Delete(int id);
    }
}
