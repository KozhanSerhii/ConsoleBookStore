using Infrastructure;
using Infrastructure.BusinessLogic;
using Infrastructure.Repositories;

namespace Common.DrawEngine
{
    public class DrawEngine
    {
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
            catch (Exception ex)
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

                Console.WriteLine($"{book.Book_ID}, {book.Title}, {book.Author}, {sale.Price}, {sale.Number_Of_Sales}");
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
            Console.WriteLine("Введіть id книжки яку хочете видалити");
            long idRemove = Convert.ToInt32(Console.ReadLine());
            if (_booksRepository.Remove(idRemove) == true && _salesRepository.Remove(idRemove) == true)
            {
                return true;
            }
            return false;
        }
        public bool UpdateSale()
        {
            Console.WriteLine("Введіть id книжки запис якої хочете змінити");
            long idUpdate = Convert.ToInt32(Console.ReadLine());

            var book = _booksRepository.Get(idUpdate);
            Console.WriteLine("Введіть оновленого автора книги");
            book.Author = Console.ReadLine();

            Console.WriteLine("Введіть оновлену назву книги");
            book.Title = Console.ReadLine();

            var sale = _salesRepository.Get(idUpdate);
            Console.WriteLine("Введіть оновлену ціну книги");
            if (long.TryParse(Console.ReadLine(), out long result))
                sale.Price = result;
            else
                Console.WriteLine();

            Console.WriteLine("Введіть оновлену кількість проданих примірників книги");
            if (long.TryParse(Console.ReadLine(), out long result1))
                sale.Number_Of_Sales = result1;
            else
                Console.WriteLine();

            if (_booksRepository.Update(book) == true && _salesRepository.Update(sale) == true)
            {
                return true;
            }
            return false;
        }
    }
}