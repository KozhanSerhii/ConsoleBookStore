using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessLogic
{
    public class SalesWorkflow : ISalesWorkflow
    {
        private ISalesRepository _salesRepository;
        private IBooksRepository _booksRepository;
        
        public SalesWorkflow()
        {
            _booksRepository = new BooksRepository();
            _salesRepository = new SalesRepository();
        }

        public bool AddEntity(SaleDto dto)
        {
            var existBook = _booksRepository.Get(dto.Title, dto.Author);
            if (existBook == null) 
            {
                _booksRepository.Add(new Book { Title = dto.Title, Author = dto.Author });
            }                       
            var newBook = _booksRepository.Get(dto.Title, dto.Author);
            if (newBook == null)
            {
                throw new Exception("book is null");
            }
            _salesRepository.Add(new Sale { Book_ID = newBook.Book_ID, Number_Of_Sales = dto.Number_Of_Sales, Price = dto.Price });
            
            return true;
        }

        public bool DeleteEntity(int id) {
            return (_salesRepository.Remove(id) == true);
        }

        public bool UpdateSaleEntity(Sale sale)
        {
            return _salesRepository.Update(sale);
        }
    }
}
