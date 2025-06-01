using MarketMatrix_Models;
using MarketMatrix_Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketMatrix.Controllers
{
	public class HomeController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public HomeController(UserManager<ApplicationUser> userManager) {
			_userManager = userManager;
		}

		public async Task<IActionResult> IndexAsync()
		{
			var user = await _userManager.GetUserAsync(User);

			if (user != null) {

				if (await _userManager.IsInRoleAsync(user, SD.Role_Admin))
				{
					return RedirectToAction("Index","Home",new { area = "Admin"});
				}
				else
				{
					return RedirectToAction("Index", "Home", new { area = "Customer" });
				}
			}


			return RedirectToAction("Index", "Home", new { area = "Customer" });
		}
	}
}
