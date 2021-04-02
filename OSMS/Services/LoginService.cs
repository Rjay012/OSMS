using Microsoft.AspNetCore.Mvc;
using OSMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LoginModel = OSMS.Models.LoginModels.LoginModel;

namespace OSMS.Services
{
    public class LoginService
    {
        private readonly OSMSContext _context;
        
        public LoginService(OSMSContext context)
        {
            _context = context;
        }

        public bool isLogin(string user, [Bind]LoginModel loginModel)
        {
            bool isLogin = _context.Students
                                   .Where(s => s.StudentID == loginModel.UserID && s.Password == loginModel.Password)
                                   .Any();
            if(user == "Instructor")
            {
                isLogin = _context.Instructors
                                  .Where(i => i.InstructorID == loginModel.UserID && i.Password == loginModel.Password)
                                  .Any();
            }

            return isLogin;
        }
    }
}
