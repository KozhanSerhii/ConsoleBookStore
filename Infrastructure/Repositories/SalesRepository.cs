﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        public bool Add(Sale sale)
        {            
            using (ConsoleBookStoreContext db = new ConsoleBookStoreContext())
            {                
                db.Sales.Add(sale);
                db.SaveChanges();
                return true;
            }          
        }

        public Sale Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Sale sale)
        {
            throw new NotImplementedException();
        }
    }
}
