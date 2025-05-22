using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibSenne_Cockx
{
    internal class Book : ILendable
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
                    throw new InvalidIsbnException("ISBN moet 13 cijfers bevatten en starten met 978 of 979.");
                }
                isbnNumber = value;
            }
        }

        public int Pages
        {
            get { return pages; }
            set {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("", "Aantal pagina's moet groter zijn dan 0.");
                }
                pages = value;
            }
        }

        public Types Type
        {
            get { return type; }
            set { type = value; }
        }

        public int PublishYear
        {
            get { return publishYear; }
            set 
            {
                if (value > DateTime.Now.Year)
                {
                    throw new InvalidYearException("Publicatiejaar kan niet in de toekomst liggen.");
                }
                publishYear = value;
            }
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
                if (value < 0 || value > 10)
                {
                    throw new ArgumentException("Rating moet tussen 0 en 10 liggen.");
                }
                rating = value;
            }
        }

        public bool IsAvailable { get; set; }
        public DateTime BorrowingDate { get; set; }
        public int BorrowDays { get; set; }

        // constructors
        public Book(string title, string author, Library library)
        {
            IsAvailable = true;
            this.title = title;
            this.author = author;
            library.AddBook(this);
        }

        // methoden
        public void ShowInfo()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Info over het boek: {Title}");
            Console.WriteLine($"Auteur: {Author}");
            Console.WriteLine($"ISBN-nummer: {IsbnNumber}");
            Console.WriteLine($"Type boek: {Type}");
            Console.WriteLine($"Aantal pagina's: {Pages}");
            Console.WriteLine($"Jaar van uitgave: {PublishYear}"); 
            Console.WriteLine($"Genre: {Type}");
            Console.WriteLine($"Beoordeling: {Rating}/10");
            Console.WriteLine("---------------------------------");
        }

        public void Borrow()
        {
            if (IsAvailable)
            {
                BorrowingDate = DateTime.Now;
                IsAvailable = false;
                if (Genre == Genres.Schoolboek)
                {
                    BorrowDays = 10;
                }
                else
                {
                    BorrowDays = 20;
                }
                DateTime dueDate = BorrowingDate.AddDays(BorrowDays);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Boek '{Title}' werd uitgeleend. Terugbrengen uiterlijk op: {dueDate:dd/MM/yyyy}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Boek '{Title}' is momenteel niet beschikbaar.");
            }
            Console.ResetColor();
        }

        public void Return()
        {
            if (!IsAvailable)
            {
                DateTime dueDate = BorrowingDate.AddDays(BorrowDays);
                DateTime returnDate = DateTime.Now;
                IsAvailable = true;

                if (returnDate > dueDate)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Boek '{Title}' is te laat teruggebracht op {returnDate:dd/MM/yyyy}. Deadline was {dueDate:dd/MM/yyyy}.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Boek '{Title}' werd op tijd teruggebracht op {returnDate:dd/MM/yyyy}.");
                }
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"Boek '{Title}' was niet uitgeleend.");
            }
        }
    }
}
