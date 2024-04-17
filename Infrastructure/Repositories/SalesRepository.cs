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
        private ConsoleBookStoreContext _context;

        public SalesRepository()
        {
            _context = new ConsoleBookStoreContext();
        }

        public bool Add(Sale sale)
        {
            using (ConsoleBookStoreContext _context = new ConsoleBookStoreContext())
            {
                _context.Sales.Add(sale);
                _context.SaveChanges();
                return true;
            }
        }
        public Sale Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetAll()
        {
            return _context.Sales.ToList();
        }

        public bool Remove(long id)
        {
            using (_context)
            {
                var entityToDelete = _context.Sales.FirstOrDefault(s => s.Sale_ID == id);

                if (entityToDelete != null)
                {
                    _context.Sales.Remove(entityToDelete);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool Update(Sale sale)
        {
            using (_context)
            {
                var existingBook = _context.Sales.FirstOrDefault(b => b.Sale_ID == sale.Sale_ID);

                if (existingBook != null)
                {
                    existingBook.Price = sale.Price;
                    existingBook.Number_Of_Sales = sale.Number_Of_Sales;
                    _context.SaveChanges();
                    return true; // Повертаємо true, якщо оновлення пройшло успішно
                }

                return false; // Повертаємо false, якщо книгу не знайдено за ID
            }
        }
    }
}
