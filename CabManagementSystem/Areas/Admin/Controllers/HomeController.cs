using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CabManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        ////[Route("[area]/login")]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var user = await userManager.FindByEmailAsync(model.Email);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError("", "Invalid details.");
        //        return View(model);
        //    }

        //    var res = await signInManager.PasswordSignInAsync(user, model.Password, true, true);

        //    if (res.Succeeded && model.Email == "admin@admin.com")
        //    {
        //        //return RedirectToAction("Index", "Home", new {Area=""});
        //        return Redirect("/");
        //    }
        //    ModelState.AddModelError("", "Invalid email / password");
        //    return View(model);
        //}

        [HttpGet]
        public async Task<IActionResult> ViewBookingsAdmin()
        {
            return View(db.Bookings.ToList());
        }

        //[HttpPost]
        //public async Task<IActionResult> Confirmation(int id)
        //{
        //    var user = await userManager.GetUserAsync(User);
        //    var cabDriver = await db.Drivers.FirstOrDefaultAsync(m => m.DriverId == user.Id);
        //    //_db.Bookings.Where(book => book.ApplicationUserId == model.ApplicationUserId).FirstAsync().Result.DriverConfirmed = true;
        //    //model.DriverId = user.Id;
        //    //model.DriverConfirmed = true;

        //    var booking = await db.Bookings.FirstOrDefaultAsync(m => m.Id == id);

        //    booking.DriverId = cabDriver.DriverId;
        //    booking.DriverConfirmed = true;
        //    //db.Bookings.Where(book => booking.UserId == model.ApplicationUserId).FirstAsync().Result.DriverConfirmed = true;
        //    await db.SaveChangesAsync();
        //    return View(user);

        //}

        [HttpGet]
        public async Task<IActionResult> ViewUsersAdmin()
        {
            return View(db.ApplicationUsers.ToList());
        }



        [HttpGet]
        public async Task<IActionResult> ViewDriversAdmin()
        {
            return View(db.Drivers.ToList());
        }


    }
}
