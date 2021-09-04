using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
   //to be implemented as Table per concrete class 
    public class CompletedJob:Job
    {
        public DateTime TimeCompleted { get; set; }

        public User MarkedCompleteBy { get; set; }
    }
}
