using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessLogic
{
    public interface ISalesWorkflow
    {
        public bool AddEntity(SaleDto dto);
        public bool DeleteEntity(int id);
        public bool UpdateEntity(SaleDto dto);
    }
}