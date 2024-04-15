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
        bool Remove(string tittle, out long book_ID);
        bool Update(Book book);
        List<Book> GetAll();
        Book Get(int id);            
    }    
}
