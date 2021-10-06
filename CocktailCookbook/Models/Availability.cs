using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
    public class Availability
    {
        public int Id { get; set; }

        public User User { get; set; }

        public DateTime PeriodBeginning { get; set; }

        public DateTime PeriodEnd { get; set; }

        public List<WorkDay> MyProperty { get; set; }

    }
}
