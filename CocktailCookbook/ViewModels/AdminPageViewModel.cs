using CocktailCookbook.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.ViewModels
{
    public class AdminPageViewModel
    {

        
        [Display(Name ="Users")]
        public List<User> Users { get; set; }


        public List<IdentityRole> Roles { get; set; }


    }
}
