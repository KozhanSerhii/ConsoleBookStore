using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessLogic
{
    public class SaleDto
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public long Price { get; set; }// to int
        public long Number_Of_Sales { get; set; }// to int  
    }
}
