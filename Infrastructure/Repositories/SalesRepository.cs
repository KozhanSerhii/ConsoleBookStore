using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        public bool Add()
        {
            string author, book;
            long id = 0, price = 0, number_of_sales = 0, x = 0;

            Console.WriteLine("Введіть номер id книги");
            if (long.TryParse(Console.ReadLine(), out long result3))
                id = result3;
            else
                Console.WriteLine();

            Console.WriteLine("Введіть автора книги");
            author = Console.ReadLine();

            Console.WriteLine("Введіть назву книги");
            book = Console.ReadLine();

            Console.WriteLine("Введіть ціну книги");
            if (long.TryParse(Console.ReadLine(), out long result1))
                price = result1;
            else
                Console.WriteLine();

            Console.WriteLine("Введіть кількість проданих примірників книги");
            if (long.TryParse(Console.ReadLine(), out long result2))
                number_of_sales = result2;
            else
                Console.WriteLine();

            using (ConsoleBookStoreContextToBook db = new ConsoleBookStoreContextToBook())
            {
                Book newBook = new Book { Book_ID = id, Author = author, Title = book};

                // Додавання
                db.Books.Add(newBook);
                db.SaveChanges();
                x += 1;
            }
            using (ConsoleBookStoreContextToSale db = new ConsoleBookStoreContextToSale())
            {
                Sale newSale = new Sale { Book_ID = id, Price = price, Number_Of_Sales = number_of_sales };

                // Додавання
                db.Sales.Add(newSale);
                db.SaveChanges();
                x += 1;
            }
            return (x == 2) ? true : false;
        }

        public Sale Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Sale sale)
        {
            throw new NotImplementedException();
        }
    }
}
