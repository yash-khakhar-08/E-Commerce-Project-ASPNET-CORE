using MarketMatrix.Repository;
using MarketMatrix_Models;
using MarketMatrix_Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace MarketMatrix.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly CategoryRepo _categoryRepo;

        private readonly ProductRepo _productRepo;

        private readonly MyCartRepo _myCartRepo;

        private readonly AddressRepo _addressRepo;

        private readonly OrdersRepo _ordersRepo;

        private readonly UserManager<ApplicationUser> _appUser;
        private ApplicationUser user;

        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, CategoryRepo categoryRepo, ProductRepo productRepo, MyCartRepo myCartRepo, 
            UserManager<ApplicationUser> appUser, AddressRepo addressRepo, OrdersRepo ordersRepo, IEmailSender emailSender)
        {
            _logger = logger;
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
            _myCartRepo = myCartRepo;
            _appUser = appUser;
            _addressRepo = addressRepo;
            _ordersRepo = ordersRepo;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {

            List<Category> categories = _categoryRepo.GetAll(includeProperties: "Products");

            string userId = _appUser.GetUserId(User);
            if (userId != null) {
                List<MyCart> myCart = _myCartRepo.GetAll(userId);

                // Pass it to the view using ViewBag
                ViewBag.CartItems = myCart;

            }


            return View(categories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public IActionResult GetCategory() {

            List<Category> categories = _categoryRepo.GetAll(includeProperties: "Products").Where(c => c.Products.Any()).ToList();
            if (categories != null)
            {
                _logger.LogInformation("ok bro getting data");
                return Json(new { success = true, data = categories });
            }
            else
            {
                _logger.LogInformation("not ok");
                return Json(new { success = false });
            }


        }

        public IActionResult Men() {

            List<Category> categories = _categoryRepo.GetAll(includeProperties: "Products").Where(c => c.Products.Any() && c.SectionName == "Male").ToList();

            string userId = _appUser.GetUserId(User);
            if (userId != null)
            {
                List<MyCart> myCart = _myCartRepo.GetAll(userId);

                // Pass it to the view using ViewBag
                ViewBag.CartItems = myCart;

            }


            return View(categories);

        }

		public IActionResult Women()
		{

			List<Category> categories = _categoryRepo.GetAll(includeProperties: "Products").Where(c => c.Products.Any() && c.SectionName == "Female").ToList();

            string userId = _appUser.GetUserId(User);
            if (userId != null)
            {
                List<MyCart> myCart = _myCartRepo.GetAll(userId);

                // Pass it to the view using ViewBag
                ViewBag.CartItems = myCart;

            }

            return View(categories);

		}

        [HttpPost]
        public IActionResult AddToCart(int productId, int qty)
        {
            string user = _appUser.GetUserId(User);

            if (user != null)
            {

                MyCart myCart = new MyCart();
                myCart.ProductId = productId;
                myCart.Quantity = qty;
                myCart.UserId = user;

                _myCartRepo.Add(myCart);
                _myCartRepo.Save();

                return Json(new { success = true });
            }
            else {
                return Json(new { success = false });

            }

        }

        [HttpGet]
        public IActionResult GetCartCount() { 
        
            string user = _appUser.GetUserId(User);
            _logger.LogInformation("calling");
            if (user != null)
            {
                List<MyCart> myCart = _myCartRepo.GetAll(user);

                if (myCart != null)
                {
                    _logger.LogInformation("Cart count :" + myCart.Count);
                    return Json(new { success = true, count=myCart.Count });
                }
                else {
                    _logger.LogInformation("Cart count :" + 0);
                    return Json(new { success = true, count=0 });
                }

            }

            return Json(new { success = false });   

        }

        [HttpGet]
        public IActionResult GoToCart() {

            string userId = _appUser.GetUserId(User);
            List<MyCart> cartItems = _myCartRepo.GetAll(userId);
            return View(cartItems);

        }

        [HttpGet]
        public IActionResult ViewProduct(int id) {

            string userId = _appUser.GetUserId(User);
            
            Products product = _productRepo.GetById(id);

            List<Products> productsList = _productRepo.GetByCategoryId(product.CategoryId);
            if (productsList.Any()) {

                productsList.Remove(product);
                ViewBag.ProductsList = productsList;

                if (userId != null) {
                    List<MyCart> myCart = _myCartRepo.GetAll(userId);

                    // Pass it to the view using ViewBag
                    ViewBag.CartItems = myCart;
                }

            }

            if (userId != null)
            {
                MyCart myCart = _myCartRepo.GetByProductId(id, userId);
                if (myCart != null)
                {
                    ViewBag.InCart = true;
                }
                else {
                    ViewBag.InCart = false;
                }
            }
            else {
                ViewBag.InCart = false;
            }

            return View(product);


        }

        [HttpGet]
        public IActionResult UpdateCart(int qty, int cartId) {
            _logger.LogInformation("qty" + qty);
            _logger.LogInformation("cartid" + cartId);

            MyCart myCart = _myCartRepo.GetById(cartId);
            if (myCart != null) {
                myCart.Quantity = qty;
                _myCartRepo.Update(myCart);
                _myCartRepo.Save();
                return Json(new { success = true });
            }

            return Json(new { success = false });

        }

        [HttpGet]
        public IActionResult RemoveFromCart(int cartId) {

            MyCart myCart = _myCartRepo.GetById(cartId);
            if (myCart != null) { 
                _myCartRepo.Delete(myCart);
                _myCartRepo.Save();
                return Json(new { success = true });
            }

            return Json(new { success = false });

        }

        public IActionResult Search(string query)
        {
            ViewBag.Query = query;
            Regex regex = new Regex(@"\b(\d+)\D*");
            MatchCollection matches = regex.Matches(query);
            int minPrice = 0;
            int maxPrice = 0;

            if (matches.Count == 2)
            {
                // Two prices
                minPrice = int.Parse(matches[0].Groups[1].Value);  // First price
                maxPrice = int.Parse(matches[1].Groups[1].Value);  // Second price
                query = regex.Replace(query, "", 2);
                
            }
            else if (matches.Count == 1)
            {
                // One price → min and max are the same
                maxPrice = int.Parse(matches[0].Groups[1].Value);
                
                query = regex.Replace(query, "", 1);
                
            }
           

            List<Products> products = _productRepo.GetBySearchQuery(query, minPrice, maxPrice);
            ViewBag.Results = products.Count;

            string userId = _appUser.GetUserId(User);
            if (userId != null)
            {
                List<MyCart> myCart = _myCartRepo.GetAll(userId);

                // Pass it to the view using ViewBag
                ViewBag.CartItems = myCart;

            }

            if (products.Any()) {

                List<Products> productsList = _productRepo.GetByCategoryId(products[0].CategoryId);

                if (productsList.Any())
                {

                    foreach (Products product in products)
                    {
                        productsList.Remove(product);
                    }

                    ViewBag.ProductsList = productsList;

                }

            }


            return View(products);

        }

        [HttpGet]
        public async Task<IActionResult> CheckoutAsync(int total) {

            ViewBag.total = total;

            string userId = _appUser.GetUserId(User);
            if (userId != null)
            {

                user = await _appUser.FindByIdAsync(userId);

                if (user != null)
                {
                    ViewBag.Name = user.Name;
                    ViewBag.Email = user.Email;

                    Address address = _addressRepo.GetById(user.AddressId ?? 0);
                    if (address != null) { 
                        ViewBag.Address = address.ToString();
                    }
                    
                }
                

            }

            return View();
        }

        public async Task<IActionResult> SaveAddress(string blockNo, string apartmentName, string pinCode, string city, string state, string country) {
            Address Address = new Address(blockNo, apartmentName, pinCode, city, state, country);
            _addressRepo.Add(Address);
            _addressRepo.Save();

            string userId = _appUser.GetUserId(User);
            if (userId != null) {

                user = await _appUser.FindByIdAsync(userId);

                if (user != null)
                {

                    user.AddressId = Address.Id;

                    var result = await _appUser.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
                else {
                    return Json(new { success = false });
                }

            }
            else
            {
                return Json(new { success = false });
            }


        }

        [HttpPost]
        public IActionResult PlaceOrder(string paymentMode) {
            string userId = _appUser.GetUserId(User);
            if (userId != null)
            {

                List<MyCart> cartItems = _myCartRepo.GetAll(userId);

                if (cartItems != null)
                {
                    foreach (MyCart cartItem in cartItems)
                    {

                        Orders orders = new Orders();
                        orders.UserId = cartItem.UserId;
                        orders.ProductId = cartItem.ProductId;

                        Products Product = _productRepo.GetById(cartItem.ProductId);
                        Product.Quantity = Product.Quantity - cartItem.Quantity;

                        orders.Quantity = cartItem.Quantity;
                        orders.total = cartItem.Quantity * Product.Price;
                        orders.PaymentMode = paymentMode;
                        orders.DateTime = DateTime.Now;
                        orders.status = "Pending";

                        _ordersRepo.Add(orders);
                        _ordersRepo.Save();
                        _productRepo.Update(Product);
                        _productRepo.Save();

                        _myCartRepo.Delete(cartItem);
                        _myCartRepo.Save();


                    }

                    return Json(new { success = true });

                }
                else { 
                    return Json(new { success = false });
                }

            }
            else { 
                return Json(new { success = false });
            }
        }

        public IActionResult ViewOrders() {
            string userId = _appUser.GetUserId(User);
            if (userId != null)
            {

                List<Orders> orders = _ordersRepo.GetAll(userId);
                return View(orders);

            }
            else {
                return View();
            }
        }

        [HttpGet]
        public IActionResult CancelOrder(int orderId) {

            Orders orders = _ordersRepo.GetById(orderId);
            if (orders != null)
            {
                Products products = _productRepo.GetById(orders.ProductId);
                if (products != null)
                {
                    products.Quantity += orders.Quantity;
                    _productRepo.Update(products);
                    _productRepo.Save();
                }
                _ordersRepo.Delete(orders);
                _ordersRepo.Save();
                return Json(new { success = true });
            }
            else {
                return Json(new { success = false });
            }

        }


    }
}