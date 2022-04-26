using CocktailCookbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.ViewModels
{
    
    public class TasksIndexViewModel
    {
        
        public List<Models.Task> PendingTasks { get; set; }
        public List<RecurringTask> HourlyTasks { get; set; }

        public List<RecurringTask> DailyTasks { get; set; }

        public List<RecurringTask> WeeklyTasks { get; set; }

        public List<CompletedTask> CompletedTasks { get; set; }

    }
}
