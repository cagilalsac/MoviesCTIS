#nullable disable
using Business.Models;
using Business.Services.Bases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UsersController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        // GET: Users
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<UserModel> userList = _userService.Query().ToList();
            return View(userList);
        }

        // GET: Users/Details/5
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            UserModel user = _userService.Query().SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return View("_Error", "User could not be found!");
            }
            return View(user);
        }

        // GET: Users/Create
        // GET: Account/Register
        [HttpGet("Account/Register")]
        public IActionResult Create()
        {
            UserModel user = new UserModel()
            {
                IsActive = true
            };
            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");
            return View(user);
        }

		// POST: Users/Create
		// POST: Account/Register
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[ValidateAntiForgeryToken]
		[HttpPost("Account/Register")]
		public IActionResult Create(UserModel user)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                user.IsActive = true;
                user.RoleId = _roleService.Query().SingleOrDefault(r => r.Name == "User").Id;
                ModelState.Remove(nameof(user.RoleId));
            }
            if (ModelState.IsValid)
            {
                var result = _userService.Add(user);
                if (result.IsSuccessful)
                {
                    if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
                        return RedirectToRoute("login");
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new {id = user.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");
            return View(user);
        }

		// GET: Users/Edit/5
		[Authorize(Roles = "Admin")]
		public IActionResult Edit(int id)
        {
            UserModel user = _userService.Query().SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return View("_Error", "User could not be found!");
            }
            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");
            return View(user);
        }

        // POST: Users/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public IActionResult Edit(UserModel user)
        {
			if (ModelState.IsValid)
			{
				var result = _userService.Update(user);
				if (result.IsSuccessful)
				{
					TempData["Message"] = result.Message;
					return RedirectToAction(nameof(Details), new { id = user.Id });
				}
				ModelState.AddModelError("", result.Message);
			}
			ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");
			return View(user);
		}

		// GET: Users/Delete/5
		[Authorize(Roles = "Admin")]
		public IActionResult Delete(int id)
        {
            TempData["Message"] = _userService.Delete(id).Message;
            return RedirectToAction(nameof(Index));
        }

        #region User Authentication
        // GET: Users/Login
        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel user)
        {
            var existingUser = _userService.Query().SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password && u.IsActive);
            if (existingUser is null)
            {
                ModelState.AddModelError("", "Invalid user name and password!");
                return View();
            }
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, existingUser.UserName),
                new Claim(ClaimTypes.Role, existingUser.RoleOutput.Name)
            };
            var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Logout
        // GET: Account/Logout
        [HttpGet("Account/{action}")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/AccessDenied
        // GET: Account/AccessDenied
        [HttpGet("Account/{action}")]
        public IActionResult AccessDenied()
        {
            return View("_Error", "You don't have access to this operation!");
        }
        #endregion
    }
}
