using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CocktailCookbook.Data;
using CocktailCookbook.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace CocktailCookbook.Controllers
{
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _um;
        private readonly SignInManager<IdentityUser> _sm;
        public JobsController(ApplicationDbContext context, UserManager<IdentityUser> um, SignInManager<IdentityUser> sm)
        {
            _context = context;
            _um = um;
            _sm = sm;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {

          
          
            ViewBag.CompleteJobs =await _context.Tasks.OfType<CompletedJob>().ToListAsync();

         
            return View(await _context.Tasks.Include(t=>t.Creator).ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TaskDescription")] Job job)
        {

            //Refactor required to database schema at this point will need to add an interface to manage user information as 
            //neglected to manage the user using the in-built user management services

            var user = await _um.GetUserAsync(User);
           

            job.TimeCreated = DateTime.Now;
            if (_sm.IsSignedIn(User))
            {
                
                job.Creator =await  _context.Staff.FirstOrDefaultAsync(u => u.UserId == user.Id);
               
            }
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }


        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Tasks.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TimeCreated,TaskDescription")] Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }
            //find the current users id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //match with database 
            job.Creator =await _context.Staff.FirstOrDefaultAsync(s => s.UserId == userId);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
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
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> MarkComplete(int? id)

        { 
            //find the user Id for the signed in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);                  
            var j = await _context.Tasks.FirstOrDefaultAsync(j => j.Id == id);

            //add the completed job to the database and remove the task from the tasks list, save changes.

            //search database for the staff members details
            var markedCompleteBy = await _context.Staff.FirstOrDefaultAsync(s => s.UserId == userId);
            //draft new completed job, inherits from job
            var c = new CompletedJob
            {
                Id = j.Id,
                TaskDescription = j.TaskDescription,
                Creator = j.Creator,
                Name = j.Name,
                TimeCompleted = DateTime.Now,
                TimeCreated = j.TimeCreated,
                MarkedCompleteBy = markedCompleteBy

            };
            _context.Tasks.Remove(j);
            _context.CompletedJobs.Add(c);
         
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index), "Home");
        }

        //returns completed job to incomplete state.
        public async Task<IActionResult> MarkedInError(int id)

        {
            var j = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            var nJ = new Job { 
                Id = j.Id,
                Name = j.Name,
                TaskDescription = j.TaskDescription,
                Creator = j.Creator,
                TimeCreated = j.TimeCreated
            };
            _context.Remove(j);
            _context.Add(nJ);
            
           await _context.SaveChangesAsync();
            

            return RedirectToAction(nameof(Index),"Home");
        }
  

        private bool JobExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
