using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibSenne_Cockx
{
    internal class Library
    {
        // attributen
        private string name;
        private List<Book> bookList = new List<Book>();

        // properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
