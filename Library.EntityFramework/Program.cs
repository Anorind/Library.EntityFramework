using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //AddVisitor("Ivan", "Ivanov", true); 
            //AddVisitor("Petro", "Petrov", true); 
            //AddVisitor("Serhiy", "Fergun", false);

            //AddAuthor("Pavlo", "Tychina");
            //AddAuthor("Taras", "Shevchenko");
            //AddAuthor("Borys", "Ryjiy");

            //AddBook("Lord of the rings", new List<(string, string)> { ("John", "Tolkien") });
            //AddBook("Kobzar", new List<(string, string)> { ("Taras", "Shevchenko") });
            //AddBook("Yard", new List<(string, string)> { ("Borys", "Ryjiy") });
            //AddBook("United Book", new List<(string, string)> { ("Pavlo", "Tychina"), ("Taras", "Shevchenko") });
            //AddBook("Eneida", new List<(string, string)> { ("Ivan", "Kotlyarevskiy") });


            //GiveBookToVisitor(1, "Lord of the rings");
            //GiveBookToVisitor(2, "Kobzar");
            //GiveBookToVisitor(2, "Yard");
            //GiveBookToVisitor(2, "United Book");


            using (LibraryContext db = new LibraryContext())
            {
                // Вывести список должников
                var debtors = db.Visitors.Where(v => v.IsDebtor).ToList();
                Console.WriteLine("Список боржників:");
                foreach (var debtor in debtors)
                {
                    Console.WriteLine($"{debtor.FirstName} {debtor.LastName}");
                }



                // Вывести список авторов книги №4
                var book1 = db.Books.FirstOrDefault(b => b.BookId == 4);

                if (book1 != null)
                {
                    db.Entry(book1).Collection(b => b.Authors).Load();

                    Console.WriteLine("\nСписок авторів книги №4:");
                    foreach (var author in book1.Authors)
                    {
                        Console.WriteLine($"{author.FirstName} {author.LastName}");
                    }
                }
                else
                {
                    Console.WriteLine("Книга №3 не знайдена.");
                }


                // Вывести список книг, которые доступны в данный момент
                var availableBooks = db.Books.Where(b => b.CurrentHolder == null).ToList();
                Console.WriteLine("\nСписок доступних книг:");
                foreach (var book in availableBooks)
                {
                    Console.WriteLine(book.Title);
                }

                // Вывести список книг, которые на руках у пользователя №2
                var visitor = db.Visitors.FirstOrDefault(v => v.VisitorId == 2);
                if (visitor != null)
                {
                    db.Entry(visitor).Collection(v => v.Books).Load();

                    Console.WriteLine("\nСписок книг на руках у відвідувача №2:");
                    foreach (var book in visitor.Books)
                    {
                        Console.WriteLine(book.Title);
                    }
                }
                else
                {
                    Console.WriteLine("Відвідувач №2 не знайден.");
                }
            }
            Console.ReadLine();
        }

        static void AddVisitor(string fn, string ln, bool isDeb)
        {
            using (LibraryContext db = new LibraryContext())
            {
                Visitor visitor = new Visitor() { FirstName = fn, LastName = ln, IsDebtor = isDeb };
                db.Visitors.Add(visitor);
                db.SaveChanges();
            }
        }
        static void AddBook(string title, List<(string FirstName, string LastName)> authorNames)
        {
            using (LibraryContext db = new LibraryContext())
            {
                List<Author> authors = new List<Author>();

                foreach (var authorName in authorNames)
                {
                    Author author = db.Authors.SingleOrDefault(a => a.FirstName == authorName.FirstName && a.LastName == authorName.LastName);

                    if (author == null)
                    {
                        author = new Author { FirstName = authorName.FirstName, LastName = authorName.LastName };
                        db.Authors.Add(author);
                    }

                    authors.Add(author);
                }

                Book book = new Book { Title = title, Authors = authors };

                db.Books.Add(book);
                db.SaveChanges();
            }
        }

        static void AddAuthor(string fn,string ln)
        {
            using (LibraryContext db = new LibraryContext())
            {
                Author author = new Author() { FirstName= fn, LastName= ln };
                db.Authors.Add(author);
                db.SaveChanges();
            }
        }
        static void GiveBookToVisitor(int visitorId, string bookTitle)
        {
            using (LibraryContext db = new LibraryContext())
            {
                Visitor visitor = db.Visitors.SingleOrDefault(v => v.VisitorId == visitorId);
                Book book = db.Books.SingleOrDefault(b => b.Title == bookTitle);

                if (visitor != null && book != null)
                {
                    visitor.Books.Add(book);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Відвідувач або книга не знайшлась в базі");
                }
            }
        }

    }
}
