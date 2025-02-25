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
        private string titel;
        private string author;
        private string isbnNumber;
        private int pages;
        private string genre;
        private int publishYear;
        private string language;
        private Genres type;

        // properties
        public string Titel
        {
            get { return Titel; }
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

        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        public int PublishYear
        {
            get { return publishYear; }
            set { publishYear = value; }
        }

        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        public Genres Type
        {
            get { return type; }
            set { type = value; }
        }

        // constructors
        public Book(string titel, string author)
        {
            this.titel = titel;
            this.author = author;
            Library.bookList.Add(this);
        }
    }
}
