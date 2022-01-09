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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConstValue.RemoteConnection);
        }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Category_Product> Category_Product { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .Property(e => e.Cart_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Cart>()
                .HasMany(e => e.OrderDetails);
                //.WithRequired(e => e.Cart)
                //.WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.parentId)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Picture1)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Category_Product);
                //.WithRequired(e => e.Category)
                //.WillCascadeOnDelete(false);

            modelBuilder.Entity<Coupon>()
                .Property(e => e.Coupon_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Order_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Guest_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Coupon_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.paymentMethod)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails);
                //.WithRequired(e => e.Order)
                //.WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Order_Id)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Cart_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Picture1)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Category_Product);
                //.WithRequired(e => e.Product)
                //.WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails);
                //.WithRequired(e => e.Product)
                //.WillCascadeOnDelete(false);

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

            modelBuilder.Entity<UserTable>()
                .HasMany(e => e.Carts);
                //.WithRequired(e => e.UserTable)
                //.WillCascadeOnDelete(false);
            
            modelBuilder.Entity<UserTable>()
                .HasMany(e => e.Orders);
                //.WithRequired(e => e.UserTable)
                //.WillCascadeOnDelete(false);

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
