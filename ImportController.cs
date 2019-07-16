using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using SWTlib.Models.ViewModels;

namespace SWTlib.Controllers
{
    public class ImportController : Controller
    {
        private readonly LibraryContext _context;
        public ImportController(LibraryContext context)
        {
            _context = context;

        }


        public IActionResult Index()
        {
            FileReader();
            return View();
        }

        public IActionResult Privacy()
        {

            return View();

        }
                          
        public void FileReader()
        {
            //XML reader setup
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            using (XmlReader reader = XmlReader.Create(@"C:\Users\09Jer\Documents\Uni\file.xml", settings))
            {
                //Setup of data needed from the xml file to be parsed
                List<string> title = new List<string>();
                List<string[]> authors = new List<string[]>();
                List<string> isbn = new List<string>();
                List<string> language = new List<string>();
                List<string> publisher = new List<string>();
                List<int> year = new List<int>();
                while (reader.Read()) //itterates through the xml file
                {


                    if (reader.IsStartElement()) //if not an empty element
                    {



                        switch (reader.Name.ToString())
                        {
                            case "key": //if the element has the label "key" do the following
                                //compares the current element with the needed data labels
                                String current = reader.ReadString();
                                if (current == "title")
                                {

                                    reader.ReadToFollowing("string"); //itterates to the following string
                                    title.Add(reader.ReadString());     //adds the current string to a list of titles

                                }
                                else if (current == "creatorsCompositeString")
                                {

                                    reader.ReadToFollowing("string");
                                    String author = reader.ReadString();
                                    authors.Add(author.Split("\n")); //splits the single composite string of authors into an array of individual author strings

                                }
                                else if (current == "isbn")
                                {

                                    reader.ReadToFollowing("string");
                                    string testing = reader.ReadString();
                                    
                                    
                                    
                                    isbn.Add(testing);

                                }
                                else if (current == "languagesCompositeString")
                                {

                                    reader.ReadToFollowing("string");
                                    language.Add(reader.ReadString());

                                }
                                else if (current == "publishersCompositeString")
                                {

                                    reader.ReadToFollowing("string");
                                    publisher.Add(reader.ReadString());
                                }
                                else if (current == "publishDate")
                                {
                                    reader.ReadToFollowing("date");
                                    String dateString = reader.ReadString();
                                    year.Add(Convert.ToInt32(dateString.Substring(0, 4)));       

                                }
                                break;

                        }
                        
                    }
                }
                int i = 0;
                
                var allAuthors = new List<int>(); //A lsit to store all id's of books to be added
                var isbnlist = new List<string>();
                foreach (string[] s in authors)
                {
                    if (isbnlist.Contains(isbn[i]).Equals(false)) //checks if isbn has been added already
                    {

                        
                        var authorsIdList = new List<int>(); //list to store author ids for current book
                        Book book = new Book { Title = title[i], ISBN = isbn[i], Language = language[i], Publisher = publisher[i], Year = year[i], LocationId = 1}; //creates book and assigns values
                        isbnlist.Add(isbn[i]);
                        List<string> booksAuthors = new List<string>(); //A list to store the authors of the current book
                        foreach (string n in s)
                        {
                            booksAuthors.Add(n); //add all individual authors of the book to the list
                        }
                        foreach (var item in booksAuthors) //for every author of the current book
                        {
                            int j = 0;
                            foreach (var id in allAuthors) //check the id of the author against the saved authors
                            {
                                if (id == int.Parse(item))
                                {
                                    j++;
                                }
                            }

                            if (j > 0) //if the id is already in the list
                            {
                                authorsIdList.Add(int.Parse(item)); //add id to list
                                allAuthors.Add(int.Parse(item));
                            }
                            else //if the author  is a new one
                            {
                                var newAuthor = new Author { AuthorName = item }; //add new author to database
                                _context.Authors.Add(newAuthor);
                                _context.SaveChanges();
                                var newAuthorId = _context.Authors.Find(newAuthor.Id);      //Get the Id of the created author and add it to authorsList.
                                authorsIdList.Add(newAuthorId.Id);

                            }


                        }
                        book.BookAuthors = new List<BookAuthor>();
                        foreach (var author in authorsIdList) //for every author id of the current book
                        {
                            var authorToAdd = new BookAuthor { BookId = book.Id, AuthorId = author }; //add to join table
                            book.BookAuthors.Add(authorToAdd);
                        }




                        if (ModelState.IsValid)
                        {
                             _context.Add(book);
                            _context.SaveChanges();

                        }
                }
                    i++;
                }
                Console.WriteLine(i);


            }


         }

        }
    }
