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
        public DbSet<CompletedJob> CompletedJobs { get; set; }
        public DbSet<RecurringTask> RecurringTasks { get; set; }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CocktailIngredient>()
                .HasKey(k => new { k.CocktailId, k.IngredientId });



            //builder.Entity<CompletedJob>().ToTable("CompletedJob");
            //builder.Entity<Job>().ToTable("Job");
            //attempting to model the jobs inheritance tree

        }

        public DbSet<CocktailCookbook.ViewModels.MakeRecurringViewModel> MakeRecurringViewModel { get; set; }
       
    }
}
