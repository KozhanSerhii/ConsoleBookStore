﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        public bool Add(Sale sale)
        {
            using (ConsoleBookStoreContext context = new ConsoleBookStoreContext())
            {
                context.Sales.Add(sale);
                context.SaveChanges();
                return true;
            }
        }
        
        public Sale? Get(long id)
        {
            using (ConsoleBookStoreContext context = new ConsoleBookStoreContext())
            {
                return context.Sales.SingleOrDefault(b => b.Sale_ID == id);
            }
        }

        public List<Sale> GetAll()
        {
            using (ConsoleBookStoreContext context = new ConsoleBookStoreContext())
            {
                return context.Sales.ToList();
            }
        }

        public bool Remove(long id)
        {
            using (ConsoleBookStoreContext context = new ConsoleBookStoreContext())
            {
                var entityToDelete = context.Sales.SingleOrDefault(s => s.Sale_ID == id);

                if (entityToDelete != null)
                {
                    context.Sales.Remove(entityToDelete);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool Update(Sale sale)
        {
            using (ConsoleBookStoreContext context = new ConsoleBookStoreContext())
            {
                var existingSale = context.Sales.FirstOrDefault(b => b.Sale_ID == sale.Sale_ID);

                if (existingSale != null)
                {
                    existingSale.Price = sale.Price;
                    existingSale.Number_Of_Sales = sale.Number_Of_Sales;

                    if (sale.Book_ID != existingSale.Book_ID)
                    {
                        var book = context.Books.SingleOrDefault(b => b.Book_ID == sale.Book_ID);
                        if (book != null)
                        {
                            existingSale.Book_ID = sale.Book_ID;
                            context.SaveChanges();
                            return true; // Повертаємо true, якщо оновлення пройшло успішно
                        }

                        else
                        {
                            Console.WriteLine("The book with this ID does not exist");
                            context.SaveChanges();
                            return true;
                        }
                    }

                    context.SaveChanges();
                    return true;                   
                }
                return false; // Повертаємо false, якщо книгу не знайдено за ID
            }
        }
    }
}
