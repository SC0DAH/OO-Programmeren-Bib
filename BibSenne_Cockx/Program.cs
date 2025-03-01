namespace BibSenne_Cockx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library mijnBibliotheek = new Library("Bib");

            Book nieuwBoek = new Book(
                "De Hobbit",
                "J.R.R. Tolkien",
                "9780261103283",
                310,
                Genres.Fantasy,
                1937,
                Types.Hardcover,
                9,
                mijnBibliotheek
            );

            nieuwBoek.ShowInfo();
        }
    }
}
