using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CocktailCookbook.Models;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CocktailIngredient>()
                .HasKey(k => new { k.CocktailId, k.IngredientId });
        }
       

    }
}
