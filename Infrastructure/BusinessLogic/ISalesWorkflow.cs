using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessLogic
{
    public interface ISalesWorkflow
    {
        bool AddEntity(SaleDto dto);
        bool DeleteEntity(int id);
        bool UpdateSaleEntity(Sale dto);
        List<Sale> GetAll();
        Sale? Get(long id);
    }
}