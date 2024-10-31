﻿// <auto-generated />
using System;
using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleanService.Migrations
{
    [DbContext(typeof(CleanServiceContext))]
    partial class CleanServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("CleanService.Src.Models.BlacklistedUsers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("BlacklistedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 947, DateTimeKind.Local).AddTicks(9082));

                    b.Property<string>("BlacklistedBy")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsPermanent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("BlacklistedBy");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("BlacklistedUsers");
                });

            modelBuilder.Entity("CleanService.Src.Models.BookingDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("BathroomCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("BedroomCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<Guid>("BookingId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 946, DateTimeKind.Local).AddTicks(6745));

                    b.Property<int>("KitchenCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("LivingRoomCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("SpecialRequirements")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("BookingDetails");
                });

            modelBuilder.Entity("CleanService.Src.Models.Bookings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CancellationReason")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 945, DateTimeKind.Local).AddTicks(9434));

                    b.Property<string>("CustomerFeedback")
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("HelperFeedback")
                        .HasColumnType("longtext");

                    b.Property<string>("HelperId")
                        .HasColumnType("varchar(255)")
                        .HasComment("Can be NULL if not assigned yet");

                    b.Property<decimal?>("HelperRating")
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)");

                    b.Property<string>("Location")
                        .HasColumnType("longtext");

                    b.Property<string>("PaymentMethod")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(24)")
                        .HasDefaultValue("Pending");

                    b.Property<DateTime>("ScheduledEndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ScheduledStartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ServiceTypeId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(24)")
                        .HasDefaultValue("Pending");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 946, DateTimeKind.Local).AddTicks(926));

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("HelperId");

                    b.HasIndex("ServiceTypeId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("CleanService.Src.Models.Complaints", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 947, DateTimeKind.Local).AddTicks(6942));

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ReportedById")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ReportedUserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Resolution")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ResolvedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("ReportedById");

                    b.HasIndex("ReportedUserId");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("CleanService.Src.Models.Contracts", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 947, DateTimeKind.Local).AddTicks(3417));

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("CleanService.Src.Models.DurationPrice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 945, DateTimeKind.Local).AddTicks(3782));

                    b.Property<int>("DurationHours")
                        .HasColumnType("int");

                    b.Property<decimal>("PriceMultiplier")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3, 2)
                        .HasColumnType("decimal(3,2)")
                        .HasDefaultValue(1m);

                    b.Property<Guid>("ServiceTypeId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ServiceTypeId", "DurationHours")
                        .IsUnique();

                    b.ToTable("DurationPrice");
                });

            modelBuilder.Entity("CleanService.Src.Models.Helpers", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("AverageRating")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)")
                        .HasDefaultValue(0m);

                    b.Property<int>("CancelledJobs")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("CompletedJobs")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 944, DateTimeKind.Local).AddTicks(135));

                    b.Property<string>("ExperienceDescription")
                        .HasColumnType("longtext");

                    b.Property<decimal>("HourlyRate")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("ResumeUploaded")
                        .HasColumnType("longtext");

                    b.Property<string>("ServicesOffered")
                        .HasColumnType("longtext")
                        .HasComment("Array of service_type_ids");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 944, DateTimeKind.Local).AddTicks(1745));

                    b.HasKey("Id");

                    b.ToTable("Helpers", t =>
                        {
                            t.HasComment("Additional information for users' type is helper");
                        });
                });

            modelBuilder.Entity("CleanService.Src.Models.Notifications", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 948, DateTimeKind.Local).AddTicks(1798));

                    b.Property<bool>("IsRead")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<Guid?>("ReferenceId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("CleanService.Src.Models.RoomPricing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("AdditionalPrice")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasDefaultValue(0m);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 945, DateTimeKind.Local).AddTicks(1452));

                    b.Property<int>("RoomCount")
                        .HasColumnType("int")
                        .HasComment("0 represents studio");

                    b.Property<string>("RoomType")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.Property<Guid>("ServiceTypeId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ServiceTypeId", "RoomType", "RoomCount")
                        .IsUnique();

                    b.ToTable("RoomPricing");
                });

            modelBuilder.Entity("CleanService.Src.Models.ServiceCategories", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 944, DateTimeKind.Local).AddTicks(6572));

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ServiceCategory");
                });

            modelBuilder.Entity("CleanService.Src.Models.ServiceTypes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 944, DateTimeKind.Local).AddTicks(9161));

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasComment("e.g., 'Standard Cleaning', 'Deep Clean'");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name", "CategoryId")
                        .IsUnique();

                    b.ToTable("ServiceTypes");
                });

            modelBuilder.Entity("CleanService.Src.Models.Users", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 943, DateTimeKind.Local).AddTicks(1776));

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Gender")
                        .HasColumnType("varchar(24)");

                    b.Property<string>("IdentityCard")
                        .HasColumnType("longtext");

                    b.Property<string>("NotificationToken")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(24)")
                        .HasDefaultValue("Active");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 10, 30, 10, 12, 34, 943, DateTimeKind.Local).AddTicks(3221));

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CleanService.Src.Models.BlacklistedUsers", b =>
                {
                    b.HasOne("CleanService.Src.Models.Users", "BlacklistedByUser")
                        .WithMany("BlacklistedByUsers")
                        .HasForeignKey("BlacklistedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanService.Src.Models.Users", "User")
                        .WithMany("BlacklistedUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BlacklistedByUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CleanService.Src.Models.BookingDetails", b =>
                {
                    b.HasOne("CleanService.Src.Models.Bookings", "Booking")
                        .WithOne("BookingDetails")
                        .HasForeignKey("CleanService.Src.Models.BookingDetails", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("CleanService.Src.Models.Bookings", b =>
                {
                    b.HasOne("CleanService.Src.Models.Users", "Customer")
                        .WithMany("CustomerBookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanService.Src.Models.Users", "Helper")
                        .WithMany("HelperBookings")
                        .HasForeignKey("HelperId");

                    b.HasOne("CleanService.Src.Models.ServiceTypes", "ServiceType")
                        .WithMany("Bookings")
                        .HasForeignKey("ServiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Helper");

                    b.Navigation("ServiceType");
                });

            modelBuilder.Entity("CleanService.Src.Models.Complaints", b =>
                {
                    b.HasOne("CleanService.Src.Models.Bookings", "Booking")
                        .WithMany("Complaints")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanService.Src.Models.Users", "ReportedBy")
                        .WithMany("CreatedComplaints")
                        .HasForeignKey("ReportedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanService.Src.Models.Users", "ReportedUser")
                        .WithMany("ReceivedComplaints")
                        .HasForeignKey("ReportedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("ReportedBy");

                    b.Navigation("ReportedUser");
                });

            modelBuilder.Entity("CleanService.Src.Models.Contracts", b =>
                {
                    b.HasOne("CleanService.Src.Models.Bookings", "Booking")
                        .WithOne("Contract")
                        .HasForeignKey("CleanService.Src.Models.Contracts", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("CleanService.Src.Models.DurationPrice", b =>
                {
                    b.HasOne("CleanService.Src.Models.ServiceTypes", "ServiceType")
                        .WithMany("DurationPrice")
                        .HasForeignKey("ServiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceType");
                });

            modelBuilder.Entity("CleanService.Src.Models.Helpers", b =>
                {
                    b.HasOne("CleanService.Src.Models.Users", "User")
                        .WithOne("Helper")
                        .HasForeignKey("CleanService.Src.Models.Helpers", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CleanService.Src.Models.Notifications", b =>
                {
                    b.HasOne("CleanService.Src.Models.Users", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CleanService.Src.Models.RoomPricing", b =>
                {
                    b.HasOne("CleanService.Src.Models.ServiceTypes", "ServiceType")
                        .WithMany("RoomPricing")
                        .HasForeignKey("ServiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceType");
                });

            modelBuilder.Entity("CleanService.Src.Models.ServiceTypes", b =>
                {
                    b.HasOne("CleanService.Src.Models.ServiceCategories", "Category")
                        .WithMany("ServiceTypes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CleanService.Src.Models.Bookings", b =>
                {
                    b.Navigation("BookingDetails")
                        .IsRequired();

                    b.Navigation("Complaints");

                    b.Navigation("Contract")
                        .IsRequired();
                });

            modelBuilder.Entity("CleanService.Src.Models.ServiceCategories", b =>
                {
                    b.Navigation("ServiceTypes");
                });

            modelBuilder.Entity("CleanService.Src.Models.ServiceTypes", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("DurationPrice");

                    b.Navigation("RoomPricing");
                });

            modelBuilder.Entity("CleanService.Src.Models.Users", b =>
                {
                    b.Navigation("BlacklistedByUsers");

                    b.Navigation("BlacklistedUsers");

                    b.Navigation("CreatedComplaints");

                    b.Navigation("CustomerBookings");

                    b.Navigation("Helper");

                    b.Navigation("HelperBookings");

                    b.Navigation("Notifications");

                    b.Navigation("ReceivedComplaints");
                });
#pragma warning restore 612, 618
        }
    }
}
