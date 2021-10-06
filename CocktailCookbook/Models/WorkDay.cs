using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{

    [NotMapped]
    public class WorkDay
    {
        
        public string DayOfWeek { get; set; }
        public bool Morning { get; set; }

        public bool Evening { get; set; }


    }
}
