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
    }
}
