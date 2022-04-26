using CocktailCookbook.Data;
using CocktailCookbook.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public DepartmentsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
           var dept = _dbContext.Departments
                .Include(t => t.Tasks)
                .ToList();
            return View(dept);
        }
    }
}
