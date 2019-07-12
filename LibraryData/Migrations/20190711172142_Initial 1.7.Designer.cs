﻿// <auto-generated />
using System;
using LibraryData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryData.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20190711172142_Initial 1.7")]
    partial class Initial17
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LibraryData.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorName");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("LibraryData.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Language");

                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<string>("Publisher");

                    b.Property<int>("RatingDown");

                    b.Property<int>("RatingUp");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int?>("Year");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LibraryData.Models.BookAuthor", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("AuthorId");

                    b.HasKey("BookId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("LibraryData.Models.BookCategory", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("CategoryId");

                    b.HasKey("BookId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BookCategories");
                });

            modelBuilder.Entity("LibraryData.Models.BookKeyword", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("KeywordId");

                    b.HasKey("BookId", "KeywordId");

                    b.HasIndex("KeywordId");

                    b.ToTable("BookKeywords");
                });

            modelBuilder.Entity("LibraryData.Models.Bookmark", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("BookId");

                    b.HasKey("UserId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("Bookmarks");
                });

            modelBuilder.Entity("LibraryData.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("LibraryData.Models.Keyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("KeywordName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("LibraryData.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LocationName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("LibraryData.Models.Rating", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("BookId");

                    b.HasKey("UserId", "BookId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("LibraryData.Models.Reminder", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("BookId");

                    b.HasKey("UserId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("Reminders");
                });

            modelBuilder.Entity("LibraryData.Models.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<bool>("ExtendedRental")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime>("RentalDate");

                    b.Property<DateTime>("ReturnDate");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BookId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("LibraryData.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsChairMember");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LibraryData.Models.Book", b =>
                {
                    b.HasOne("LibraryData.Models.Location", "Location")
                        .WithMany("Books")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LibraryData.Models.BookAuthor", b =>
                {
                    b.HasOne("LibraryData.Models.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LibraryData.Models.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LibraryData.Models.BookCategory", b =>
                {
                    b.HasOne("LibraryData.Models.Book", "Book")
                        .WithMany("BookCategories")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LibraryData.Models.Category", "Category")
                        .WithMany("BookCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LibraryData.Models.BookKeyword", b =>
                {
                    b.HasOne("LibraryData.Models.Book", "Book")
                        .WithMany("BookKeywords")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LibraryData.Models.Keyword", "Keyword")
                        .WithMany("BookKeywords")
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LibraryData.Models.Bookmark", b =>
                {
                    b.HasOne("LibraryData.Models.Book", "Book")
                        .WithMany("Bookmarks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LibraryData.Models.User", "User")
                        .WithMany("Bookmarks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LibraryData.Models.Reminder", b =>
                {
                    b.HasOne("LibraryData.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LibraryData.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LibraryData.Models.Rental", b =>
                {
                    b.HasOne("LibraryData.Models.Book", "Book")
                        .WithOne("Rental")
                        .HasForeignKey("LibraryData.Models.Rental", "BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LibraryData.Models.User")
                        .WithMany("Rentals")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
