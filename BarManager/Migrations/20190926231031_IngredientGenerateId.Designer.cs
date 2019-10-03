﻿// <auto-generated />
using System;
using BarManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BarManager.Migrations
{
    [DbContext(typeof(BarManagerContext))]
    [Migration("20190926231031_IngredientGenerateId")]
    partial class IngredientGenerateId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BarManager.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(60);

                    b.Property<bool>("Owned");

                    b.Property<DateTime>("PurchaseDate");

                    b.Property<string>("User")
                        .IsRequired();

                    b.HasKey("IngredientID", "Name");

                    b.HasIndex("IngredientID")
                        .IsUnique();

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("BarManager.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddedDate");

                    b.Property<string>("Description")
                        .HasMaxLength(100);

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Rating");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("User")
                        .IsRequired();

                    b.HasKey("RecipeID");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("BarManager.Models.RecipeIngredient", b =>
                {
                    b.Property<int>("RecipeIngredientID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int?>("IngredientID");

                    b.Property<int>("IngredientId");

                    b.Property<string>("IngredientName");

                    b.Property<int>("RecipeId");

                    b.Property<bool>("Required");

                    b.Property<string>("User")
                        .IsRequired();

                    b.HasKey("RecipeIngredientID");

                    b.HasIndex("RecipeId");

                    b.HasIndex("IngredientID", "IngredientName");

                    b.ToTable("RecipeIngredient");
                });

            modelBuilder.Entity("BarManager.Models.RecipeIngredient", b =>
                {
                    b.HasOne("BarManager.Models.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BarManager.Models.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientID", "IngredientName");
                });
#pragma warning restore 612, 618
        }
    }
}
