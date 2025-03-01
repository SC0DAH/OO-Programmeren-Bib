namespace BibSenne_Cockx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library mijnBibliotheek = new Library("AP Bib");
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Welkom bij de Bibliotheek!");
                Console.WriteLine("Kies een optie:");
                Console.WriteLine("1. Voeg een boek toe aan de bibliotheek op basis van titel en auteur.");
                Console.WriteLine("2. Voeg informatie toe aan een boek (ISBN, genre, etc.).");
                Console.WriteLine("3. Toon alle info van een boek op basis van titel en auteur.");
                Console.WriteLine("4. Zoek een boek op verschillende manieren.");
                Console.WriteLine("5. Verwijder een boek uit de bibliotheek.");
                Console.WriteLine("6. Toon alle boeken in de bibliotheek.");
                Console.WriteLine("7. Lees boeken in vanuit een CSV-bestand.");
                Console.WriteLine("8. Verlaat de applicatie.");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        
                        break;
                    case "2":
                        
                        break;
                    case "3":
                        
                        break;
                    case "4":
                        
                        break;
                    case "5":
                        
                        break;
                    case "6":
                        
                        break;
                    case "7":
                        
                        break;
                    case "8":
                        Console.WriteLine("Tot ziens!");
                        isRunning = false;
                        return;
                    default:
                        Console.WriteLine("Ongeldige keuze, probeer het opnieuw.");
                        break;
                }
            }
        }
    }
}
