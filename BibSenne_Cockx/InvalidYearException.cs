using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibSenne_Cockx
{
    internal class InvalidYearException : ApplicationException
    {
        public InvalidYearException(string message) : base(message)
        {

        }
    }
}
