using Business.Models;
using DataAccess.Results.Bases;

namespace Business.Services.Bases
{
    public interface IGenreService
    {
        IQueryable<GenreModel> Query();
        Result Add(GenreModel model);
        Result Update(GenreModel model);
        Result Delete(int id);
        List<GenreModel> GetList();
        GenreModel GetItem(int id);
    }
}
