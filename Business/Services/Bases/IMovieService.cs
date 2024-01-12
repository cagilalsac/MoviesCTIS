using Business.Models;
using Business.Results.Bases;

namespace Business.Services.Bases
{
    public interface IMovieService
    {
        IQueryable<MovieModel> Query();
        Result Add(MovieModel model);
        Result Update(MovieModel model);
        Result Delete(int id);
    }
}
