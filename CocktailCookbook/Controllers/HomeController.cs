using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CocktailCookbook.Models;
using CocktailCookbook.Data;
using Microsoft.EntityFrameworkCore;
using CocktailCookbook.ViewModels;

namespace CocktailCookbook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //TODO: remove the ADD role button and develop an administration page that has all of the functions for role assignment and removal.
            //TODO: add the ability to store images for the cocktail cards.

            var c = await _context.Cocktail.Include(c=>c.Glassware).ToListAsync();
            var Tasks = await _context.Tasks.Include(j=>j.Creator).ToListAsync();
            //TODO: retrieve Task/completed Task information in single database call 
            List<Models.Task> currentTasks = new List<Models.Task>();
            List<CompletedTask> completedTasks = new List<CompletedTask>();
            if (Tasks.Count() > 0)
            {
                //Object relational mapping in EFCore 3.1 is not supporterd for inherited types, 

                List<Cocktail> cocktails = new List<Cocktail>();
                foreach (var j in Tasks)
                {
                    
                    if (j.GetType() != typeof(CompletedTask))
                    {
                        currentTasks.Add(j);
                    }
                    else
                    {
                        completedTasks.Add((CompletedTask)j);
                    }
                }

            }      
            var vm = new HomePageViewModel
            {
                Cocktails = c,
                CompletedTasks = completedTasks,
                Tasks = Tasks
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
