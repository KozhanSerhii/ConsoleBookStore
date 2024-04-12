using Infrastructure.Repositories;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        public bool Add(Book book)
        {
            using (ConsoleBookStoreContext db = new ConsoleBookStoreContext())
            {
                db.Books.Add(book);
                db.SaveChanges();
                return true;
            }
        }

        public Sale Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Book book)
        {
            throw new NotImplementedException();
        }
    }
}