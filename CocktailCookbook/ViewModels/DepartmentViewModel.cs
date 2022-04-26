using CocktailCookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.ViewModels
{
    public class DepartmentViewModel
    {
        public List<Department> Departments { get; set; }

        public List<Models.Task> Tasks { get; set; }
    }
}
