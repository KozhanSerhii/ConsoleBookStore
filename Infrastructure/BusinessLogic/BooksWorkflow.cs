using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessLogic
{
    public class BooksWorkflow : IBooksWorkflow
    {
        private ISalesRepository _salesRepository;
        private IBooksRepository _booksRepository;

        public BooksWorkflow()
        {
            _booksRepository = new BooksRepository();
            _salesRepository = new SalesRepository();
        }

        public bool AddEntity(BookDto dto)
        {
            var existBook = _booksRepository.Get(dto.Title, dto.Author);
            if (existBook == null)
            {
                _booksRepository.Add(new Book { Title = dto.Title, Author = dto.Author });
            }      
            
            return true;
        }

        public bool DeleteEntity(int id)
        {
            return (_booksRepository.Remove(id) == true || _salesRepository.Remove(id) == true);
        }

        public bool UpdateSaleEntity(Book book)
        {
            return _booksRepository.Update(book);
        }
    }
}
