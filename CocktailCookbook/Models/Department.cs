﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<User> Staff { get; set; }

        [Display(Name="Department Tasks")]
        public List<Task> Tasks { get; set; }

    }
}
