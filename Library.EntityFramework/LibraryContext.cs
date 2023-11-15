using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EntityFramework
{
    internal class LibraryContext : DbContext
    {
        public LibraryContext(): base("LibConn") { }

        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }




    }
}
