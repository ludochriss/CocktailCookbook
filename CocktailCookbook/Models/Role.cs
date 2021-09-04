using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CocktailCookbook.Models
{
    public class Role
    {
        [Display(Name="Role Name")]
        public int Id { get; set; }

        public string  RoleName { get; set; }

        public List<Authorisation> Authorisations { get; set; }


    }
}
