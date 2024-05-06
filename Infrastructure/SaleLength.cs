using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Infrastructure
{
    public class SaleLength
    {        
        public int maxSaleIDLength { get; set; }
        public int maxTitleLength { get; set; }
        public int maxAuthorLength { get; set; }
        public int maxPriceLength { get; set; }
        public int maxNumberOfSalesLength { get; set; }
        public int maxStringLength { get; set; }
        public int maxBookIDLength { get; set; }
        public int maxTotalAmountLength { get; set; }
    }
}
