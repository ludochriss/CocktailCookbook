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
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _um;
        private readonly SignInManager<IdentityUser> _sm;
        public TasksController(ApplicationDbContext context, UserManager<IdentityUser> um, SignInManager<IdentityUser> sm)
        {
            _context = context;
            _um = um;
            _sm = sm;
        }

        // GET: Tasks

        //Tasks controller is currently under construction and will be finished soon.

        //TASKS CAN BE CREATED HOWEVER RECURRING TASKS CANNOT BE SET YET.
        public async Task<IActionResult> Index()
        {
            var allTasks = await _context.Tasks.Include(t=>t.Department).Include(t=>t.Department).ToListAsync();
            var j = allTasks.OfType<RecurringTask>().ToList();
            List<RecurringTask> hourly = new List<RecurringTask>();
            List<RecurringTask> daily = new List<RecurringTask>();
            List<RecurringTask> weekly = new List<RecurringTask>();
            List<Models.Task> pendingTasks = new List<Models.Task>();

            foreach (var k in allTasks)
            {
                if (k.GetType() !=typeof(CompletedTask))
                {
                    pendingTasks.Add(k);
                }
            }
            foreach (var Task in j)
            {
               if (Task.RecursDaily == true)
                {
                    daily.Add(Task);
                   
                }
                else if (Task.RecursWeekly == true)
                {
                    weekly.Add(Task);
                }
            
            }
            

            List<CompletedTask> compTasks = await _context.Tasks.OfType<CompletedTask>().ToListAsync();
            var vm = new TasksIndexViewModel
            {
                DailyTasks = daily,
                HourlyTasks = hourly,
                WeeklyTasks = weekly,
                CompletedTasks = compTasks,
                PendingTasks = pendingTasks
            };

            return View(vm);
        }              

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Task == null)
            {
                return NotFound();
            }
            return View(Task);
        }
        // GET: Tasks/Create
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
            var Tasks = await _context.Tasks.Include(t=>t.Department).ToListAsync();
            List<Models.Task> TasksList = new List<Models.Task>();
            List<RecurringTask> recurringList = new List<RecurringTask>();
            foreach (var j in Tasks)
            {
                if (j.GetType() != typeof(RecurringTask))
                {
                    TasksList.Add(j);
                }
                else
                {
                    recurringList.Add((RecurringTask)j);
                }
            }

            var vm = new TaskManagementViewModel();
            vm.Tasks = TasksList;
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
            
        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Department,Name,TaskDescription")] CreateTaskViewModel Task)
        {

            //Refactor required to database schema at this point will need to add an interface to manage user information as 
            //neglected to manage the user using the in-built user management services
            
            bool parseSuccessful = int.TryParse(Task.Department, out  int deptId);           
            //int deptId = Convert.ToInt32(Task.Department);
            var user = await _um.GetUserAsync(User);       
                       
            //checks if signed in for naming 
            if (_sm.IsSignedIn(User))
            {
                Task.Creator = await _context.Staff.FirstOrDefaultAsync(u => u.UserId == user.Id);
            }
            //if user is not signed in, gives unknown user, shouldn't get this far without it anyway, but just incase
            else
            {
                Task.Creator = new User { NickName = "Unknown User", Email = "Unknown User" };
            }
        
            //checks model state
            if (ModelState.IsValid)
            {
                var newTask = new Models.Task
                {
                    Id = Task.Id,
                    Name = Task.Name,
                    
                    TaskDescription = Task.TaskDescription,
                    Creator = Task.Creator,
                    TimeCreated = DateTime.Now
                   
                };
                if (parseSuccessful)
                {
                     newTask.Department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == deptId);
                }
                else
                {
                    newTask.Department = new Department
                    {
                        Name = "None"
                    };
                }
                _context.Add(newTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Task = await _context.Tasks.FindAsync(id);
            if (Task == null)
            {
                return NotFound();
            }
            return View(Task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TimeCreated,TaskDescription")] Models.Task Task)
        {
            if (id != Task.Id)
            {
                return NotFound();
            }
            //find the current users id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //match with database 
            Task.Creator =await _context.Staff.FirstOrDefaultAsync(s => s.UserId == userId);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(Task.Id))
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
            return View(Task);
        }

        //gets recurring Task page.
        [HttpGet]
        public async Task<IActionResult> MakeRecurringTask(int? id)
        {
            //refactor this code to get all information in a single call, needs to be condensed.
            if (id == null)
            {
                return NotFound();
            }
            //bring the department that the Task is assigned to
            var Task = await _context.Tasks.Include(d=>d.Department).FirstOrDefaultAsync(t=>t.Id == id);
            if (Task == null)
            {
                return NotFound();
            }
            var hourList = GetHourlyFrequencyList();
            ViewBag.HourList = hourList;

            var newTask = new MakeRecurringViewModel
            {
                Id = Task.Id,
                Name = Task.Name,
                TaskDescription = Task.TaskDescription,
                Department = Task.Department,
                Creator = Task.Creator
                
              
                

            };
            return View(newTask);

           
        }

        //method not finished

        [HttpPost]
        public async Task<IActionResult> MakeRecurringTask(int? id, [Bind("Id,Name,TaskDescription,RecursDaily,DailyTime,RecursHourly,RecursWeekly,HourlyFrequency,Creator")]
        RecurringTask recurringTask)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            if (ModelState.IsValid)
            {
                var existingTask = await _context.Tasks.Include(j=>j.Department).FirstOrDefaultAsync(t=>t.Id ==recurringTask.Id);
                recurringTask.TimeCreated = DateTime.Now;
                //Will need to get the daily time to be at session open
              
                try
                {                 
                        _context.Tasks.Remove(existingTask);
                        _context.RecurringTasks.Add(recurringTask);
                        await _context.SaveChangesAsync();                    
                  }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(recurringTask.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(TaskManagement), "Tasks");

            }
            return View(recurringTask);
        }

 

            // GET: Tasks/Delete/5
            public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Task == null)
            {
                return NotFound();
            }

            return View(Task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(Task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> MarkComplete(int? id)
        { 
            //find the user Id for the signed in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);                  
            var j = await _context.Tasks.FirstOrDefaultAsync(j => j.Id == id);

            //add the completed Task to the database and remove the task from the tasks list, save changes.

            //search database for the staff members details
            var markedCompleteBy = await _context.Staff.FirstOrDefaultAsync(s => s.UserId == userId);
            //draft new completed Task, inherits from Task
            var c = new CompletedTask
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
            _context.CompletedTasks.Add(c);
         
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index), "Home");
        }

        //returns completed Task to incomplete state.
        public async Task<IActionResult> MarkedInError(int id)

        {
            var j = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            var nJ = new Models.Task { 
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
 

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
