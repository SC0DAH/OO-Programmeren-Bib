using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibSenne_Cockx
{
    internal class NewsPaper : ReadingRoomItem
    {
        string[,] kranten = new string[,]
        {
            { "Gazet Van Antwerpen", "GVA" },
            { "De Standaard", "DS" },
            { "Het Laatste Nieuws", "HLN" },
            { "De Morgen", "DM" },
            { "Het Nieuwsblad", "HN" }
        };

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public override string Identification
        {
            get
            {
                string prefix = "";
                for (int i = 0; i < kranten.GetLength(0); i++)
                {
                    if (kranten[i, 0] == Title)
                    {
                        prefix = kranten[i, 1];
                    }
                }
                string datepart = Date.ToString("ddMMyyyy");
                return $"{prefix}{datepart}";
            }
        }

        public override string Categorie
        {
            get
            {
                return "Krant";
            }
        }

        public NewsPaper(string title, string publisher, DateTime date):base(title, publisher)
        {
            Date = date;
        }
    }
}
