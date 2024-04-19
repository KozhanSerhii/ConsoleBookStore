using Infrastructure;
using Infrastructure.BusinessLogic;
using Infrastructure.Repositories;

namespace Common.DrawEngine
{
    public class DrawEngine
    {
        private ISalesRepository _salesRepository;
        private IBooksRepository _booksRepository;
        private ISalesWorkflow _salesWorkflow;

        public DrawEngine()
        {
            _salesRepository = new SalesRepository();
            _booksRepository = new BooksRepository();
            _salesWorkflow = new SalesWorkflow();
        }

        public void PrintMenu()
        {
            Console.WriteLine("Оберіть пункт меню:");
            Console.WriteLine("1.Переглянути продажі книгарні");
            Console.WriteLine("2.Переглянути існуючі книги");
            Console.WriteLine("3.Вийти з програми");
        }        
        public void PrintBooksMenu()
        {
            Console.WriteLine("Оберіть пункт меню:");
            Console.WriteLine("1.Додати запис до таблиці");
            Console.WriteLine("2.Видалити запис з таблиці");
            Console.WriteLine("3.Редактувати запис з таблиці");
            Console.WriteLine("4.Повернутися назад");
        }
        public void PrintSalesMenu()
        {
            Console.WriteLine("Оберіть пункт меню:");
            Console.WriteLine("1.Додати запис до таблиці");
            Console.WriteLine("2.Видалити запис з таблиці");
            Console.WriteLine("3.Редактувати запис з таблиці");
            Console.WriteLine("4.Повернутися назад");
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

        public void PrintAllSales()
        {
            var sales = _salesRepository.GetAll();
            var books = _booksRepository.GetAll();

            foreach (var sale in sales)
            {
                var book = books.FirstOrDefault(b => b.Book_ID == sale.Book_ID);
                if (book == null)
                    throw new Exception("book not found");

                Console.WriteLine($"{sale.Sale_ID}, {book.Title}, {book.Author}, {sale.Price}, {sale.Number_Of_Sales}");
            }
        }
        public bool AddSale()
        {
            var newSale = new SaleDto();
            Console.WriteLine("Введіть автора книги");
            newSale.Author = Console.ReadLine();

            Console.WriteLine("Введіть назву книги");
            newSale.Title = Console.ReadLine();

            Console.WriteLine("Введіть ціну книги");
            var price = Console.ReadLine();
            if (long.TryParse(price, out long result1))
                newSale.Price = result1;
            else
                DrawQuestion(price);

            Console.WriteLine("Введіть кількість проданих примірників книги");
            var number = Console.ReadLine();
            if (long.TryParse(number, out long result2))
                newSale.Number_Of_Sales = result2;
            else
                DrawQuestion(number);

            return _salesWorkflow.AddEntity(newSale);
        }

        private void DrawQuestion(object? obj)
        {
            var value = obj is null ? "Incorrect format" : obj.ToString();
            Console.WriteLine($"Дані були введені некорректно: {value} .Оберіть пункт меню:");
            Console.WriteLine("1.Повторити введення");
            Console.WriteLine("2.повернутися до головного меню");
            var answer = ReadEnteredValue();
            if (answer == 1)
                AddSale();
            else 
                PrintSalesMenu();
        }

        public bool RemoveSale()
        {
            Console.WriteLine("Введіть id книжки яку хочете видалити");
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int id)){
                Console.WriteLine($"Дані були введені некорректно: {input}. Повернення до головного меню");
                PrintSalesMenu();
            }

            return _salesWorkflow.DeleteEntity(id);
        }

        public bool UpdateSale()
        {
            Console.WriteLine("Введіть ID запису продажу який хочете змінити");
            var idSale = Console.ReadLine();
            if (!long.TryParse(idSale, out long id))
            {
                Console.WriteLine($"Дані були введені некорректно: {idSale}. Повернення до головного меню");
                PrintSalesMenu();
                return false;
            }

            var sale = _salesRepository.Get(id);
            if (sale == null)
            {
                Console.WriteLine("Запис не знайдено");
                PrintSalesMenu();
                return false;
            }
            PrintSale(sale);
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

            Console.WriteLine("Чи хочете ви змінити ID книги?  1 - так, 2 - ні");
            var input1 = ReadEnteredValue();
            if (input1 == 1)
            {
                Console.WriteLine("Введіть ID книги");
                var input2 = ReadEnteredValue();
                sale.Book_ID = input2;

            }

            return _salesWorkflow.UpdateSaleEntity(sale);
        }

        private void PrintSale(Sale sale)
        {
            Console.WriteLine($"{sale.Sale_ID}, {sale.Book_ID}, {sale.Price}, {sale.Number_Of_Sales}");
        }
    }
}