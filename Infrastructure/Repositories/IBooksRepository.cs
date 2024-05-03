using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
     public interface IBooksRepository
    {
        bool Add(Book book);
        bool RemoveBook(long id);
        bool Update(Book book);
        List<Book> GetAll();
        Book? Get(long id);
        Book? Get(string title, string author);
        bool RemoveSales(long idSale);
    }
}
