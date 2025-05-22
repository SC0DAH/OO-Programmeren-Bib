namespace BibSenne_Cockx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library apBibliotheek = new Library("AP Bibliotheek");
            bool isRunning = true;
            string title, author;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine($"Welkom bij de {apBibliotheek.Name}!");
                Console.WriteLine("Kies een optie:");
                Console.WriteLine("******* BOEKEN *******");
                Console.WriteLine("1. Voeg een boek toe aan de bibliotheek op basis van titel en auteur");
                Console.WriteLine("2. Voeg informatie toe aan een boek");
                Console.WriteLine("3. Toon alle info van een boek op basis van titel en auteur");
                Console.WriteLine("4. Zoek een boek op verschillende manieren");
                Console.WriteLine("5. Verwijder een boek uit de bibliotheek");
                Console.WriteLine("6. Toon alle boeken in de bibliotheek");
                Console.WriteLine("7. Lees boeken in vanuit een CSV-bestand");
                Console.WriteLine("******* LEESZAAL *******");
                Console.WriteLine("8. Voeg een krant of maandblad toe");
                Console.WriteLine("9. Bekijk alle kranten");
                Console.WriteLine("10. Bekijk alle maandbladen");
                Console.WriteLine("11. Aanwinsten van de leeszaal bekijken");
                Console.WriteLine("******* (UIT)LENEN *******");
                Console.WriteLine("12. Leen een boek");
                Console.WriteLine("13. Breng een boek terug");
                Console.WriteLine("14. Verlaat de applicatie.");
                string choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Wat is de naam van het boek?");
                        title = Console.ReadLine();
                        Console.WriteLine("Wie is de auteur?");
                        author = Console.ReadLine();
                        try
                        {
                            Book newBook = new Book(title, author, apBibliotheek);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Onverwachte fout: " + ex.Message);
                            Console.ResetColor();
                        }
                        PressKey();
                        break;
                    case "2":
                        if (apBibliotheek.BookList.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("De bibliotheek bevat momenteel geen boeken.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("Geef de titel in van het boek waar je info aan wilt toevoegen: ");
                            title = Console.ReadLine();
                            Console.WriteLine("Geef de auteur in van het boek waar je info aan wilt toevoegen: ");
                            author = Console.ReadLine();
                            Book bookToAddInfo = apBibliotheek.SearchBookByTitleAndAuthor(title, author);
                            if (bookToAddInfo != null)
                            {
                                bool goodInput = false;
                                Console.WriteLine("Geef het ISBN nummer in: ");
                                try
                                {
                                    bookToAddInfo.IsbnNumber = Console.ReadLine();
                                }
                                catch (InvalidIsbnException iiex)
                                {
                                    Console.WriteLine("Error: " + iiex.Message);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Onverwachte fout: " + ex.Message);
                                }

                                Console.WriteLine("Geef het aantal pagina's in: ");
                                try
                                {
                                    bookToAddInfo.Pages = Convert.ToInt32(Console.ReadLine());
                                }
                                catch (ArgumentOutOfRangeException aoex)
                                {
                                    Console.WriteLine("Error: " + aoex.Message);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Onverwachte fout:" + ex.Message);
                                }


                                goodInput = false;
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
                                try
                                {
                                    bookToAddInfo.PublishYear = Convert.ToInt32(Console.ReadLine());
                                }
                                catch (InvalidYearException iyex)
                                {
                                    Console.WriteLine("Error: " + iyex.Message);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Onverwachte fout:" + ex.Message);
                                }


                                goodInput = false;
                                while (!goodInput)
                                {
                                    Console.WriteLine("Geef het genre van het boek in: ");
                                    string inputGenre = Console.ReadLine();
                                    if (Enum.TryParse(inputGenre, out Genres genre))
                                    {
                                        bookToAddInfo.Genre = genre;
                                        goodInput = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Ongeldige invoer voor het genre boek! Het moet bv. 'Fictie', 'NonFictie', ... zijn");
                                    }
                                }

                                Console.WriteLine("Geef een beoordeling in (0-10): ");
                                try
                                {
                                    bookToAddInfo.Rating = Convert.ToInt32(Console.ReadLine());
                                }
                                catch (ArgumentException aex)
                                {
                                    Console.WriteLine("Error: " + aex.Message);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Onverwachte fout:" + ex.Message);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Het boek dat je zocht is niet gevonden!");
                            }
                        }
                        PressKey();
                        break;
                    case "3":
                        Console.WriteLine("Geef de titel van het boek in: ");
                        string searchTitle = Console.ReadLine();
                        Console.WriteLine("Geef de auteur van het boek in: ");
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
                        Console.WriteLine("Zoek een boeken op basis van:");
                        Console.WriteLine("1. Titel en auteur");
                        Console.WriteLine("2. ISBN");
                        Console.WriteLine("3. Auteur");
                        Console.WriteLine("4. Pagina's");

                        string searchChoice = Console.ReadLine();
                        switch (searchChoice)
                        {
                            case "1":
                                Console.Write("Titel: ");
                                string titleSearch = Console.ReadLine();
                                Console.Write("Auteur: ");
                                string authorSearch = Console.ReadLine();
                                Book bookByTitleAndAuthor = apBibliotheek.SearchBookByTitleAndAuthor(titleSearch, authorSearch);
                                if (bookByTitleAndAuthor != null)
                                {
                                    bookByTitleAndAuthor.ShowInfo();
                                }
                                else 
                                {
                                    Console.WriteLine("Geen boek gevonden.");
                                }
                                break;

                            case "2":
                                Console.Write("ISBN: ");
                                string isbnSearch = Console.ReadLine();
                                Book bookByIsbn = apBibliotheek.SearchBookByISBN(isbnSearch);
                                if (bookByIsbn != null)
                                {
                                    bookByIsbn.ShowInfo();
                                }
                                else
                                {
                                    Console.WriteLine("Geen boek gevonden.");
                                }
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
                                else
                                {
                                    Console.WriteLine("Geen boeken gevonden.");
                                }
                                break;

                            case "4":
                                Console.Write("Minimaal aantal pagina’s: ");
                                int minPages = 0;
                                try
                                {
                                    minPages = Convert.ToInt32(Console.ReadLine());
                                }
                                catch (FormatException fex)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Geef een geldig aantal pagina's in, probeer later opnieuw");
                                    Console.ResetColor();
                                    break;
                                }
                                

                                List<Book> booksByPages = apBibliotheek.SearchBooksByPages(minPages: minPages);
                                if (booksByPages != null)
                                {
                                    foreach (var book in booksByPages)
                                    {
                                        book.ShowInfo();
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Geen boeken gevonden met minimaal {minPages} pagina's.");
                                    Console.ResetColor(); 
                                }
                                break;

                            default:
                                Console.WriteLine("Ongeldige keuze.");
                                break;
                        }
                        PressKey();
                        break;

                    case "5":
                        Console.WriteLine("Geef de titel van het boek dat je wilt verwijderen: ");
                        string removeTitle = Console.ReadLine();
                        Console.WriteLine("Geef de auteur van het boek dat je wilt verwijderen: ");
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
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("De bibliotheek bevat momenteel geen boeken.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("Boeken in de bibliotheek:");
                            foreach (Book book in apBibliotheek.BookList)
                            {
                                book.ShowInfo();
                            }
                        }
                        PressKey();
                        break;

                    case "7":
                        Console.Write("Geef de naam van het CSV-bestand in: ");
                        string csvPath = Console.ReadLine();
                        if (File.Exists(csvPath))
                        {
                            apBibliotheek.ReadStudentsFromCSV(csvPath);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("CSV-bestand succesvol ingelezen en boeken toegevoegd aan de bibliotheek.");
                            Console.ResetColor();
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
                            Console.WriteLine("Foutieve keuze");
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
                    case "12": // lenen van boek
                        Console.WriteLine("Welk boek wil je lenen?");
                        if (apBibliotheek.BookList.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Er zijn momenteel geen uitgeleende boeken.");
                        }
                        else
                        {
                            for (int i = 0; i < apBibliotheek.BookList.Count; i++)
                            {
                                Console.ForegroundColor = apBibliotheek.BookList[i].IsAvailable ? ConsoleColor.Green : ConsoleColor.Red;
                                Console.WriteLine($"{i + 1}: {apBibliotheek.BookList[i].Title} - {apBibliotheek.BookList[i].Author}");
                                Console.ResetColor();
                            }
                            try
                            {
                                int bookChoice = Convert.ToInt32(Console.ReadLine()) - 1;
                                apBibliotheek.BookList[bookChoice].Borrow();
                            }
                            catch (FormatException)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Geef een getal in!");
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Geef een geldig getal in!");
                            }
                        }
                        PressKey();
                        break;
                    case "13": // terugbrengen van boek
                        Console.WriteLine("Welk boek wil je terug brengen?");
                        List<int> borrowedBookIndexes = new();

                        for (int i = 0; i < apBibliotheek.BookList.Count; i++)
                        {
                            if (!apBibliotheek.BookList[i].IsAvailable)
                            {
                                borrowedBookIndexes.Add(i);
                                Console.WriteLine($"{borrowedBookIndexes.Count}: {apBibliotheek.BookList[i].Title} - {apBibliotheek.BookList[i].Author}");
                            }
                        }

                        if (borrowedBookIndexes.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Er zijn momenteel geen uitgeleende boeken.");
                        }
                        else
                        {
                            try
                            {
                                int returnChoice = Convert.ToInt32(Console.ReadLine()) - 1;
                                int actualIndex = borrowedBookIndexes[returnChoice];
                                apBibliotheek.BookList[actualIndex].Return();
                            }
                            catch (FormatException)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Geef een getal in!");
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Geef een geldig getal in!");
                            }
                        }
                        PressKey();
                        break;
                    case "14":
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Druk een toets om verder te gaan");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
