using System;
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
            using (ConsoleBookStoreContext db = new ConsoleBookStoreContext())
            {                
                db.Sales.Add(sale);
                db.SaveChanges();
                return true;
            }          
        }

        public Sale Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(long id)
        {
            using (var context = new ConsoleBookStoreContext())
            {
                var entityToDelete = context.Sales.FirstOrDefault(s => s.Sale_ID == id);

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
            using (var context = new ConsoleBookStoreContext())
            {
                var existingBook = context.Sales.FirstOrDefault(b => b.Sale_ID == sale.Sale_ID);

                if (existingBook != null)
                {
                    existingBook.Price = sale.Price;
                    existingBook.Number_Of_Sales = sale.Number_Of_Sales;
                    context.SaveChanges();
                    return true; // Повертаємо true, якщо оновлення пройшло успішно
                }

                return false; // Повертаємо false, якщо книгу не знайдено за ID
            }
        }
    }
}
