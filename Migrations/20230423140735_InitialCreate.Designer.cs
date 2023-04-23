﻿// <auto-generated />
using System;
using InventoryManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryManagement.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230423140735_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("InventoryManagement.Domain.Entities.Asset", b =>
                {
                    b.Property<Guid>("AssetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AssetType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InventoryNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("AssetId");

                    b.ToTable("Assets");

                    b.HasData(
                        new
                        {
                            AssetId = new Guid("01c8a12c-f97a-49bb-855b-12d4a8328190"),
                            AssetType = 1,
                            InventoryNumber = "698754333121",
                            Location = "г. Самара, Заводское шоссе, д. 10",
                            Name = "Samsung Galaxy S22 8/256 Snapdragon 8 Gen 1",
                            Price = 59990.0,
                            Quantity = 5
                        },
                        new
                        {
                            AssetId = new Guid("43d1787f-ea86-4aaf-89ab-eec3b936d22a"),
                            AssetType = 1,
                            InventoryNumber = "386129104576",
                            Location = "г. Самара, Заводское шоссе, д. 10",
                            Name = "Samsung Galaxy S22 8/128 Snapdragon 8 Gen 1",
                            Price = 54990.0,
                            Quantity = 5
                        },
                        new
                        {
                            AssetId = new Guid("a29f676c-9df5-4dfa-91e6-590436f83293"),
                            AssetType = 1,
                            InventoryNumber = "499111487615",
                            Location = "г. Самара, Заводское шоссе, д. 10",
                            Name = "Samsung Galaxy S23 8/128 Snapdragon 8 Gen 2",
                            Price = 74990.0,
                            Quantity = 5
                        },
                        new
                        {
                            AssetId = new Guid("267055a3-9493-4d75-be17-164f620f1dc7"),
                            AssetType = 1,
                            InventoryNumber = "332829934363",
                            Location = "г. Самара, Заводское шоссе, д. 10",
                            Name = "Samsung Galaxy S23 8/256 Snapdragon 8 Gen 2",
                            Price = 79990.0,
                            Quantity = 5
                        });
                });

            modelBuilder.Entity("InventoryManagement.Domain.Entities.Inventory", b =>
                {
                    b.Property<Guid>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("ActualQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("AssetId")
                        .HasColumnType("TEXT");

                    b.Property<string>("InventoryNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("QuantityOnStock")
                        .HasColumnType("INTEGER");

                    b.HasKey("InventoryId");

                    b.HasIndex("AssetId")
                        .IsUnique();

                    b.ToTable("Inventories");

                    b.HasData(
                        new
                        {
                            InventoryId = new Guid("7fb07785-d92a-4509-92f1-4100ce54be27"),
                            ActualQuantity = 5,
                            AssetId = new Guid("01c8a12c-f97a-49bb-855b-12d4a8328190"),
                            InventoryNumber = "698754333121",
                            Name = "Samsung Galaxy S22 8/256 Snapdragon 8 Gen 1",
                            QuantityOnStock = 5
                        },
                        new
                        {
                            InventoryId = new Guid("941a623d-bc1b-407b-b5e9-88c08171fa29"),
                            ActualQuantity = 5,
                            AssetId = new Guid("43d1787f-ea86-4aaf-89ab-eec3b936d22a"),
                            InventoryNumber = "386129104576",
                            Name = "Samsung Galaxy S23 8/128 Snapdragon 8 Gen 2",
                            QuantityOnStock = 5
                        },
                        new
                        {
                            InventoryId = new Guid("3ddcc02f-f66c-47cf-84e5-e0e2d62fe4e2"),
                            ActualQuantity = 5,
                            AssetId = new Guid("a29f676c-9df5-4dfa-91e6-590436f83293"),
                            InventoryNumber = "499111487615",
                            Name = "Samsung Galaxy S23 8/256 Snapdragon 8 Gen 2",
                            QuantityOnStock = 5
                        },
                        new
                        {
                            InventoryId = new Guid("415ce6ac-c3e6-4392-8e8c-55ac72004c01"),
                            ActualQuantity = 5,
                            AssetId = new Guid("267055a3-9493-4d75-be17-164f620f1dc7"),
                            InventoryNumber = "332829934363",
                            Name = "Samsung Galaxy S23 8/256 Snapdragon 8 Gen 2",
                            QuantityOnStock = 5
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "994015C4-E1CE-4B39-8CA0-9D814FE9FDFE",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "E43DA02E-DA5C-4E35-BE9A-D2487C98A910",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9de2abe9-4ef9-47e6-8a6e-a0f0eafe634b",
                            Email = "SuperCompany@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "SUPERCOMPANY@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEBK5c+dSevQyMmn7lLy51nyMM2nuiISsEe0Tf5usKRHfrLIrHgL7Qu2sm8Tu/YsbnA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "E43DA02E-DA5C-4E35-BE9A-D2487C98A910",
                            RoleId = "994015C4-E1CE-4B39-8CA0-9D814FE9FDFE"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("InventoryManagement.Domain.Entities.Inventory", b =>
                {
                    b.HasOne("InventoryManagement.Domain.Entities.Asset", "Asset")
                        .WithOne("Inventory")
                        .HasForeignKey("InventoryManagement.Domain.Entities.Inventory", "AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InventoryManagement.Domain.Entities.Asset", b =>
                {
                    b.Navigation("Inventory");
                });
#pragma warning restore 612, 618
        }
    }
}