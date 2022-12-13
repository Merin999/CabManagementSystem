using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CabManagementSystem.Areas.Driver.Controllers
{
    [Area("Driver")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(ApplicationDbContext db,
            //UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            //this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }


        [HttpGet]
        public async Task<IActionResult> DriverEdit(int id)
        {
            var driver = await db.ApplicationUsers.FindAsync(id);
            if (driver == null)
                return NotFound();

            return View(new DriverEditViewModel()
            {
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                Email = driver.Email,
                
            });
        }

        [HttpPost]
        public async Task<IActionResult> DriverEdit(int id, RegisterViewModelDriver model)
        {
            var driver = await db.ApplicationUsers.FindAsync(id);
            if (driver == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            driver.FirstName = model.FirstName;
            driver.LastName = model.LastName;
            driver.Email = model.Email;
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var driver = await db.Drivers.FindAsync(id);
            if (driver == null)
                return NotFound();

            db.Drivers.Remove(driver);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UserConfirm(int id)
        {
            return View(db.Bookings.ToList());
       
            var user = await userManager.GetUserAsync(User);
            var cabDriver = await db.Drivers.FirstOrDefaultAsync(m => m.DriverId == user.Id);
            //_db.Bookings.Where(book => book.ApplicationUserId == model.ApplicationUserId).FirstAsync().Result.DriverConfirmed = true;
            //model.DriverId = user.Id;
            //model.DriverConfirmed = true;
            
            var booking = await db.Bookings.FirstOrDefaultAsync(m => m.Id == id);

            booking.DriverId = cabDriver.DriverId;
            booking.DriverConfirmed = true;

            await db.SaveChangesAsync();
            return View(user);
            

        }
    }
}
