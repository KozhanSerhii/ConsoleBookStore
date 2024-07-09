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
        private ISalesWorkflow _salesWorkflow;
        private IBooksRepository _booksRepository;

        public BooksWorkflow()
        {
            _booksRepository = new BooksRepository();
            
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
            _booksRepository.Remove(id);
            _salesWorkflow = new SalesWorkflow();
            var sales = _salesWorkflow.GetAll().Where(s => s.Book_ID == id).ToList();
            foreach(var sale in sales)
            {
                _salesWorkflow.DeleteEntity((int)sale.Sale_ID);
            }                
            return true;
        }
        public Book? Get(long id)
        {
            return _booksRepository.Get(id);
        }
        public Book? Get(string title, string author)
        {
            return _booksRepository.Get(title, author);
        }
        public List<Book> GetAll()
        {
            return _booksRepository.GetAll();
        }
        public bool UpdateBookEntity(Book book)
        {
            return _booksRepository.Update(book);
        }
    }
}
