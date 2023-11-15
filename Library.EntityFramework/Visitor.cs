using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EntityFramework
{
    internal class Visitor
    {
        public Visitor()
        {
            Books = new List<Book>();
        }

        public int VisitorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDebtor { get; set; }
        public List<Book> Books { get; set; }
    }
}
