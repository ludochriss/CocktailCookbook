using CocktailCookbook.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.ViewModels
{
    public class MakeRecurringViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public User Creator { get; set; }

       

        public DateTime TimeCreated { get; set; }

        public Department Department { get; set; }
        public string TaskDescription { get; set; }



        [DisplayName("Recurs Weekly")]
        public bool RecursWeekly { get; set; }
        [DisplayName("Recurs Daily")]
        public bool RecursDaily { get; set; }

        [DisplayName("Recurs Hourly")]
        public bool RecursHourly { get; set; }


        [DisplayName("Hourly tasks will repeat from this date at this time")]
        // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM}")]
        [BindProperty, DataType(DataType.Time)]
        public DateTime DailyTime { get; set; }

        [DisplayName("Hourly Frequency")]
        public double HourlyFrequency { get; set; }
    }
}
