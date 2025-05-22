using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BibSenne_Cockx
{
    internal class Magazine : ReadingRoomItem
    {
        string[,] bladen = new string[,]
		{
			{ "Data News", "DN" },
			{ "National Geographic", "NG" },
			{ "EOS Wetenschap", "EOS" },
			{ "Humo", "HU" },
			{ "Knack", "K" }
		};

        private byte month;

		public byte Month
		{
			get { return month; }
			set 
			{
				if (value > 12)
				{
					throw new ArgumentOutOfRangeException("","De maand kan maximaal 12 zijn!");
				}
                month = value;
			}
		}

		private uint year;

		public uint Year
		{
			get { return year; }
			set { year = value; }
		}

        public override string Identification
		{
            get
            {
                string prefix = "";
                for (int i = 0; i < bladen.GetLength(0); i++)
                {
                    if (bladen[i, 0] == Title)
                    {
                        prefix = bladen[i, 1];
                    }
                }
                return $"{prefix}{Month}{Year}";
            }
        }

        public override string Categorie
		{
			get { return "Maandblad"; }
        }

		public Magazine(string title, string publisher, byte month, uint year):base(title, publisher)
		{
			Month = month;
			Year = year;
		}
    }
}
