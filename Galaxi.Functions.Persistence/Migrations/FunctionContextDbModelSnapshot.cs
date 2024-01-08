﻿// <auto-generated />
using System;
using Galaxi.Functions.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Galaxi.Functions.Persistence.Migrations
{
    [DbContext(typeof(FunctionContextDb))]
    partial class FunctionContextDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Galaxi.Functions.Data.Models.Function", b =>
                {
                    b.Property<int>("FunctionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FunctionId"));

                    b.Property<DateTime>("FunctionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Room")
                        .HasColumnType("int");

                    b.HasKey("FunctionId");

                    b.ToTable("MovieFunction", "DBO");
                });
#pragma warning restore 612, 618
        }
    }
}