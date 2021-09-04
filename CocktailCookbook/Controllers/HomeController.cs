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
        

            //#TODO: make a view model that has a list of everything
            //#TODO: combine all users based on role and items in the database based on role so that they have a linking table which allows includ functionality

            var c = await _context.Cocktail.ToListAsync();
            var jobs = await _context.Tasks.ToListAsync();
            List<Job> currentJobs = new List<Job>();
            List<CompletedJob> completedJobs = new List<CompletedJob>();
            //Object relational mapping in EFCore is not supporterd for inherited types, perhaps this would be easier elsewhere but for now I remove after querying the data base
            //organised by table per heirarchy and requires sort for completed vs non completed
            //will need to create viewmodel with list of cocktails, completed and incomplete jobs
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

            //var home = from c in _context.Tasks
            //           join  j in _context.Cocktails
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
