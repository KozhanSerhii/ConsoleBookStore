using Infrastructure.Repositories;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private ConsoleBookStoreContext _context;

        public BooksRepository()
        {
            _context = new ConsoleBookStoreContext();
        }                  
       
        public bool Add(Book book)
        {           
            using (ConsoleBookStoreContext _context = new ConsoleBookStoreContext())
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return true;
            }                       
        }

        public Book Get(long id)
        {
            using (ConsoleBookStoreContext context = new ConsoleBookStoreContext())
            {
                return context.Books.SingleOrDefault(b => b.Book_ID == id);
            }
        }
         
        public Book? Get(string title, string author)
        {
            using (ConsoleBookStoreContext context = new ConsoleBookStoreContext())
            {
                return context.Books.SingleOrDefault(b => b.Author == author && b.Title == title);
            }
        }



        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public bool Remove(long id)
        {
            using (_context)
            {
                var entityToDelete = _context.Books.FirstOrDefault(s => s.Book_ID == id);

                if (entityToDelete != null)
                {
                    _context.Books.Remove(entityToDelete);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }

        }

        public bool Update(Book book)
        {
            using (_context)
            {
                var existingBook = _context.Books.FirstOrDefault(b => b.Book_ID == book.Book_ID);

                if (book != null)
                {
                    existingBook.Title = book.Title;
                    existingBook.Author = book.Author;
                    _context.SaveChanges();
                    return true; // Повертаємо true, якщо оновлення пройшло успішно
                }

                return false; // Повертаємо false, якщо книгу не знайдено за ID
            }
        }
    }
}