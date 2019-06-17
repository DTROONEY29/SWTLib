using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LibraryServices
{
    public class BookService : IBook
    {
        private LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(Book newBook)
        {
            _context.Add(newBook);
            _context.SaveChanges();
        }

        public void AddRange(IEnumerable<Book> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> Find(Expression<Func<Book, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books
                .Include(b => b.Location);
        }     

        public Author GetAuthor(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Author> GetAuthors(int id)
        {
            return _context.Authors.ToList();
        }

        public BookAuthor GetBookAuthor(int id)
        {
            return _context.BookAuthors.FirstOrDefault();
        }

        public BookCategory GetBookCategory(int id)
        {
            throw new NotImplementedException();
        }

        public BookKeyword GetBookKeyword(int id)
        {
            throw new NotImplementedException();
        }

        public Book GetById(int id)
        {
            return
                GetAll()
                .FirstOrDefault(book => book.Id == id);
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public string GetDescription(int id)
        {
            return _context.Books
                .FirstOrDefault(a => a.Id == id)
                .Description;
        }

        public int GetIsbn(int id)
        {
            return _context.Books
               .FirstOrDefault(a => a.Id == id)
               .ISBN;
        }

        public Keyword GetKeyword(int id)
        {
            throw new NotImplementedException();
        }

        public string GetLanguage(int id)
        {
            return _context.Books
                .FirstOrDefault(a => a.Id == id)
                .Language;
        }

        public Location GetLocation(int id)
        {
            return GetById(id).Location;
        }

        public string GetPublisher(int id)
        {
            return _context.Books
                .FirstOrDefault(a => a.Id == id)
                .Publisher;
        }

        public string GetTitle(int id)
        {
            return _context.Books
                .FirstOrDefault(a => a.Id == id)
                .Title;
        }

        public int GetYear(int id)
        {
            return _context.Books
                .FirstOrDefault(a => a.Id == id)
                .Year;
        }

        public void Remove(Book entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Book> entities)
        {
            throw new NotImplementedException();
        }
    }
}
