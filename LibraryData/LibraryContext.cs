﻿using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryData
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookKeyword> BookKeywords { get; set; }


        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(c => new { c.BookId, c.AuthorId });

            modelBuilder.Entity<BookCategory>()
                .HasKey(c => new { c.BookId, c.CategoryId });

            modelBuilder.Entity<BookKeyword>()
                .HasKey(c => new { c.BookId, c.KeywordId });
        }
    }
}
