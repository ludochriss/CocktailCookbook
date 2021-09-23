using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.ViewModels
{
    public class UsersRolesViewModel
    {
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string AssignedRoles { get; set; }
   
    }
   
}
