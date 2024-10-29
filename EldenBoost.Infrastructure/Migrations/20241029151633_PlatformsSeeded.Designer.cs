﻿// <auto-generated />
using System;
using EldenBoost.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EldenBoost.Infrastructure.Migrations
{
    [DbContext(typeof(EldenBoostDbContext))]
    [Migration("20241029151633_PlatformsSeeded")]
    partial class PlatformsSeeded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApplicationPlatform", b =>
                {
                    b.Property<int>("ApplicationId")
                        .HasColumnType("int")
                        .HasColumnName("ApplicationId");

                    b.Property<int>("PlatformId")
                        .HasColumnType("int")
                        .HasColumnName("PlatformId");

                    b.HasKey("ApplicationId", "PlatformId");

                    b.HasIndex("PlatformId");

                    b.ToTable("ApplicationsPlatforms", (string)null);
                });

            modelBuilder.Entity("BoosterPlatform", b =>
                {
                    b.Property<int>("BoosterId")
                        .HasColumnType("int")
                        .HasColumnName("BoosterId");

                    b.Property<int>("PlatformId")
                        .HasColumnType("int")
                        .HasColumnName("PlatformId");

                    b.HasKey("BoosterId", "PlatformId");

                    b.HasIndex("PlatformId");

                    b.ToTable("BoostersPlatforms", (string)null);
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier for the Application.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicationType")
                        .HasColumnType("int")
                        .HasComment("Type of application submitted by the user.");

                    b.Property<int>("Availability")
                        .HasColumnType("int")
                        .HasComment("Availability status of the applicant, typically represented in hours or a defined scale.");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(56)
                        .HasColumnType("nvarchar(56)")
                        .HasComment("Country of the applicant.");

                    b.Property<string>("Experience")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("Experience level or description provided by the applicant.");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the application has been approved.");

                    b.Property<bool>("IsRejected")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the application has been rejected.");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User ID associated with the application.");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasComment("Nickname for application user");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePicture")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)")
                        .HasComment("Profile picture application user.");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier for the Article.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArticleType")
                        .HasColumnType("int")
                        .HasComment("Type of Article, represented by the ArticleType enumeration.");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasComment("ID of the author who created the Article.");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Main content of the Article.");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)")
                        .HasComment("URL of the image associated with the Article.");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date when the Article was released or published.");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Title of the Article.");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier for the Author");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(56)
                        .HasColumnType("nvarchar(56)")
                        .HasComment("Country of residence for the Author.");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User ID associated with the author.");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Booster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier for the Booster.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(56)
                        .HasColumnType("nvarchar(56)")
                        .HasComment("Country of residence for the Booster.");

                    b.Property<double>("Rating")
                        .HasColumnType("float")
                        .HasComment("Current average rating of the Booster.");

                    b.Property<int>("RatingCount")
                        .HasColumnType("int")
                        .HasComment("Total number of ratings received by the Booster.");

                    b.Property<decimal>("TotalEarned")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Total amount earned by the Booster (completed orders only).");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User ID associated with the Booster.");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Boosters");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier for the Order.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BoosterId")
                        .HasColumnType("int")
                        .HasComment("ID of the assigned Booster, if any.");

                    b.Property<decimal>("BoosterPay")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Amount to be paid to the Booster.");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("ID of the client placing the order.");

                    b.Property<bool>("HasStream")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the Order includes streaming.");

                    b.Property<string>("Information")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Additional information provided for the Order.");

                    b.Property<bool>("IsAddedToPayment")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the Order has been added to a payment batch.");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the Order has been archived.");

                    b.Property<bool>("IsExpress")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the Order is an express request.");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the Order has been paid to the booster.");

                    b.Property<bool>("IsRated")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the Order has been rated by the client.");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int")
                        .HasComment("ID of the payment associated with the Order, if any.");

                    b.Property<int>("PlatformId")
                        .HasColumnType("int")
                        .HasComment("ID of the platform associated with the Order.");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Total price of the Order.");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int")
                        .HasComment("ID of the associated Service.");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Current status of the Order.");

                    b.Property<DateTime>("TimeOfPurchase")
                        .HasColumnType("datetime2")
                        .HasComment("Timestamp of when the Order was placed.");

                    b.HasKey("Id");

                    b.HasIndex("BoosterId");

                    b.HasIndex("ClientId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("PlatformId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier for the Payment.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Total amount of the Payment.");

                    b.Property<int>("BoosterId")
                        .HasColumnType("int")
                        .HasComment("Foreign key linking the Payment to the associated Booster.");

                    b.Property<DateTime>("CompletionDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date when the Payment was completed.");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the Payment has been completed.");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date when the Payment was issued.");

                    b.HasKey("Id");

                    b.HasIndex("BoosterId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier for the Platform.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)")
                        .HasComment("Name of the Platform.");

                    b.HasKey("Id");

                    b.ToTable("Platforms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "PC"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Playstation"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Xbox"
                        });
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier for the Review.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(248)
                        .HasColumnType("nvarchar(248)")
                        .HasComment("Content of the review, limited to 248 characters.");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date and time when the review was created, defaulting to UTC now.");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Foreign key referencing the User who created the review.");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier for the Service.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(248)
                        .HasColumnType("nvarchar(248)")
                        .HasComment("Service description.");

                    b.Property<string>("ImageURL")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)")
                        .HasComment("Service image url.");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasComment("Flag for active and inactive services.");

                    b.Property<int>("MaxAmount")
                        .HasColumnType("int")
                        .HasComment("Max amount for slider type services.");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Service price.");

                    b.Property<int>("PurchaseCount")
                        .HasColumnType("int")
                        .HasComment("Service purchase count.");

                    b.Property<int>("ServiceType")
                        .HasColumnType("int")
                        .HasComment("Service type");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasComment("Service title.");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.ServiceOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier for the ServiceOption.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasComment("Name of the ServiceOption.");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Price associated with this ServiceOption.");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int")
                        .HasComment("Reference to the associated Service entity.");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceOptions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ApplicationPlatform", b =>
                {
                    b.HasOne("EldenBoost.Infrastructure.Data.Models.Application", null)
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ApplicationPlatform_Applications_Id");

                    b.HasOne("EldenBoost.Infrastructure.Data.Models.Platform", null)
                        .WithMany()
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ApplicationPlatform_Platforms_Id");
                });

            modelBuilder.Entity("BoosterPlatform", b =>
                {
                    b.HasOne("EldenBoost.Infrastructure.Data.Models.Booster", null)
                        .WithMany()
                        .HasForeignKey("BoosterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BoosterPlatform_Boosters_Id");

                    b.HasOne("EldenBoost.Infrastructure.Data.Models.Platform", null)
                        .WithMany()
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BoosterPlatform_Platforms_Id");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Application", b =>
                {
                    b.HasOne("EldenBoost.Infrastructure.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Article", b =>
                {
                    b.HasOne("EldenBoost.Infrastructure.Data.Models.Author", "Author")
                        .WithMany("Articles")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Author", b =>
                {
                    b.HasOne("EldenBoost.Infrastructure.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Booster", b =>
                {
                    b.HasOne("EldenBoost.Infrastructure.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Order", b =>
                {
                    b.HasOne("EldenBoost.Infrastructure.Data.Models.Booster", "Booster")
                        .WithMany("Orders")
                        .HasForeignKey("BoosterId");

                    b.HasOne("EldenBoost.Infrastructure.Data.Models.ApplicationUser", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EldenBoost.Infrastructure.Data.Models.Payment", "Payment")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentId");

                    b.HasOne("EldenBoost.Infrastructure.Data.Models.Platform", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EldenBoost.Infrastructure.Data.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booster");

                    b.Navigation("Client");

                    b.Navigation("Payment");

                    b.Navigation("Platform");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Payment", b =>
                {
                    b.HasOne("EldenBoost.Infrastructure.Data.Models.Booster", "Booster")
                        .WithMany("Payments")
                        .HasForeignKey("BoosterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booster");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Review", b =>
                {
                    b.HasOne("EldenBoost.Infrastructure.Data.Models.ApplicationUser", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.ServiceOption", b =>
                {
                    b.HasOne("EldenBoost.Infrastructure.Data.Models.Service", "Service")
                        .WithMany("Options")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");
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
                    b.HasOne("EldenBoost.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EldenBoost.Infrastructure.Data.Models.ApplicationUser", null)
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

                    b.HasOne("EldenBoost.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EldenBoost.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Author", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Booster", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Payment", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("EldenBoost.Infrastructure.Data.Models.Service", b =>
                {
                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
