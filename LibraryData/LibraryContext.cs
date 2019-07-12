using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryData
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookKeyword> BookKeywords { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Rating> Ratings { get; set; }


        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(c => new { c.BookId, c.AuthorId });

            modelBuilder.Entity<BookCategory>()
                .HasKey(c => new { c.BookId, c.CategoryId });

            modelBuilder.Entity<BookKeyword>()
                .HasKey(c => new { c.BookId, c.KeywordId });

            modelBuilder.Entity<Book>()
                .Property(s => s.Status)
                .HasDefaultValue(false);

            modelBuilder.Entity<Book>()
                .Property(s => s.LocationId)
                .HasDefaultValue(0);

            modelBuilder.Entity<Rental>()
                .Property(s => s.ExtendedRental)
                .HasDefaultValue(false);

            modelBuilder.Entity<Bookmark>()
                .HasKey(c => new { c.UserId, c.BookId });

            modelBuilder.Entity<Rating>()
                .HasKey(c => new { c.UserId, c.BookId });

            modelBuilder.Entity<Reminder>()
                .HasKey(c => new { c.UserId, c.BookId });
        }
    }
}
