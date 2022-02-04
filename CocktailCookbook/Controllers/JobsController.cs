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
using CocktailCookbook.ViewModels;

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

        //Jobs controller is currently under construction and will be finished soon.

        //TASKS CAN BE CREATED HOWEVER RECURRING TASKS CANNOT BE SET YET.
        public async Task<IActionResult> Index()
        {
            var allJobs = await _context.Tasks.Include(t=>t.Department).Include(t=>t.Department).ToListAsync();
            var j = allJobs.OfType<RecurringTask>().ToList();
            List<RecurringTask> hourly = new List<RecurringTask>();
            List<RecurringTask> daily = new List<RecurringTask>();
            List<RecurringTask> weekly = new List<RecurringTask>();
            List<Job> pendingTasks = new List<Job>();

            foreach (var k in allJobs)
            {
                if (k.GetType() !=typeof(CompletedJob))
                {
                    pendingTasks.Add(k);
                }
            }
            foreach (var job in j)
            {
                if (job.RecursHourly == true)
                {
                    hourly.Add(job);
                    
                }
                else if (job.RecursDaily == true)
                {
                    daily.Add(job);
                   
                }
                else if (job.RecursWeekly == true)
                {
                    weekly.Add(job);
                }
            
            }
            

            List<CompletedJob> compJobs = await _context.Tasks.OfType<CompletedJob>().ToListAsync();
            var vm = new JobsIndexViewModel
            {
                DailyJobs = daily,
                HourlyJobs = hourly,
                WeeklyJobs = weekly,
                CompletedJobs = compJobs,
                PendingJobs = pendingTasks
            };

            return View(vm);
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
        public async Task<IActionResult> Create()
        {           
           //create dropdown list of departments
            var dropdown = new List<SelectListItem> {
               new SelectListItem{Value="No Department", Text = "None" , Selected = true}
              };
            var departments = await _context.Departments.ToListAsync();

            foreach (var x in departments)
            {
                dropdown.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            }
            ViewBag.Departments = dropdown;

            return View();
        }



        //modifies existing tasks to become recurrant tasks
        //brings the list of tasts and tabs for the user to view different kinds of tasks.
        [HttpGet]
        public async Task<IActionResult> TaskManagement()
        {
            var jobs = await _context.Tasks.Include(t=>t.Department).ToListAsync();
            List<Job> jobsList = new List<Job>();
            List<RecurringTask> recurringList = new List<RecurringTask>();
            foreach (var j in jobs)
            {
                if (j.GetType() != typeof(RecurringTask))
                {
                    jobsList.Add(j);
                }
                else
                {
                    recurringList.Add((RecurringTask)j);
                }
            }

            var vm = new TaskManagementViewModel();
            vm.Jobs = jobsList;
            vm.RecurringTasks = recurringList;
           
            return View(vm);
        }
 
        public List<SelectListItem> GetHourlyFrequencyList()
        {
            //should not hard code for obvious reasons, can be done better.
            
            var hourlyFrequency = new List<SelectListItem>
            {
                 new SelectListItem{Value="0", Text = "No Hourly Recurrance"},
                new SelectListItem{Value="1", Text = "1 Hours"},
                new SelectListItem{Value="2", Text = "2 Hours"},
                new SelectListItem{Value="3", Text = "3 Hours"},
                new SelectListItem{Value="4", Text = "4 Hours"},
                new SelectListItem{Value="5", Text = "5 Hours"},
                new SelectListItem{Value="6", Text = "6 Hours"},
                new SelectListItem{Value="7", Text = "7 Hours"},
                new SelectListItem{Value="8", Text = "8 Hours"},
                new SelectListItem{Value="9", Text = "9 Hours"},
                new SelectListItem{Value="10", Text = "10 Hours"},
                new SelectListItem{Value="11", Text = "11 Hours"},
                new SelectListItem{Value="12", Text = "12 Hours"}
          
            };

            return hourlyFrequency;
        }
            
        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Department,Name,TaskDescription")] CreateJobViewModel job)
        {

            //Refactor required to database schema at this point will need to add an interface to manage user information as 
            //neglected to manage the user using the in-built user management services
            
            bool parseSuccessful = int.TryParse(job.Department, out  int deptId);           
            //int deptId = Convert.ToInt32(job.Department);
            var user = await _um.GetUserAsync(User);       
                       
            //checks if signed in for naming 
            if (_sm.IsSignedIn(User))
            {
                job.Creator = await _context.Staff.FirstOrDefaultAsync(u => u.UserId == user.Id);
            }
            //if user is not signed in, gives unknown user, shouldn't get this far without it anyway, but just incase
            else
            {
                job.Creator = new User { NickName = "Unknown User", Email = "Unknown User" };
            }
        
            //checks model state
            if (ModelState.IsValid)
            {
                var newJob = new Job
                {
                    Id = job.Id,
                    Name = job.Name,
                    
                    TaskDescription = job.TaskDescription,
                    Creator = job.Creator,
                    TimeCreated = DateTime.Now
                   
                };
                if (parseSuccessful)
                {
                     newJob.Department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == deptId);
                }
                else
                {
                    newJob.Department = new Department
                    {
                        Name = "None"
                    };
                }
                _context.Add(newJob);
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
                return RedirectToAction(nameof(Index),"Home");
            }
            return View(job);
        }

        //gets recurring job page.
        [HttpGet]
        public async Task<IActionResult> MakeRecurringJob(int? id)
        {
            //refactor this code to get all information in a single call, needs to be condensed.
            if (id == null)
            {
                return NotFound();
            }
            //bring the department that the job is assigned to
            var job = await _context.Tasks.Include(d=>d.Department).FirstOrDefaultAsync(t=>t.Id == id);
            if (job == null)
            {
                return NotFound();
            }
            var hourList = GetHourlyFrequencyList();
            ViewBag.HourList = hourList;

            var newJob = new MakeRecurringViewModel
            {
                Id = job.Id,
                Name = job.Name,
                TaskDescription = job.TaskDescription,
                Department = job.Department,
                Creator = job.Creator
                
              
                

            };
            return View(newJob);

           
        }

        //method not finished

        [HttpPost]
        public async Task<IActionResult> MakeRecurringJob(int? id, [Bind("Id,Name,TaskDescription,RecursDaily,DailyTime,RecursHourly,RecursWeekly,HourlyFrequency,Creator")]
        RecurringTask recurringJob)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            if (ModelState.IsValid)
            {
                var existingJob = await _context.Tasks.Include(j=>j.Department).FirstOrDefaultAsync(t=>t.Id ==recurringJob.Id);
                recurringJob.TimeCreated = DateTime.Now;
                //Will need to get the daily time to be at session open
                recurringJob.DailyTime = DateTime.Now;
                try
                {                 
                        _context.Tasks.Remove(existingJob);
                        _context.RecurringTasks.Add(recurringJob);
                        await _context.SaveChangesAsync();                    
                  }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(recurringJob.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(TaskManagement), "Jobs");

            }
            return View(recurringJob);
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
