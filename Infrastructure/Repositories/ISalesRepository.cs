using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
     public interface ISalesRepository
    {
        bool Add(Sale sale, Book book);
        bool Remove(int id);
        bool Update(Sale sale);
        List<Sale> GetAll();
        Sale Get(int id);
            
    }    
}
