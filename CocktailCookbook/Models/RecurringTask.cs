using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
    public class RecurringTask : Task
    {
        [DisplayName("Recurs Weekly")]
        public bool RecursWeekly { get; set; }
        [DisplayName("Recurs Daily")]
        public bool RecursDaily { get; set; }

    


      

       
    }
}
