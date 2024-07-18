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
        private IBooksWorkflow _booksWorkflow;
        
        public SalesWorkflow()
        {
            
            _salesRepository = new SalesRepository();
        }

        public bool AddEntity(SaleDto dto)
        {
            _booksWorkflow = new BooksWorkflow();
            var existBook = _booksWorkflow.Get(dto.Title, dto.Author);
            if (existBook == null) 
            {
                _booksWorkflow.AddEntity(new BookDto { Title = dto.Title, Author = dto.Author });
            }                       
            var newBook = _booksWorkflow.Get(dto.Title, dto.Author);
            if (newBook == null)
            {
                throw new Exception("book is null");
            }
            _salesRepository.Add(new Sale { Book_ID = newBook.Book_ID, Number_Of_Sales = dto.Number_Of_Sales, Price = dto.Price });
            
            return true;
        }

        public bool DeleteEntity(int id)
        {
            return (_salesRepository.Remove(id) == true);
        }

        public Sale? Get(long id)
        {
            return _salesRepository.Get(id);
        }

        public List<Sale> GetAll()
        {
            return _salesRepository.GetAll();
        }

        public bool UpdateSaleEntity(Sale sale)
        {
            return _salesRepository.Update(sale);
        }
    }
}