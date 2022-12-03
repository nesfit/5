using System;
using CookBook.Api.DAL.Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Api.DAL.EF
{
    public class CookBookDbContext : IdentityDbContext<AppUserEntity, AppRoleEntity, Guid, AppUserClaimEntity, AppUserRoleEntity, AppUserLoginEntity, AppRoleClaimEntity, AppUserTokenEntity>
    {
        public DbSet<IngredientEntity> Ingredients { get; set; } = null!;
        public DbSet<RecipeEntity> Recipes { get; set; } = null!;
        public DbSet<IngredientAmountEntity> IngredientAmounts { get; set; } = null!;
        
        public CookBookDbContext(DbContextOptions<CookBookDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppRoleClaimEntity>().ToTable("AppRoleClaims");
            modelBuilder.Entity<AppRoleEntity>().ToTable("AppRoles");
            modelBuilder.Entity<AppUserClaimEntity>().ToTable("AppUserClaims");
            modelBuilder.Entity<AppUserEntity>().ToTable("AppUsers");
            modelBuilder.Entity<AppUserLoginEntity>().ToTable("AppUserLogins");
            modelBuilder.Entity<AppUserRoleEntity>().ToTable("AppUserRoles");
            modelBuilder.Entity<AppUserTokenEntity>().ToTable("AppUserTokens");
            modelBuilder.Entity<IngredientAmountEntity>().ToTable("IngredientAmounts");
            modelBuilder.Entity<IngredientEntity>().ToTable("Ingredients");
            modelBuilder.Entity<RecipeEntity>().ToTable("Recipes");

            modelBuilder.Entity<RecipeEntity>()
                .HasMany(recipeEntity => recipeEntity.IngredientAmounts)
                .WithOne(ingredientAmountEntity => ingredientAmountEntity.Recipe)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IngredientEntity>()
                .HasMany(typeof(IngredientAmountEntity))
                .WithOne(nameof(IngredientAmountEntity.Ingredient))
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
