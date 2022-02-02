using CocktailCookbook.Data;
using CocktailCookbook.Models;
using CocktailCookbook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Controllers
{
    [Authorize(Roles = "Administrator")]
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
            // loads the index page which contains the most information for a single request

            //--EXPLORE OPTIONS TO REFACTOR FOR EFFICIENCY
            // ---perhaps this is getting too chunky---
            // separates out roles 
            var rolesList = await _context.Roles.ToListAsync();
            var user = await userManager.GetUserAsync(User);
            //var inRole = await userManager.GetRolesAsync(user);
            
           
            List<UsersRolesViewModel> list = new List<UsersRolesViewModel>();
            //gets a list of users
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
            var vm = new AdminIndexViewModel();

            //gets a list of roles
            var vmRoles = new List<string>();
            foreach (var r in rolesList)
            {
                vmRoles.Add(r.Name);
            }

            //gets list of departments
            var deptList = await _context.Departments.ToListAsync();

            //adds all users, associated roles and departments to viewmodel

            vm.Roles = vmRoles;
            vm.UserRoles = list;
            vm.Departments = deptList;
              //aspects of the view model are then passed as parameters to partial views within the view --Not sure best practice--    
            
            return View(vm);
        }


       //Gets the role view for the user 
        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            //gets all current roles
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

        //creates a new role from user input
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

        //gets the assings roles page
        [HttpGet]
        public IActionResult AssignRoles()
        {
           var roles =  roleManager.Roles.ToListAsync();         

            //needs a list of staff, list of roles
            
            return View();
        }
        //assigns roles based on user selection, binds some attributes and assigns others manually
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
        //gets create department view
        [HttpGet]
        public IActionResult CreateDepartment()
        {
            return View();
        }

        //assigns new department based on user input
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([Bind("Id,Name")] Department dept)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(dept);
                await _context.SaveChangesAsync();
            }
          
            //redirects to page if model is invalid
            else
            {
                return RedirectToAction(nameof(CreateDepartment));
            }

            return RedirectToAction(nameof(Index), "Admin");
        }


        //removes roles from the list of roles
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
