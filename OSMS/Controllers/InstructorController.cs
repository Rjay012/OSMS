using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSMS.Models;

namespace OSMS.Controllers
{
    [Authorize]
    public class InstructorController : Controller
    {
        [Authorize(Roles = "Instructor")]
        public IActionResult Index()
        {
            return View();
        }
    }
}