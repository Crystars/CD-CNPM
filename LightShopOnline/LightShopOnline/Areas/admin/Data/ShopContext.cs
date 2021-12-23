using LightShopOnline.Areas.admin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using LightShopOnline.Const;

namespace LightShopOnline.Areas.admin.Data
{
    public class ShopContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Category_Product> Category_Product { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConstValue.LocalConnection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.parentId)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Picture1)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Order_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Guest_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Order_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Picture1)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Picture2)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Picture3)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Picture4)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.ggAuthId)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.Gmail)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>().HasKey(table => new {
                table.Product_Id
            });

            modelBuilder.Entity<Category_Product>().HasKey(table => new {
                table.Category_Id,
                table.Product_Id
            });
        }
    }
}
