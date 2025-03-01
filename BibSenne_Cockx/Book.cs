using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibSenne_Cockx
{
    internal class Book
    {
        // attributen
        private string title;
        private string author;
        private string isbnNumber;
        private int pages;
        private Genres genre;
        private int publishYear;
        private Types type;
        private int rating;

        // properties
        public string Title
        {
            get { return title; }
        }

        public string Author {
            get { return author; } 
        }

        public string IsbnNumber
        {
            get { return isbnNumber; }
            set {
                if (value.Length != 13 || !value.StartsWith("978") && !value.StartsWith("979"))
                {
                    Console.WriteLine("Geef een geldig ISBN nummer op!");
                }
                else
                {
                    isbnNumber = value;
                }
            }
        }

        public int Pages
        {
            get { return pages; }
            set {
                if (value > 0)
                {
                    pages = value;
                }
                else
                {
                    Console.WriteLine("Geef een geldig aantal paginas op!");
                }
            }
        }

        public Types Type
        {
            get { return type; }
        }

        public int PublishYear
        {
            get { return publishYear; }
        }

        public Genres Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        public int Rating
        {
            get { return rating; }
            set
            {
                if (value < 10 && value >= 0)
                {
                    rating = value;
                }
                else
                {
                    Console.WriteLine($"Geef een geldige beoordeling op 10!");
                }
            }
        }

        // constructors
        public Book(string title, string author, string isbnNumber, int pages, Genres genre, int publishYear, Types type, int rating, Library library)
        {
            this.title = title;
            this.author = author;
            library.AddBook(this);
        }

        // methoden
        public void ShowInfo()
        {
            Console.WriteLine($"Info over het boek: {Title}");
            Console.WriteLine($"Auteur: {Author}");
            Console.WriteLine($"ISBN-nummer: {IsbnNumber}");
            Console.WriteLine($"Type boek: {Type}");
            Console.WriteLine($"Aantal pagina's: {Pages}");
            Console.WriteLine($"Jaar van uitgave: {PublishYear}"); 
            Console.WriteLine($"Genre: {Type}");
            Console.WriteLine($"Beoordeling: {Rating}/10");
        }
    }
}
