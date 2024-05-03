using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessLogic
{
    public interface IBooksWorkflow
    {
        public bool AddEntity(BookDto dto);
        public bool DeleteEntityBook(int id);
        public bool DeleteEntitySales(long id);
        public bool UpdateSaleEntity(Book dto);
    }
}