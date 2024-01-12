#nullable disable
using Business.Models;
using Business.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//Generated from Custom Template.
namespace MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: Roles
        public IActionResult Index()
        {
            List<RoleModel> roleList = _roleService.Query().ToList();
            return View(roleList);
        }

        // GET: Roles/Details/5
        public IActionResult Details(int id)
        {
            RoleModel role = _roleService.Query().SingleOrDefault(r => r.Id == id);
            if (role == null)
            {
                return View("_Error", "User could not be found!");
            }
            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                var result = _roleService.Add(role);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new {id = role.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public IActionResult Edit(int id)
        {
            RoleModel role = _roleService.Query().SingleOrDefault(r => r.Id == id);
            if (role == null)
            {
                return View("_Error", "User could not be found!");
            }
            return View(role);
        }

        // POST: Roles/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                var result = _roleService.Update(role);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = role.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public IActionResult Delete(int id)
        {
            TempData["Message"] = _roleService.Delete(id).Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
