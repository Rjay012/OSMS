using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OSMS.Models;
using OSMS.Models.LoginModels;
using OSMS.Models.RegistrationModels;

namespace OSMS.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly OSMSContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, OSMSContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ShowRegistrationForm([Bind]RegistrationModel registrationModel)
        {
            registrationModel.StandardList = GetStandard().ToList();
            return PartialView("Partials/Modals/_Registration", registrationModel);
        }

        private IEnumerable<SelectListItem> GetStandard()
        {
            List<SelectListItem> item = new List<SelectListItem>();
            List<Standard> standard = _context.Standards.ToList();
            foreach(var s in standard)
            {
                item.Add(new SelectListItem
                {
                    Value = s.StandardID.ToString(),
                    Text = s.StandardName
                });
            }
            return item;
        }

        public IActionResult ShowLoginModalForm()
        {
            return PartialView("Partials/Modals/_Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind]RegistrationModel registrationModel)
        {
            if(ModelState.IsValid)
            {
                Student student = new Student();
                student.StudentID = registrationModel.StudentID;
                student.StudentName = registrationModel.StudentName;
                student.StandardID = registrationModel.StandardID;
                student.Password = registrationModel.ConfirmPassword;
                student.RoleID = 4;

                _context.Add(student);
                await _context.SaveChangesAsync();

                //login
                await SetLoginCredentials(new LoginModel { UserID = registrationModel.StudentID, Role = "Student" });
                return RedirectToAction("Index", "Student");
            }
            return View(nameof(Index));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind]LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                bool islogin = _context.Instructors
                                       .Where(i => i.InstructorID == loginModel.UserID && i.Password == loginModel.Password)
                                       .Any();
                loginModel.Role = "Instructor";
                if (islogin == false)
                {
                    islogin = _context.Students
                                      .Where(s => s.StudentID == loginModel.UserID && s.Password == loginModel.Password)
                                      .Any();
                    loginModel.Role = "Student";
                }

                if(islogin == true)
                {
                    await SetLoginCredentials(loginModel);
                    switch(loginModel.Role)
                    {
                        case "Instructor":
                            return RedirectToAction("Index", "Instructor");
                        case "Student":
                            return RedirectToAction("Index", "Student");
                    }
                }

            }
            return View("Index");
        }

        private async Task<IActionResult> SetLoginCredentials([Bind("UserID,Role")] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                List<Claim> loginCredentials = new List<Claim>()
                    {
                        new Claim("UserID", loginModel.UserID),
                        new Claim(ClaimTypes.Role, loginModel.Role)
                    };
                var userIdentity = new ClaimsIdentity(loginCredentials, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                await HttpContext.SignInAsync(userPrincipal);

                return Ok();
            }

            return NoContent();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
