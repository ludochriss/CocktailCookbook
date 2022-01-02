using CocktailCookbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.ViewModels
{
    
    public class JobsIndexViewModel
    {
        
        public List<Job> PendingJobs { get; set; }
        public List<RecurringTask> HourlyJobs { get; set; }

        public List<RecurringTask> DailyJobs { get; set; }

        public List<RecurringTask> WeeklyJobs { get; set; }

        public List<CompletedJob> CompletedJobs { get; set; }

    }
}
