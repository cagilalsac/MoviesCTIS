using Business.Models;
using DataAccess.Results.Bases;

namespace Business.Services.Bases
{
    public interface IDirectorService
    {
        IQueryable<DirectorModel> Query();
        Result Add(DirectorModel model);
        Result Update(DirectorModel model);
        Result Delete(int id);
    }
}
