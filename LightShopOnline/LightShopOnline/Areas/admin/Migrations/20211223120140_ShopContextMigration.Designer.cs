﻿// <auto-generated />
using System;
using LightShopOnline.Areas.admin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LightShopOnline.Areas.admin.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20211223120140_ShopContextMigration")]
    partial class ShopContextMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LightShopOnline.Areas.admin.Models.Category", b =>
                {
                    b.Property<int>("Category_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category_Name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Picture1")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<int>("isHidden")
                        .HasColumnType("int");

                    b.Property<string>("parentId")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("url")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Category_Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("LightShopOnline.Areas.admin.Models.Category_Product", b =>
                {
                    b.Property<int>("Category_Id")
                        .HasColumnType("int");

                    b.Property<int>("Product_Id")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("Category_Id", "Product_Id");

                    b.HasIndex("Product_Id");

                    b.ToTable("Category_Product");
                });

            modelBuilder.Entity("LightShopOnline.Areas.admin.Models.Order", b =>
                {
                    b.Property<string>("Order_Id")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(510)")
                        .HasMaxLength(510);

                    b.Property<string>("Guest_Name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Guest_Phone")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<long?>("Price")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("dateCreate")
                        .HasColumnType("datetime2");

                    b.HasKey("Order_Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("LightShopOnline.Areas.admin.Models.OrderDetail", b =>
                {
                    b.Property<int>("Product_Id")
                        .HasColumnType("int");

                    b.Property<string>("Order_Id")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<int>("Product_Id1")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Product_Id");

                    b.HasIndex("Order_Id");

                    b.HasIndex("Product_Id1");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("LightShopOnline.Areas.admin.Models.Product", b =>
                {
                    b.Property<int>("Product_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Discount")
                        .HasColumnType("float");

                    b.Property<string>("Picture1")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("Picture2")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("Picture3")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("Picture4")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<long?>("Price")
                        .HasColumnType("bigint");

                    b.Property<string>("Product_Name")
                        .HasColumnType("nvarchar(510)")
                        .HasMaxLength(510);

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Warrant")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("isHidden")
                        .HasColumnType("int");

                    b.Property<string>("url")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.HasKey("Product_Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("LightShopOnline.Areas.admin.Models.UserTable", b =>
                {
                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Gmail")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Password")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Role")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Username")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("ggAuthId")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("User_Id");

                    b.ToTable("UserTable");
                });

            modelBuilder.Entity("LightShopOnline.Areas.admin.Models.Category_Product", b =>
                {
                    b.HasOne("LightShopOnline.Areas.admin.Models.Category", "Category")
                        .WithMany("Category_Product")
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LightShopOnline.Areas.admin.Models.Product", "Product")
                        .WithMany("Category_Product")
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LightShopOnline.Areas.admin.Models.OrderDetail", b =>
                {
                    b.HasOne("LightShopOnline.Areas.admin.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("Order_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LightShopOnline.Areas.admin.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("Product_Id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
