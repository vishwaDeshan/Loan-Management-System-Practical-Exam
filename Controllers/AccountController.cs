using LoanManagementSystemAssignment.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoanManagementSystemAssignment.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult Login(string? returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View(new LoginViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "/")
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (model.Username == "Admin" && model.Password == "Admin@123")
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, model.Username),
					new Claim(ClaimTypes.Role, "Admin")
				};

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				return LocalRedirect(returnUrl);
			}

			ModelState.AddModelError(string.Empty, "Invalid username or password");
			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login");
		}
	}
}
