using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
    public class RecurringTask : Job
    {
        [DisplayName("Recurs Weekly")]
        public bool RecursWeekly { get; set; }
        [DisplayName("Recurs Daily")]
        public bool RecursDaily { get; set; }

        [DisplayName("Recurs Hourly")]
        public bool RecursHourly { get; set; }


        [DisplayName("Select a time for daily recurrance. Only triggers if Recus Daily is selected.")]
        public DateTime DailyTime { get; set; }

        [DisplayName("Hourly Frequency")]
        public double HourlyFrequency { get; set; }

    }
}
