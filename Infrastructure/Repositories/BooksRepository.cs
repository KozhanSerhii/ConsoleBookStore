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
            using (_context)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return true;
            }
        }

        public Book Get(int id)
        {
            throw new NotImplementedException();
        }

        public Book? Get(string title, string author)
        {
            return _context.Books.SingleOrDefault(b => b.Author == author && b.Title == title);
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public bool Remove(string tittle, out long book_ID)
        {
            using (_context)
            {
                var entityToDelete = _context.Books.FirstOrDefault(b => b.Title.Trim().ToLower().Replace(" ", "") == tittle);
                book_ID = entityToDelete != null ? entityToDelete.Book_ID : 0;
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

                if (existingBook != null)
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