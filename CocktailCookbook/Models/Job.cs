using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
  
    public  class Job
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public User Creator { get; set; }

       
        public DateTime TimeCreated { get; set; }

      
        public string TaskDescription { get; set; }

    }
}
