using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CocktailCookbook.Data;
using CocktailCookbook.Models;
using CocktailCookbook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CocktailCookbook.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;
        public AdminController(RoleManager<IdentityRole> roleManager, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            _context = context;
            this.userManager = userManager;
        }
    
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rolesList = await _context.Roles.ToListAsync();
            var user = await userManager.GetUserAsync(User);
            var inRole = await userManager.GetRolesAsync(user);
            //var isInRole = await roleManager.AddClaimAsync(rolesList[1], new Claim("", ""));
            var result = await userManager.IsInRoleAsync(user, "Bar");
            //need to select all users and their associated tol
            ViewBag.ExistingRoles = await roleManager.Roles.ToListAsync();
            List<UsersRolesViewModel> list = new List<UsersRolesViewModel>();

            var x = await _context.Users.ToListAsync();
            foreach (var u in x)
            {
                list.Add(new UsersRolesViewModel
                {
                    UserId = u.Id,
                    Name = u.UserName,
                    AssignedRoles = string.Join(",", await userManager.GetRolesAsync(u))
                });
                
            }
         
            //gets a list of users and their associated roles
            //var usersWithRoles = (from user in _context.Users
            //                      select new
            //                      {
            //                          UserId = user.Id,
            //                          Username = user.UserName,
                                      
            //                          RoleNames = (from userRole in _context.UserRoles
            //                                       join role in _context.Roles on userRole.RoleId
            //                                       equals role.Id
            //                                       select role.Name).ToList()
            //                      }).ToList().Select(p => new UsersRolesViewModel()

            //                      {
            //                          UserId = p.UserId,
            //                          Name = p.Username,
                                      
            //                          AssignedRoles = string.Join(",", p.RoleNames)
            //                      });
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {

            var uR = await _context.Roles.ToListAsync();
            List<CreateRoleViewModel> crvm = new List<CreateRoleViewModel>();
            foreach (var r in uR)
            {
                var model = new CreateRoleViewModel { Name = r.Name };
                crvm.Add(model);
            }
            ViewBag.Roles = crvm;
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> CreateRole(CreateRoleViewModel role)
        {
           //create role
            var roleExist = await  roleManager.RoleExistsAsync(role.Name);
            if (!roleExist)
            {
                var newRole = new IdentityRole(role.Name);
                var result = await roleManager
                .CreateAsync(newRole);
                //create role claim
               //var claim = roleManager.AddClaimAsync(newRole, new Claim(role.Name, role.Name));
            }
            //TODO: May need to remove above claims. still doesn't seem to be working
            return RedirectToAction(nameof(CreateRole));
        }

        [HttpGet]

        public IActionResult AssignRoles()
        {
           var roles =  roleManager.Roles.ToListAsync();         

            //needs a list of staff, list of roles
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssignRoles([Bind("UserId,Name,AssignedRoles")] UsersRolesViewModel ur)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == ur.UserId);
                var userRoles = await userManager.GetRolesAsync(user);
                List<string> roles = ur.AssignedRoles.Split(",").ToList();
                foreach (var role in roles)
                {

                    bool exists = await roleManager.RoleExistsAsync(role);
                    if (exists)
                    {

                        var result = await userManager.AddToRolesAsync(user, roles);

                    }
                }



                return RedirectToAction(nameof(Index));

            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
            
        }



        [HttpPost]
        public async Task<IActionResult> RemoveRoles([Bind("UserId,Name,AssignedRoles")] UsersRolesViewModel ur)
        {
            
           var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == ur.UserId);
            var userRoles = await userManager.GetRolesAsync(user);
            List<string> roles = ur.AssignedRoles.Split(",").ToList();
                    
           IdentityResult result = await userManager.RemoveFromRolesAsync(user, roles);
                       
         
            return RedirectToAction(nameof(Index));
        }       
    }
}
