using MarketMatrix.Repository;
using MarketMatrix_Models;
using MarketMatrix_Models.ViewModels;
using MarketMatrix_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketMatrix.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class HomeController : Controller
	{

        private readonly OrdersRepo _ordersRepo;
		private readonly AddressRepo _addressesRepo;
		private readonly UserManager<ApplicationUser> _userManager;
		public HomeController(OrdersRepo ordersRepo, AddressRepo addressRepo, UserManager<ApplicationUser> userManager)
		{
			_ordersRepo = ordersRepo;
			_addressesRepo = addressRepo;
			_userManager = userManager;
		}

        public IActionResult Index()
		{
			return View();
		}

		public IActionResult Orders(string statustype) {

			ViewData["StatusType"] = statustype;

            return View();

		}

		public IActionResult OrdersDetails(int id) {

			if (id != 0) {
                Orders order = _ordersRepo.GetById(id);
				
				Address address = _addressesRepo.GetById(order.User.AddressId??0);
				if (address != null) {
					ViewData["address"] = address;
                }
                
				return View(order);

            }

			return View();

		}

		public IActionResult ViewCustomer() { 
			return View();
		}


		// api calls

        [HttpGet]
        public IActionResult GetOrders(string statusType)
        {

            List<Orders> orders = _ordersRepo.GetAllWithUser(statusType);

            return Json(new { data = orders });

        }

		[HttpPost]
		public IActionResult EditOrderStatus(int id, string status)
		{
			try {

                Orders orders = _ordersRepo.GetById(id);
                orders.status = status;
				orders.StatusChangeDate = DateTime.Now;

                _ordersRepo.Update(orders);
				_ordersRepo.Save();

				return Json(new { success = true });

            } catch(Exception ex)
			{
                return Json(new { success = false });
            }

		}

		[HttpGet]
		public async Task<IActionResult> GetCustomerDetails() {

			List<UserWithRolesVM> userWithRolesVms = new List<UserWithRolesVM>();

			// we need await to get all user first and then loop through
			var users = await _userManager.Users.ToListAsync();

			foreach (var user in users) { 

				var roles = await _userManager.GetRolesAsync(user);

				var address = _addressesRepo.GetById(user.AddressId ?? 0);

                userWithRolesVms.Add(new UserWithRolesVM{

					UserId = user.Id,
					FullName = user.Name,
					Username = user.UserName,
					PhoneNumber = user.PhoneNumber,
					Roles = roles.ToList(),
					Address =  address != null ? address.ToString() : ""

				});

			}


			return Json(new { data = userWithRolesVms });


		}


    }
}
