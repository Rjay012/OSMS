using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OSMS.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        [Authorize(Roles = "Student")]
        public IActionResult Index()
        {
            return View();
        }
    }
}