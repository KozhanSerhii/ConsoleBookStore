using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessLogic
{
    public interface IBooksWorkflow
    {
        bool AddEntity(BookDto dto);
        bool DeleteEntity(int id);        
        bool UpdateSaleEntity(Book dto);
        Book? Get(long id);
        List<Book> GetAll();
        Book? Get(string title, string author);
    }
}