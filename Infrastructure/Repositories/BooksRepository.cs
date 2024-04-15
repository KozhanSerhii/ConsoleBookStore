using Infrastructure.Repositories;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        public bool Add(Book book)
        {
            using (ConsoleBookStoreContext db = new ConsoleBookStoreContext())
            {
                db.Books.Add(book);
                db.SaveChanges();
                return true;
            }
        }

        public Book Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(string tittle, out long book_ID)
        {
            using (var context = new ConsoleBookStoreContext())
            {
                var entityToDelete = context.Books.FirstOrDefault(b => b.Title.Trim().ToLower().Replace(" ", "") == tittle);
                book_ID = entityToDelete != null ? entityToDelete.Book_ID : 0;
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
            using (var context = new ConsoleBookStoreContext())
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