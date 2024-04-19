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
            using (ConsoleBookStoreContext context = new ConsoleBookStoreContext())
            {
                context.Books.Add(book);
                context.SaveChanges();
                return true;
            }                       
        }

        public Book? Get(long id)
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
                var a = author.Trim().Replace(" ", "").ToLower();
                var t = title.Trim().Replace(" ", "").ToLower();
                return context.Books.SingleOrDefault(b => 
                    b.Author.Replace(" ", "").ToLower() == a && b.Title.Replace(" ", "").ToLower() == t);
            }
        }

        public List<Book> GetAll()
        {
            return [.. _context.Books];
        }

        public bool Remove(long id)
        {
            using (ConsoleBookStoreContext context = new ConsoleBookStoreContext())
            {
                var entityToDelete = context.Books.FirstOrDefault(s => s.Book_ID == id);

                if (entityToDelete != null)
                {
                    context.Books.Remove(entityToDelete);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }

        }

        public bool Update(Book book)
        {
            using (ConsoleBookStoreContext context = new ConsoleBookStoreContext())
            {
                var existingBook = context.Books.FirstOrDefault(b => b.Book_ID == book.Book_ID);

                if (existingBook != null)
                {
                    existingBook.Title = book.Title;
                    existingBook.Author = book.Author;
                    context.SaveChanges();
                    return true; // Повертаємо true, якщо оновлення пройшло успішно
                }

                return false; // Повертаємо false, якщо книгу не знайдено за ID
            }
        }
    }
}