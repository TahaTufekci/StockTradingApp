﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockTradingApp.Data;

#nullable disable

namespace StockTradingApp.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240504165712_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StockTradingApp.Models.Portfolio", b =>
                {
                    b.Property<int>("PortfolioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PortfolioID"));

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("PortfolioID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("StockTradingApp.Models.Stock", b =>
                {
                    b.Property<int>("StockID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockID"));

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("StockListID")
                        .HasColumnType("int");

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WatchlistUserID")
                        .HasColumnType("int");

                    b.HasKey("StockID");

                    b.HasIndex("StockListID");

                    b.HasIndex("WatchlistUserID");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("StockTradingApp.Models.StockList", b =>
                {
                    b.Property<int>("StockListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockListID"));

                    b.HasKey("StockListID");

                    b.ToTable("StockLists");
                });

            modelBuilder.Entity("StockTradingApp.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StockTradingApp.Models.UserStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("PortfolioID")
                        .HasColumnType("int");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("StockID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioID");

                    b.HasIndex("StockID");

                    b.HasIndex("UserID");

                    b.ToTable("UserStocks");
                });

            modelBuilder.Entity("StockTradingApp.Models.Watchlist", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("StockID")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.ToTable("Watchlists");
                });

            modelBuilder.Entity("StockTradingApp.Models.Portfolio", b =>
                {
                    b.HasOne("StockTradingApp.Models.User", "User")
                        .WithOne("Portfolio")
                        .HasForeignKey("StockTradingApp.Models.Portfolio", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StockTradingApp.Models.Stock", b =>
                {
                    b.HasOne("StockTradingApp.Models.StockList", null)
                        .WithMany("ListStock")
                        .HasForeignKey("StockListID");

                    b.HasOne("StockTradingApp.Models.Watchlist", null)
                        .WithMany("Stocks")
                        .HasForeignKey("WatchlistUserID");
                });

            modelBuilder.Entity("StockTradingApp.Models.UserStock", b =>
                {
                    b.HasOne("StockTradingApp.Models.Portfolio", null)
                        .WithMany("UserStocks")
                        .HasForeignKey("PortfolioID");

                    b.HasOne("StockTradingApp.Models.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockTradingApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StockTradingApp.Models.Watchlist", b =>
                {
                    b.HasOne("StockTradingApp.Models.User", "User")
                        .WithOne("Watchlist")
                        .HasForeignKey("StockTradingApp.Models.Watchlist", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StockTradingApp.Models.Portfolio", b =>
                {
                    b.Navigation("UserStocks");
                });

            modelBuilder.Entity("StockTradingApp.Models.StockList", b =>
                {
                    b.Navigation("ListStock");
                });

            modelBuilder.Entity("StockTradingApp.Models.User", b =>
                {
                    b.Navigation("Portfolio")
                        .IsRequired();

                    b.Navigation("Watchlist")
                        .IsRequired();
                });

            modelBuilder.Entity("StockTradingApp.Models.Watchlist", b =>
                {
                    b.Navigation("Stocks");
                });
#pragma warning restore 612, 618
        }
    }
}
