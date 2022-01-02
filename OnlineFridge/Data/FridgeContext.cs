using Microsoft.EntityFrameworkCore;
using OnlineFridge.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace OnlineFridge.Data {
    public class FridgeContext : IdentityDbContext<ApplicationUser> {
        public FridgeContext(DbContextOptions<FridgeContext> options) : base(options) {}

        public DbSet<Recipe>? Recipes {get; set;}
        public DbSet<Ingredient>? Ingredients {get; set;}
        public DbSet<Quantity>? Quantities {get; set;}
        public DbSet<Step>? Steps {get; set;}
        public DbSet<UserIngredient>? userIngredients {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Recipe>().ToTable("Recipe");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredient");
            modelBuilder.Entity<Quantity>().ToTable("Quantity");
            modelBuilder.Entity<Step>().ToTable("Step");
            modelBuilder.Entity<UserIngredient>().ToTable("UserIngredient");
        }
    }
}