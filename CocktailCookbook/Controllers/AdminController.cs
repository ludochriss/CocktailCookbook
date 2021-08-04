using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CocktailCookbook.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;

        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            
            return View();

        }

        //[HttpPost]
        //public IActionResult CreateRole()
        //{


        //}

    }
}
