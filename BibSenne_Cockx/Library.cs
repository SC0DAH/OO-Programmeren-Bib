using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibSenne_Cockx
{
    internal class Library
    {
        // attributen
        private string name;
        private List<Book> bookList;

        // properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Book> BookList
        {
            get { return bookList; }
        }

        // constructors
        public Library(string name)
        {
            Name = name;
            bookList = new List<Book>();
        }

        // methoden

        public void AddBook(Book newBook)
        {
            if (!(newBook is null))
            {
                bookList.Add(newBook);
            }
        }

        public void RemoveBook(Book bookToRemove)
        {
            if (bookToRemove == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kan geen null-boek verwijderen.");
                Console.ResetColor();
                return;
            }

            if (bookList.Contains(bookToRemove))
            {
                bookList.Remove(bookToRemove);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Boek succesvol verwijderd.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Boek niet gevonden in de bibliotheek.");
            }
            Console.ResetColor();
        }

        public Book SearchBookByTitleAndAuthor(string title, string author)
        {
            foreach (Book book in bookList)
            {
                if (book.Title == title && book.Author == author)
                {
                    return book;
                }
            }
            return null;
        }

        public Book SearchBookByISBN(string isbn)
        {
            foreach (Book book in bookList)
            {
                if (book.IsbnNumber == isbn)
                {
                    return book;
                }
            }
            return null;
        }

        public List<Book> SearchAllBooksFromAuthor(string author)
        {
            List<Book> allBooksFromAuthor = new List<Book>();
            foreach (Book book in bookList)
            {
                if (book.Author == author)
                {
                    allBooksFromAuthor.Add(book);
                }
            }
            if (allBooksFromAuthor.Count > 0)
            {
                return allBooksFromAuthor;
            }
            else
            {
                return null; // geen boeken gevonden
            }
        }

        public List<Book> SearchBooks(string title = null, string author = null, string isbnNumber = null,
                                   int? minPages = null, Genres? genre = null, int? publishYear = null,
                                   Types? type = null, int? rating = null)
        {
            List<Book> foundBooks = new List<Book>();

            foreach (Book book in bookList)
            {
                bool matches = true;

                if (title != null && book.Title == title)
                {
                    matches = true;
                }
                if (author != null && book.Author == author)
                {
                    matches = true;
                }
                if (isbnNumber != null && book.IsbnNumber == isbnNumber)
                {
                    matches = true;
                }
                if (minPages != null && book.Pages >= minPages)
                {
                    matches = true;
                }
                if (genre != null && book.Genre == genre)
                {
                    matches = true;
                }
                if (publishYear != null && book.PublishYear == publishYear)
                {
                    matches = true;
                }
                if (type != null && book.Type == type)
                {
                    matches = true;
                }
                if (rating != null && book.Rating == rating)
                {
                    matches = true;
                }

                if (matches)
                {
                    foundBooks.Add(book);
                }
            }

            return foundBooks;
        }

        public void ReadStudentsFromCSV(string path)
        {
            string[] lines = File.ReadAllLines(path);
            Book[] books = new Book[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(";");

                string title = values[0];
                string author = values[1];

                books[i] = new Book(title, author, this);
            }
        }
    }
}
