﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using e_Estoque_API.Infrastructure.Persistence;

#nullable disable

namespace e_Estoque_API.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(EstoqueDbContext))]
    partial class EstoqueDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(5000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Categories", "public");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("DocId")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Companies", "public");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("DocId")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Customers", "public");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Inventory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DateOrder")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("IdProduct")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("IdProduct");

                    b.ToTable("inventories", "public");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal");

                    b.Property<Guid>("IdCategory")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdCompany")
                        .HasColumnType("uuid");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("varchar(5000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Length")
                        .HasColumnType("decimal");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal");

                    b.HasKey("Id");

                    b.HasIndex("IdCategory");

                    b.HasIndex("IdCompany");

                    b.ToTable("Products", "public");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeliveryDate")
                        .IsRequired()
                        .HasColumnType("timestamp");

                    b.Property<Guid>("IdCustomer")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("PaymentDate")
                        .IsRequired()
                        .HasColumnType("timestamp");

                    b.Property<int>("PaymentType")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("timestamp");

                    b.Property<int>("SaleType")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal");

                    b.Property<decimal>("TotalTax")
                        .HasColumnType("decimal");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("IdCustomer");

                    b.ToTable("Sales", "public");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.SaleProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("IdProduct")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdSale")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("IdProduct");

                    b.HasIndex("IdSale");

                    b.ToTable("SaleProducts", "public");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Tax", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<Guid>("IdCategory")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("IdCategory");

                    b.ToTable("Taxs", "public");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Company", b =>
                {
                    b.OwnsOne("e_Estoque_API.Domain.ValueObjects.CompanyAddress", "CompanyAddress", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CompanyAddress_City");

                            b1.Property<string>("Complement")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CompanyAddress_Complement");

                            b1.Property<string>("County")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CompanyAddress_County");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CompanyAddress_District");

                            b1.Property<string>("Latitude")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CompanyAddress_Latitude");

                            b1.Property<string>("Longitude")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CompanyAddress_Longitude");

                            b1.Property<string>("Neighborhood")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CompanyAddress_Neighborhood");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CompanyAddress_Number");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CompanyAddress_Street");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CompanyAddress_ZipCode");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies", "public");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.Navigation("CompanyAddress")
                        .IsRequired();
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Customer", b =>
                {
                    b.OwnsOne("e_Estoque_API.Domain.ValueObjects.CustomerAddress", "CustomerAddress", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CustomerAddress_City");

                            b1.Property<string>("Complement")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CustomerAddress_Complement");

                            b1.Property<string>("County")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CustomerAddress_County");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CustomerAddress_District");

                            b1.Property<string>("Latitude")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CustomerAddress_Latitude");

                            b1.Property<string>("Longitude")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CustomerAddress_Longitude");

                            b1.Property<string>("Neighborhood")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CustomerAddress_Neighborhood");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CustomerAddress_Number");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CustomerAddress_Street");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("varchar(80)")
                                .HasColumnName("CustomerAddress_ZipCode");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers", "public");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("CustomerAddress")
                        .IsRequired();
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Inventory", b =>
                {
                    b.HasOne("e_Estoque_API.Core.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("IdProduct")
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Product", b =>
                {
                    b.HasOne("e_Estoque_API.Core.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("IdCategory")
                        .IsRequired();

                    b.HasOne("e_Estoque_API.Core.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("IdCompany")
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Sale", b =>
                {
                    b.HasOne("e_Estoque_API.Core.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("IdCustomer")
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.SaleProduct", b =>
                {
                    b.HasOne("e_Estoque_API.Core.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("IdProduct")
                        .IsRequired();

                    b.HasOne("e_Estoque_API.Core.Entities.Sale", "Sale")
                        .WithMany("SaleProducts")
                        .HasForeignKey("IdSale")
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Tax", b =>
                {
                    b.HasOne("e_Estoque_API.Core.Entities.Category", "Category")
                        .WithMany("Taxs")
                        .HasForeignKey("IdCategory")
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Category", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Taxs");
                });

            modelBuilder.Entity("e_Estoque_API.Core.Entities.Sale", b =>
                {
                    b.Navigation("SaleProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
