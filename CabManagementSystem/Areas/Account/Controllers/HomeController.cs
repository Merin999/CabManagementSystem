using CabServiceManagement.Models;
using Microsoft.AspNetCore.Identity;


namespace CabManagementSystem.Areas.Account.Controllers
{
    [Area("Account")]
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

        [HttpGet]
        //[Route("[area]/index")]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Route("[area]/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("[area]/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
                //if (!ModelState.IsValid) return View(model);
             
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid Details");
                    return View(model);
                }

                var res = await signInManager.PasswordSignInAsync(user, model.Password, true, true);
                var role = userManager.GetRolesAsync(user);

                if (res.Succeeded)
                {

                    if (role.Result.Contains("Admin"))
                    {
                        Console.WriteLine("Admin here");
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }
                    if (role.Result.Contains("User"))
                    {
                        Console.WriteLine("User is here");
                        return RedirectToAction("Index", "Home", new { Area = "User" });
                    }

                    return RedirectToAction("index", "Home", new { Area = "Driver" });
                }



                return View(model);
            }


            //if (!ModelState.IsValid)
            //    return View(model);

            //var user = await userManager.FindByEmailAsync(model.Email);
            //if (user == null)
            //{
            //    ModelState.AddModelError("", "Invalid details.");
            //    return View(model);
            //}

            //var res = await signInManager.PasswordSignInAsync(user, model.Password, true, true);

            //if (res.Succeeded)
            //{
            //    //return RedirectToAction("Index", "Home", new {Area=""});
            //    return Redirect("/");
            //}
            //ModelState.AddModelError("", "Invalid email / password");
            //return View(model);


            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}



            //var user = await userManager.FindByEmailAsync(model.Email);
            //if (user == null)
            //{
            //    ModelState.AddModelError("", "Invalid Email");
            //    return View(model);
            //}
            //var res = await signInManager.PasswordSignInAsync(user, model.Password, true, true);
            //if (res.Succeeded)
            //{
            //    var role = await userManager.IsInRoleAsync(user, "Admin");
            //    if (role)
            //    {
            //        return RedirectToAction(nameof(Index), "Home", new { Area = "Admin" });
            //    }
            //    else if (User.IsInRole("Driver"))
            //    {
            //        return RedirectToAction(nameof(Index), "Home", new { Area = "Driver" });
            //    }
            //    else
            //    {
            //        return RedirectToAction(nameof(Index), "Home", new { Area = "" });
            //    }



            //    //return Redirect("/");
            //}
            //ModelState.AddModelError("", "Invalid Email/Password");
            //return View(model);
        

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                //Roles = model.Roles,
                UserName = Guid.NewGuid().ToString().Replace("-", ""),
            };
            var res = await userManager.CreateAsync(user, model.Password);

            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                return RedirectToAction(nameof(Login));
            }
            foreach (var item in res.Errors)
            {
                ModelState.AddModelError("", item.Description);

            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("/");
        }

        public async Task<IActionResult> GenerateData()
        {
            await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            await roleManager.CreateAsync(new IdentityRole() { Name = "User" });
            await roleManager.CreateAsync(new IdentityRole() { Name = "Driver" });

            var users = await userManager.GetUsersInRoleAsync("Admin");
            if (users.Count == 0)
            {
                var appUser = new ApplicationUser()
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@admin.com",
                    UserName = "admin",
                };
                var res = await userManager.CreateAsync(appUser, "Pass@123");
                await userManager.AddToRoleAsync(appUser, "Admin");
            }
            return Ok("Data generated");
        }



        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var signeduser = await userManager.GetUserAsync(User);
            var user = await userManager.FindByEmailAsync(signeduser.Email);
            return View(new EditViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);
            var signeduser = await userManager.GetUserAsync(User);
            var user = await userManager.FindByEmailAsync(signeduser.Email);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            await userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete()
        {
            var signeduser = await userManager.GetUserAsync(User);
            var user = await userManager.FindByEmailAsync(signeduser.Email);
            await signInManager.SignOutAsync();
            await userManager.DeleteAsync(user);
            return Redirect("/");
        }


        [HttpGet]
        public IActionResult Booking()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Booking(BookingViewModel model)
        {
            if (model.From == model.To)
            {
                ModelState.AddModelError(nameof(model.To), "Invalid destination");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            db.Bookings.Add(new Booking()
            {
                To = model.To,
                From = model.From,
                Date = model.Date,

            });
            await db.SaveChangesAsync();



            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        [HttpGet]
        public IActionResult DriverRegister()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DriverRegister(RegisterViewModelDriver model)
        {
            var uid = "";
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = Guid.NewGuid().ToString().Replace("-", ""),
            };
            var res = await userManager.CreateAsync(user, model.Password);

            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Driver");
                uid = user.Id;
                //return RedirectToAction(nameof(Login));
            }
            db.Drivers.Add(new Models.Driver()
            {
                City = model.City,
                //Dob = model.Dob,
                LicenceNumber = model.LicenceNumber,
                LicenceValidity = model.LicenceValidity,
                CabType = model.CabType,
                ModelName = model.ModelName,
                NoOfSeats = model.NoOfSeats,
                DriverId = uid

            });
            
            //if (res.Succeeded)
            //{
            //    //await userManager.AddToRoleAsync(user, "User");
            //    return RedirectToAction(nameof(Login));
            //}
            //foreach (var item in res.Errors)
            //{
            //   

            //}
            await db.SaveChangesAsync();
            //ModelState.AddModelError("","Error");
            return RedirectToAction(nameof(Login));
        }

    }
}
