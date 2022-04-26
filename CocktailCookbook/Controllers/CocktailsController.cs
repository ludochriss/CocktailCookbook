using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CocktailCookbook.Data;
using CocktailCookbook.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using CocktailCookbook.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CocktailCookbook.Controllers
{
    //[Authorize(Policy ="UserOnly")]
    public class CocktailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        //web host evnironment seems to be a file path helper for the .Net runtime
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CocktailsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Cocktails
        public async Task<IActionResult> Index()
        {
            //TODO: add buttons to cocktail cards on home screen
            //TODO: change the default tabs on the 
           
            return View(await _context.Cocktail.ToListAsync());
        }

        // GET: Cocktails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocktail == null)
            {
                return NotFound();
            }
                       
            return View(cocktail);
        }

        // GET: Cocktails/Create
        public IActionResult Create()
        {         
            //after select list make pass the glassware names in
            var vm = new CreateCocktailViewModel();
            var glasses = _context.Glassware.ToList();

            ViewBag.Glasses = new SelectList(glasses, "Id", "Name");
       
            

          
            return View(vm);
        }

        // POST: Cocktails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //glassware return string that represents id of current glassware object
        public async Task<IActionResult> Create([Bind("Id,Name,Method,Garnish,Description,Ingredients,Glassware,Photo")] CreateCocktailViewModel cocktail)
        {
            if (ModelState.IsValid)
            {
                //string uniqueFileName = UploadedFile(cocktail);
                var newCocktail = new Cocktail
                {
                    Id = cocktail.Id,
                    Ingredients = cocktail.Ingredients,
                    Method = cocktail.Method,                    
                    Garnish = cocktail.Garnish, 
                    Creator = cocktail.Creator,
                    Description = cocktail.Description,
                    Name = cocktail.Name                   
                 };
                if (cocktail.Glassware != null && Int32.TryParse(cocktail.Glassware, out int result)== true)
                {
                   newCocktail.Glassware = _context.Glassware.FirstOrDefault(g => g.Id ==result);
                }
                if (cocktail.Photo != null)
                {
                    newCocktail.Photo = newCocktail.ConvertImageToFilePath(cocktail.Photo, _webHostEnvironment);
                }
                _context.Add(newCocktail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cocktail);
        }
        // GET: Cocktails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cocktail = await _context.Cocktail.FindAsync(id);
            var cocktailVm = new EditCocktailViewModel
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                Method = cocktail.Method,
                Ingredients = cocktail.Ingredients,
                
                Description = cocktail.Description,
                Creator = cocktail.Creator,
                Garnish = cocktail.Garnish,
                ImagePath = cocktail.Photo,
                Photo = null,
                
            };
           

            if (cocktail == null)
            {
                return NotFound();
            }
            var glasses = _context.Glassware.ToList();

            ViewBag.Glasses = new SelectList(glasses, "Id", "Name");
            return View(cocktailVm);
        }
        // POST: Cocktails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //using create cocktail to minimise the need to create an extra view model to deal with the photo handling
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Method,Glassware,Garnish,Description,Ingredients,Photo,ImagePath")] EditCocktailViewModel cocktailVm)
        {
            //check if the entity is valid
            if (id != cocktailVm.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
                {
                //string uniqueFileName= UploadedFile(cocktailVm);
                var cocktail = new Cocktail
                    {
                        Id = cocktailVm.Id,
                        Name = cocktailVm.Name,
                        Method = cocktailVm.Method,
                        Description = cocktailVm.Description,                        
                        Ingredients = cocktailVm.Ingredients,
                        Garnish = cocktailVm.Garnish,
                        Creator = cocktailVm.Creator
                    };
                if (cocktailVm.Photo != null)
                {
                    string uniqueFileName = cocktail.ConvertImageToFilePath(cocktailVm.Photo, _webHostEnvironment);
                    cocktail.Photo = uniqueFileName;
                }
                else if (cocktailVm.Photo == null)
                {
                    cocktail.Photo = cocktailVm.ImagePath;
                }
                
              
                if (cocktail.Glassware != null && Int32.TryParse(cocktailVm.Glassware, out int result) == true)
                {
                    cocktail.Glassware = _context.Glassware.FirstOrDefault(g => g.Id == result);
                }
                try
                {
                        _context.Update(cocktail);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CocktailExists(cocktailVm.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            
            return View(cocktailVm);
        }

        // GET: Cocktails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocktail == null)
            {
                return NotFound();
            }

            return View(cocktail);
        }

        // POST: Cocktails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cocktail = await _context.Cocktail.FindAsync(id);
            _context.Cocktail.Remove(cocktail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddIngredientsToCocktail()
        {
            await _context.SaveChangesAsync();
            return View("_IngredientPartial");
        }
        private bool CocktailExists(int id)
        {
            return _context.Cocktail.Any(e => e.Id == id);
        }
        private string UploadedFile(CreateCocktailViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
      
    }
}
