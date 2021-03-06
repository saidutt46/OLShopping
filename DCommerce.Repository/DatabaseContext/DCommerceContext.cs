﻿using System;
using System.Linq;
using System.Threading.Tasks;
using DCommerce.Data.Domain;
using DCommerce.Data.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DCommerce.Repository.DatabaseContext
{
    public class DCommerceContext : IdentityDbContext<ApplicationUser>
    {
        public DCommerceContext(DbContextOptions<DCommerceContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasKey("Id");
            builder.Entity<Category>().HasMany(p => p.Products).WithOne(e => e.Category);
            builder.Entity<Category>().Property(p => p.Name).IsRequired();

            builder.Entity<Product>().HasKey("Id");
            builder.Entity<Product>().HasOne(p => p.Category).WithMany(e => e.Products);
            builder.Entity<Product>().Property(p => p.Name).IsRequired();
            builder.Entity<Product>().Property(p => p.Description).HasDefaultValue<string>("No Description available");
            builder.Entity<Product>().Property(p => p.QuantityInPackage).HasDefaultValue<short>(1).IsRequired();
            builder.Entity<Product>().Property(p => p.UnitPrice).IsRequired();

            builder.Entity<CartItem>().HasKey("Id");
            builder.Entity<CartItem>().HasOne(p => p.Product);
            builder.Entity<CartItem>().Property(p => p.Quantity).HasDefaultValue(1);
            builder.Entity<CartItem>().HasOne(p => p.User).WithMany(e => e.CartItems).OnDelete(DeleteBehavior.ClientCascade).
                HasForeignKey(p => p.IdentityId);

            builder.Entity<ApplicationUser>().HasMany(e => e.CartItems).WithOne(p => p.User);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public override int SaveChanges()
        {
            AddAuitInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuitInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuitInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).Created = DateTime.UtcNow;
                }
                ((BaseEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}
