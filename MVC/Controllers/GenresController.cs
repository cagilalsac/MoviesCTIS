#nullable disable
using Business.Models;
using Business.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//Generated from Custom Template.
namespace MVC.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: Genres
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<GenreModel> genreList = _genreService.GetList();
            return View(genreList);
        }

        // GET: Genres/Details/5
        public IActionResult Details(int id)
        {
            GenreModel genre = _genreService.GetItem(id);
            if (genre == null)
            {
                return View("_Error", "Genre could not be found!");
            }
            return View(genre);
        }

        // GET: Genres/Create
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public IActionResult Create(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                var result = _genreService.Add(genre);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        [Authorize(Roles = "User")]
        public IActionResult Edit(int id)
        {
            GenreModel genre = _genreService.GetItem(id);
            if (genre == null)
            {
                return View("_Error", "Genre could not be found!");
            }
            return View(genre);
        }

        // POST: Genres/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public IActionResult Edit(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                var result = _genreService.Update(genre);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        [Authorize(Roles = "User")]
        public IActionResult Delete(int id)
        {
            GenreModel genre = _genreService.GetItem(id);
            if (genre == null)
            {
                return View("_Error", "Genre could not be found!");
            }
            return View(genre);
        }

        // POST: Genres/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public IActionResult DeleteConfirmed(int id)
        {
            TempData["Message"] = _genreService.Delete(id).Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
