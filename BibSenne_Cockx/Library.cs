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
            else
            {
                return null;
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

        public void AddNewspaper()
        {
            Console.WriteLine("Wat is de naam van de krant?");
            string newspaperName = Console.ReadLine();
            Console.WriteLine("Wat is de datum van de krant?");
            DateTime date = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Wat is de uitgeverij van de krant?");
            string publisher = Console.ReadLine();
            NewsPaper newsPaper = new NewsPaper(newspaperName, publisher, date);
            allReadingRoom.Add(DateTime.Now, newsPaper);
        }

        public void AddMagazine()
        {
            Console.WriteLine("Wat is de naam van het maandblad?");
            string magazineName = Console.ReadLine();
            Console.WriteLine("Wat is de maand van het maandblad?");
            byte month = Convert.ToByte(Console.ReadLine());
            Console.WriteLine("Wat is het jaar van het maandblad?");
            uint year = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Wat is de uitgeverij van het maadnblad?");
            string publisher = Console.ReadLine();
            Magazine magazine = new Magazine(magazineName, publisher, month, year);
            allReadingRoom.Add(DateTime.Now, magazine);
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
