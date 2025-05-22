using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibSenne_Cockx
{
    internal class Library
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<Book> bookList;

        public List<Book> BookList
        {
            get { return bookList; }
        }

        private static Dictionary<DateTime, ReadingRoomItem> allReadingRoom = new Dictionary<DateTime, ReadingRoomItem>();

        public static Dictionary<DateTime, ReadingRoomItem> AllReadingRoom
        {
            get { return allReadingRoom; }
        }


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
            return null;
        }

        public List<Book> SearchBooksByPages(int minPages)
        {
            List<Book> foundBooks = new List<Book>();

            foreach (Book book in bookList)
            {
                if (book.Pages >= minPages)
                {
                    foundBooks.Add(book);
                }
            }
            if (foundBooks.Count > 0)
            {
                return foundBooks;
            }
            return null;
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

        public void AddNewspaper()
        {
            Console.WriteLine("Wat is de naam van de krant?");
            string newspaperName = Console.ReadLine();
            Console.WriteLine("Wat is de datum van de krant?");
            DateTime date;
            try
            {
                date = DateTime.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Je moet een geldige datum ingeven! Probeer later opnieuw");
                return;
            }
            
            Console.WriteLine("Wat is de uitgeverij van de krant?");
            string publisher = Console.ReadLine();
            try
            {
                NewsPaper newsPaper = new NewsPaper(newspaperName, publisher, date);
                allReadingRoom.Add(DateTime.Now, newsPaper);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Krant niet toegevoegd!: " + ex.Message);
                Console.ResetColor();
            }
            
            
        }

        public void AddMagazine()
        {
            Console.WriteLine("Wat is de naam van het maandblad?");
            string magazineName = Console.ReadLine();
            Console.WriteLine("Wat is de maand van het maandblad?");
            byte month;
            try
            {
                month = Convert.ToByte(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Je moet een geldige maand invoeren! Probeer later opnieuw");
                return;
            }
            
            Console.WriteLine("Wat is het jaar van het maandblad?");
            uint year;
            try
            {
                year = Convert.ToUInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Je moet een geldig jaar invoeren! Probeer later opnieuw");
                return;
            }
            
            Console.WriteLine("Wat is de uitgeverij van het maandblad?");
            string publisher = Console.ReadLine();
            try
            {
                Magazine magazine = new Magazine(magazineName, publisher, month, year);
                allReadingRoom.Add(DateTime.Now, magazine);
            }
            catch (ArgumentOutOfRangeException aoex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Magazine niet toegevoegd!: " + aoex.Message);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Onverwachte fout: " + ex.Message);
            }
        }

        public void ShowAllMagazines()
        {
            Console.WriteLine("Alle magazines uit de leeszaal:");
            foreach (var readingRoomItem in allReadingRoom)
            {
                if (readingRoomItem.Value is Magazine)
                {
                    Console.WriteLine($"- {readingRoomItem.Value.Title} van {((Magazine)readingRoomItem.Value).Month}/{((Magazine)readingRoomItem.Value).Year} van uitgeverij {readingRoomItem.Value.Publisher}");
                }
            }
        }

        public void ShowAllNewspapers()
        {
            Console.WriteLine("Alle kranten uit de leeszaal:");
            foreach (var readingRoomItem in allReadingRoom)
            {
                if (readingRoomItem.Value is NewsPaper)
                {
                    Console.WriteLine($"- {readingRoomItem.Value.Title} van {((NewsPaper)readingRoomItem.Value).Date.ToString("D")} van uitgeverij {readingRoomItem.Value.Publisher}");
                }
            }
        }

        public void AcquisitionsReadingRoomToday(DateTime date)
        {
            Console.WriteLine($"Aanwinsten in de leeszaal van {date.ToString("D")}");
            foreach (var readingRoomItem in allReadingRoom)
            {
                if (readingRoomItem.Key.Date == date.Date)
                {
                    Console.WriteLine($"{readingRoomItem.Value.Title} met id {readingRoomItem.Value.Identification}");
                }
            }
        }
    }
}
