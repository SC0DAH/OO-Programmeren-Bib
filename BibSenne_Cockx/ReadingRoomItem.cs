using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibSenne_Cockx
{
    public abstract class ReadingRoomItem
    {
        private string title;

        public string Title
        {
            get { return title; }
        }

        private string publisher;
        public string Publisher
        {
            get { return publisher; }
            set { publisher = value; }
        }

        public abstract string Identification { get; }
        public abstract string Categorie { get; }

        protected ReadingRoomItem(string title, string publisher)
        {
            this.title = title;
            Publisher = publisher;
        }
    }

}
