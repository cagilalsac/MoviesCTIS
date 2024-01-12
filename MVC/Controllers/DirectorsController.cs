#nullable disable
using Business.Models;
using Business.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly IDirectorService _directorService;

        public DirectorsController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        // GET: Directors
        public IActionResult Index()
        {
            List<DirectorModel> directorList = _directorService.Query().ToList();
            return View(directorList);
        }

        // GET: Directors/Details/5
        public IActionResult Details(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToRoute("login");
            DirectorModel director = _directorService.Query().SingleOrDefault(d => d.Id == id);
            if (director == null)
            {
                return View("_Error", "Director could not be found!");
            }
            return View(director);
        }

        // GET: Directors/Create
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Directors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public IActionResult Create(DirectorModel director)
        {
            if (ModelState.IsValid)
            {
                var result = _directorService.Add(director);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(director);
        }

        // GET: Directors/Edit/5
        [Authorize(Roles = "User")]
        public IActionResult Edit(int id)
        {
            DirectorModel director = _directorService.Query().SingleOrDefault(d => d.Id == id);
            if (director == null)
            {
                return View("_Error", "Director could not be found!");
            }
            return View(director);
        }

        // POST: Directors/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public IActionResult Edit(DirectorModel director)
        {
            if (ModelState.IsValid)
            {
                var result = _directorService.Update(director);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(director);
        }

        // GET: Directors/Delete/5
        [Authorize(Roles = "User")]
        public IActionResult Delete(int id)
        {
            DirectorModel director = _directorService.Query().SingleOrDefault(d => d.Id == id);
            if (director == null)
            {
                return View("_Error", "Director could not be found!");
            }
            return View(director);
        }

        // POST: Directors/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _directorService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
