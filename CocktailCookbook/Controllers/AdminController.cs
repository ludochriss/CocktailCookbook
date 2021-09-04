using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailCookbook.Data;
using CocktailCookbook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CocktailCookbook.Controllers
{
    public class AdminController : Controller
    {
        //private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _context;
        public AdminController(/*RoleManager<IdentityRole> roleManager,*/ ApplicationDbContext context)
        {
            //this.roleManager = roleManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            
            
            return View();

        }

        [HttpPost]
        public IActionResult CreateRole(User user)
        {

            return View();
        }

    }
}
