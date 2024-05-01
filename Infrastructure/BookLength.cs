using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Infrastructure
{
    public class BookLength
    {        
        public int maxBookIDLength { get; set; }
        public int maxTitleLength { get; set; }
        public int maxAuthorLength { get; set; }  
        public int maxStringLength { get; set; }
    }
}
