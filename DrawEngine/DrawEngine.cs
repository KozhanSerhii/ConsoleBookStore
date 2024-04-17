using Infrastructure;
using Infrastructure.BusinessLogic;
using Infrastructure.Repositories;

namespace Common.DrawEngine
{
    public class DrawEngine
    {
        //private ConsoleBookStoreContext _context;
        private ISalesRepository _salesRepository;
        private IBooksRepository _booksRepository;

        public DrawEngine()
        {
            _salesRepository = new SalesRepository();
            _booksRepository = new BooksRepository();
        }

        public void PrintMenu()
        {
            Console.WriteLine("Оберіть пункт меню:");
            Console.WriteLine("1.Переглянути продажі книгарні");            
            Console.WriteLine("2.Вийти з програми");
        }

        public int ReadEnteredValue()
        {
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch(Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public void PrintSalesMenu()
        {
            Console.WriteLine("Оберіть пункт меню:");
            Console.WriteLine("1.Додати запис до таблиці");
            Console.WriteLine("2.Видалити запис з таблиці");
            Console.WriteLine("3.Редактувати запис з таблиці");
            Console.WriteLine("4.Повернутися назад");
        }

        public void PrintAllSales()
        {
            var sales = _salesRepository.GetAll();
            var books = _booksRepository.GetAll();

            foreach (var sale in sales)
            {
                var book = books.FirstOrDefault(b => b.Book_ID == sale.Book_ID);
                if (book == null)
                    throw new Exception("book not found");

                Console.WriteLine($"{book.Title}, {book.Author}, {sale.Price}, {sale.Number_Of_Sales}");
            }
        }
        public void AddSale(SalesDto newSale)
        {
            Console.WriteLine("Введіть автора книги");
            newSale.Author = Console.ReadLine();

            Console.WriteLine("Введіть назву книги");
            newSale.Title = Console.ReadLine();

            Console.WriteLine("Введіть ціну книги");
            if (long.TryParse(Console.ReadLine(), out long result1))
                newSale.Price = result1;
            else
                Console.WriteLine();

            Console.WriteLine("Введіть кількість проданих примірників книги");
            if (long.TryParse(Console.ReadLine(), out long result2))
                newSale.Number_Of_Sales = result2;
            else
                Console.WriteLine();            
        }

        public bool RemoveSale()
        {
            Console.WriteLine("Введіть назву книжки яку хочете видалити");
            string tittleRemove = Console.ReadLine().Trim().ToLower().Replace(" ", "");
            if (_booksRepository.Remove(tittleRemove, out long id) == true && _salesRepository.Remove(id) == true)
            {
                return true;
            }
            return false;                         
        }
        public void f1(int choice)
        {
            Sale sale1 = new Sale();
            Book book1 = new Book();

            switch (choice)
            {
                case 1:
                    {
                        PrintSalesMenu();
                        PrintSalesMenu();
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {                                                        
                            case 3:
                                {
                                    Console.WriteLine("Введіть назву книжки запис якої хочете редагувати");
                                    string tittleUpdate = Console.ReadLine().Trim().ToLower().Replace(" ", "");
                                    BooksRepository updateBook = new BooksRepository();
                                    SalesRepository updateSale = new SalesRepository();

                                    using (var context = new ConsoleBookStoreContext())
                                    {
                                        var entityToUpdate = context.Books.FirstOrDefault(b => b.Title.Trim().ToLower().Replace(" ", "") == tittleUpdate);
                                        long book_ID = entityToUpdate != null ? entityToUpdate.Book_ID : 0;
                                        Console.WriteLine("Введіть оновленого автора книги");
                                        entityToUpdate.Author = Console.ReadLine();

                                        Console.WriteLine("Введіть оновлену назву книги");
                                        entityToUpdate.Title = Console.ReadLine();

                                        var entityToUpdate2 = context.Sales.FirstOrDefault(s => s.Sale_ID == book_ID);

                                        Console.WriteLine("Введіть оновлену ціну книги");
                                        if (long.TryParse(Console.ReadLine(), out long result1))
                                            entityToUpdate2.Price = result1;
                                        else
                                            Console.WriteLine();

                                        Console.WriteLine("Введіть оновлену кількість проданих примірників книги");
                                        if (long.TryParse(Console.ReadLine(), out long result2))
                                            entityToUpdate2.Number_Of_Sales = result2;
                                        else
                                            Console.WriteLine();

                                        if (updateBook.Update(entityToUpdate) == true && updateSale.Update(entityToUpdate2) == true)
                                            Console.WriteLine("Запис успішно змінений");
                                    }
                                    break;
                                }
                            case 4:
                                break;
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }

}