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

            var c = await _context.Cocktail.ToListAsync();
            var jobs = await _context.Tasks.Include(j=>j.Creator).ToListAsync();
            //TODO: retrieve job/completed job information in single database call 
            List<Job> currentJobs = new List<Job>();
            List<CompletedJob> completedJobs = new List<CompletedJob>();
            if (jobs.Count() > 0)
            {
                //Object relational mapping in EFCore is not supporterd for inherited types, 

                List<Cocktail> cocktails = new List<Cocktail>();
                foreach (var j in jobs)
                {

                    if (j.GetType() != typeof(CompletedJob))
                    {
                        currentJobs.Add(j);
                    }
                    else
                    {
                        completedJobs.Add((CompletedJob)j);
                    }
                }

            }
         
           

            ViewBag.CompletedJobs = completedJobs as IEnumerable<CompletedJob>;
            ViewBag.Cocktails = c as IEnumerable<Cocktail>;


            return View(currentJobs);
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
