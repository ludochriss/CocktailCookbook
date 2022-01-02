using CocktailCookbook.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.ViewModels
{
    public class AdminIndexViewModel
    {
        public List<string> Roles { get; set; }
        public List<UsersRolesViewModel> UserRoles { get; set; }

        public List<Department> Departments { get; set; }
    }
}
