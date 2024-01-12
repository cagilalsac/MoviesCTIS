#nullable disable
using Business.Models;
using Business.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

//Generated from Custom Template.
namespace MVC.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IDirectorService _directorService;
        private readonly IGenreService _genreService;

        public MoviesController(IMovieService movieService, IDirectorService directorService, IGenreService genreService)
        {
            _movieService = movieService;
            _directorService = directorService;
            _genreService = genreService;
        }

        // GET: Movies
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<MovieModel> movieList = _movieService.Query().ToList();
            return View(movieList);
        }

        // GET: Movies/Details/5
        public IActionResult Details(int id)
        {
            MovieModel movie = _movieService.Query().SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "FullNameOutput");
            ViewBag.Genres = new MultiSelectList(_genreService.GetList(), "Id", "Name");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public IActionResult Create(MovieModel movie)
        {
            if (ModelState.IsValid)
            {
                var result = _movieService.Add(movie);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "FullNameOutput");
			ViewBag.Genres = new MultiSelectList(_genreService.GetList(), "Id", "Name");
			return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "User")]
        public IActionResult Edit(int id)
        {
            MovieModel movie = _movieService.Query().SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "FullNameOutput");
			ViewBag.Genres = new MultiSelectList(_genreService.GetList(), "Id", "Name");
			return View(movie);
        }

        // POST: Movies/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public IActionResult Edit(MovieModel movie)
        {
			if (ModelState.IsValid)
			{
				var result = _movieService.Update(movie);
				if (result.IsSuccessful)
				{
					TempData["Message"] = result.Message;
					return RedirectToAction(nameof(Index));
				}
				ModelState.AddModelError("", result.Message);
			}
			ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "FullNameOutput");
			ViewBag.Genres = new MultiSelectList(_genreService.GetList(), "Id", "Name");
			return View(movie);
		}

        // GET: Movies/Delete/5
        [Authorize(Roles = "User")]
        public IActionResult Delete(int id)
        {
            var result = _movieService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
