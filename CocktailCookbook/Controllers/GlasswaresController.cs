using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CocktailCookbook.Data;
using CocktailCookbook.Models;
using CocktailCookbook.ViewModels.Glasswares;
using Microsoft.AspNetCore.Hosting;

namespace CocktailCookbook.Controllers
{
    public class GlasswaresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _whe;

        public GlasswaresController(ApplicationDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _whe = webHostEnvironment;
            _context = context;
        }

        // GET: Glasswares
        public async Task<IActionResult> Index()
        {
            return View(await _context.Glassware.ToListAsync());
        }

        // GET: Glasswares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glassware = await _context.Glassware
                .FirstOrDefaultAsync(m => m.Id == id);
            if (glassware == null)
            {
                return NotFound();
            }

            return View(glassware);
        }

        // GET: Glasswares/Create
        public IActionResult Create()
        {
            var vm = new CreateGlasswaresViewModel();


            return View(vm);
        }

        // POST: Glasswares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImagePath")] EditGlasswaresViewModel glassware)
        {
            if (ModelState.IsValid)
            {
                var model = new Glassware
                {
                    Id = glassware.Id,
                    Name = glassware.Name
                   
                };
              
               model.ImagePath = model.ConvertImageToFilePath(glassware.Photo, _whe);
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(glassware);
        }

        // GET: Glasswares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glassware = await _context.Glassware.FindAsync(id);
            if (glassware == null)
            {
                return NotFound();
            }
            return View(glassware);
        }

        // POST: Glasswares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImagePath")] Glassware glassware)
        {
            if (id != glassware.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(glassware);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlasswareExists(glassware.Id))
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
            return View(glassware);
        }

        // GET: Glasswares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glassware = await _context.Glassware
                .FirstOrDefaultAsync(m => m.Id == id);
            if (glassware == null)
            {
                return NotFound();
            }

            return View(glassware);
        }

        // POST: Glasswares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var glassware = await _context.Glassware.FindAsync(id);
            _context.Glassware.Remove(glassware);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GlasswareExists(int id)
        {
            return _context.Glassware.Any(e => e.Id == id);
        }
    }
}
