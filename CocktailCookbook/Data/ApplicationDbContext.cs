using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CocktailCookbook.Models;
using Microsoft.AspNetCore.Identity;
using CocktailCookbook.ViewModels;

namespace CocktailCookbook.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CocktailCookbook.Models.Post> Post { get; set; }
        public DbSet<Models.User> Staff { get; set; }


        public Cocktail Cocktails { get; set; }
        public DbSet<CocktailCookbook.Models.Comment> Comment { get; set; }
        public DbSet<CocktailCookbook.Models.Cocktail> Cocktail { get; set; }
        public DbSet<CocktailCookbook.Models.CocktailIngredient> CocktailIngredient { get; set; }
        public DbSet<CocktailCookbook.Models.Ingredient> Ingredient { get; set; }
        public DbSet<CompletedTask> CompletedTasks { get; set; }
        public DbSet<RecurringTask> RecurringTasks { get; set; }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CocktailIngredient>()
                .HasKey(k => new { k.CocktailId, k.IngredientId });



            //builder.Entity<CompletedTask>().ToTable("CompletedTask");
            //builder.Entity<Task>().ToTable("Task");
            //attempting to model the Tasks inheritance tree

        }

        public DbSet<CocktailCookbook.ViewModels.MakeRecurringViewModel> MakeRecurringViewModel { get; set; }

        public DbSet<CocktailCookbook.Models.Glassware> Glassware { get; set; }

        public DbSet<CocktailCookbook.Models.Premix> Premix { get; set; }
       
    }
}
