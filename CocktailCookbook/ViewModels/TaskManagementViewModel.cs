using CocktailCookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.ViewModels
{
    public class TaskManagementViewModel
    {
        public List<Job> Jobs { get; set; }

        public List<RecurringTask> RecurringTasks { get; set; }
    }
}
