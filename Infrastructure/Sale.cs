﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Sale
    {
        public long Sale_ID { get; set; }// to int
        public long Book_ID {  get; set; }// to int
        public long Price { get; set; }// to int
        public long Number_Of_Sales { get; set; }// to int  
    }
}
