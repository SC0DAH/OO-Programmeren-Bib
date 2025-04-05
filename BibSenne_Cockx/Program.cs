namespace BibSenne_Cockx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library apBibliotheek = new Library("AP Bib");
            bool isRunning = true;
            string title, author;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Welkom bij de Bibliotheek!");
                Console.WriteLine("Kies een optie:");
                Console.WriteLine("1. Voeg een boek toe aan de bibliotheek op basis van titel en auteur");
                Console.WriteLine("2. Voeg informatie toe aan een boek");
                Console.WriteLine("3. Toon alle info van een boek op basis van titel en auteur");
                Console.WriteLine("4. Zoek een boek op verschillende manieren");
                Console.WriteLine("5. Verwijder een boek uit de bibliotheek");
                Console.WriteLine("6. Toon alle boeken in de bibliotheek");
                Console.WriteLine("7. Lees boeken in vanuit een CSV-bestand");
                Console.WriteLine("8. Voeg een krant of maandblad toe");
                Console.WriteLine("9. Bekijk alle kranten");
                Console.WriteLine("10. Bekijk alle maandbladen");
                Console.WriteLine("11. Aanwinsten van de leeszaal bekijken");
                Console.WriteLine("12. Verlaat de applicatie.");
                string choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Wat is de naam van het boek?");
                        title = Console.ReadLine();
                        Console.WriteLine("Wie is de auteur?");
                        author = Console.ReadLine();
                        Book newBook = new Book(title, author, apBibliotheek);
                        PressKey();
                        break;
                    case "2":
                        Console.Write("Geef de titel in van het boek dat je wilt zoeken: ");
                        title = Console.ReadLine();
                        Console.Write("Geef de titel in van het boek dat je wilt zoeken: ");
                        author = Console.ReadLine();
                        Book bookToAddInfo  = apBibliotheek.SearchBookByTitleAndAuthor(title, author);
                        if (bookToAddInfo != null)
                        {
                            Console.WriteLine("Geef het ISBN nummer in: ");
                            bookToAddInfo.IsbnNumber = Console.ReadLine();
                            Console.WriteLine("Geef het aantal pagina's in: ");
                            bookToAddInfo.Pages = Convert.ToInt32(Console.ReadLine());

                            bool goodInput = false;
                            while (!goodInput)
                            {
                                Console.WriteLine("Geef het type van het boek in (Hardcover, EBook, Audioboek): ");
                                string inputType = Console.ReadLine();
                                if (Enum.TryParse(inputType, out Types type))
                                {
                                    bookToAddInfo.Type = type;
                                    goodInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Ongeldige invoer voor het type boek! Het moet 'Hardcover', 'EBook' of 'Audioboek' zijn.");
                                }
                            }
                            

                            Console.WriteLine("Geef het publicatiejaar in: ");
                            bookToAddInfo.PublishYear = Convert.ToInt32(Console.ReadLine());

                            goodInput = false;
                            while (!goodInput)
                            {
                                Console.WriteLine("Geef het genre van het boek in : ");
                                string inputGenre = Console.ReadLine();
                                if (Enum.TryParse(inputGenre, out Genres genre))
                                {
                                    bookToAddInfo.Genre = genre;
                                    goodInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Ongeldige invoer voor het type boek! Het moet 'Hardcover', 'EBook' of 'Audioboek' zijn.");
                                }
                            }

                            Console.WriteLine("Geef een beoordeling in (0-10): ");
                            bookToAddInfo.Rating = Convert.ToInt32(Console.ReadLine());

                        }
                        else
                        {
                            Console.WriteLine("Het boek dat je zocht is niet gevonden!");
                        }
                        PressKey();
                        break;
                    case "3":
                        Console.Write("Geef de titel van het boek in: ");
                        string searchTitle = Console.ReadLine();
                        Console.Write("Geef de auteur van het boek in: ");
                        string searchAuthor = Console.ReadLine();

                        Book foundBook = apBibliotheek.SearchBookByTitleAndAuthor(searchTitle, searchAuthor);
                        if (foundBook != null)
                        {
                            foundBook.ShowInfo();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Boek niet gevonden.");
                            Console.ResetColor();
                        }
                        PressKey();
                        break;

                    case "4":
                        Console.WriteLine("Zoek een boek op basis van:");
                        Console.WriteLine("1. Titel en auteur");
                        Console.WriteLine("2. ISBN");
                        Console.WriteLine("3. Auteur");
                        Console.WriteLine("4. Andere eigenschappen");

                        string searchChoice = Console.ReadLine();
                        switch (searchChoice)
                        {
                            case "1":
                                Console.Write("Titel: ");
                                string titleSearch = Console.ReadLine();
                                Console.Write("Auteur: ");
                                string authorSearch = Console.ReadLine();
                                Book bookByTitleAndAuthor = apBibliotheek.SearchBookByTitleAndAuthor(titleSearch, authorSearch);
                                if (bookByTitleAndAuthor != null) bookByTitleAndAuthor.ShowInfo();
                                else Console.WriteLine("Geen boek gevonden.");
                                break;

                            case "2":
                                Console.Write("ISBN: ");
                                string isbnSearch = Console.ReadLine();
                                Book bookByIsbn = apBibliotheek.SearchBookByISBN(isbnSearch);
                                if (bookByIsbn != null) bookByIsbn.ShowInfo();
                                else Console.WriteLine("Geen boek gevonden.");
                                break;

                            case "3":
                                Console.Write("Auteur: ");
                                string authorToSearch = Console.ReadLine();
                                List<Book> booksByAuthor = apBibliotheek.SearchAllBooksFromAuthor(authorToSearch);
                                if (booksByAuthor != null)
                                {
                                    foreach (var book in booksByAuthor)
                                    {
                                        book.ShowInfo();
                                    }
                                }
                                else Console.WriteLine("Geen boeken gevonden.");
                                break;

                            case "4":
                                Console.Write("Minimaal aantal pagina’s: ");
                                int minPages = int.Parse(Console.ReadLine());

                                List<Book> booksByCriteria = apBibliotheek.SearchBooks(minPages: minPages);
                                if (booksByCriteria.Count > 0)
                                {
                                    foreach (var book in booksByCriteria)
                                    {
                                        book.ShowInfo();
                                    }
                                }
                                else Console.WriteLine("Geen boeken gevonden.");
                                break;

                            default:
                                Console.WriteLine("Ongeldige keuze.");
                                break;
                        }
                        PressKey();
                        break;

                    case "5":
                        Console.Write("Geef de titel van het boek in dat je wilt verwijderen: ");
                        string removeTitle = Console.ReadLine();
                        Console.Write("Geef de auteur van het boek in: ");
                        string removeAuthor = Console.ReadLine();

                        Book bookToRemove = apBibliotheek.SearchBookByTitleAndAuthor(removeTitle, removeAuthor);
                        if (bookToRemove != null)
                        {
                            apBibliotheek.RemoveBook(bookToRemove);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Boek niet gevonden.");
                            Console.ResetColor();
                        }
                        PressKey();
                        break;

                    case "6":
                        if (apBibliotheek.BookList.Count == 0)
                        {
                            Console.WriteLine("De bibliotheek bevat momenteel geen boeken.");
                        }
                        else
                        {
                            Console.WriteLine("Boeken in de bibliotheek:");
                            foreach (Book book in apBibliotheek.BookList)
                            {
                                book.ShowInfo();
                                Console.WriteLine("---------------------------------");
                            }
                        }
                        PressKey();
                        break;

                    case "7":
                        Console.Write("Geef het pad van het CSV-bestand in: ");
                        string csvPath = Console.ReadLine();
                        if (File.Exists(csvPath))
                        {
                            apBibliotheek.ReadStudentsFromCSV(csvPath);
                            Console.WriteLine("CSV-bestand succesvol ingelezen en boeken toegevoegd aan de bibliotheek.");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Het opgegeven bestand bestaat niet.");
                            Console.ResetColor();
                        }
                        PressKey();
                        break;
                    case "8":
                        Console.WriteLine("Wil je een krant of een maandblad toevoegen? (K/M)");
                        string addChoice = Console.ReadLine().ToUpper();
                        if (addChoice == "K")
                        {
                            apBibliotheek.AddNewspaper();
                        }
                        else if(addChoice == "M")
                        {
                            apBibliotheek.AddMagazine();
                        }
                        else
                        {
                            Console.WriteLine("Foute keuze");
                        }
                        PressKey();
                        break;
                    case "9":
                        apBibliotheek.ShowAllNewspapers();
                        PressKey();
                        break;
                    case "10":
                        apBibliotheek.ShowAllMagazines();
                        PressKey();
                        break;
                    case "11":
                        apBibliotheek.AcquisitionsReadingRoomToday(DateTime.Now);
                        PressKey();
                        break;
                    case "12":
                        Console.WriteLine("Tot ziens!");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Ongeldige keuze, probeer het opnieuw.");
                        break;
                }
            }
        }

        public static void PressKey()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Druk een toets om verder te gaan");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
