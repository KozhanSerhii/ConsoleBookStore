using Infrastructure;
using Infrastructure.BusinessLogic;
using Infrastructure.Repositories;
using Microsoft.VisualBasic;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace Common.DrawEngine
{
    public class DrawEngine
    {
        private ISalesRepository _salesRepository;
        private IBooksRepository _booksRepository;
        private ISalesWorkflow _salesWorkflow;
        private IBooksWorkflow _booksWorkflow;
        public DrawEngine()
        {
            _salesRepository = new SalesRepository();
            _booksRepository = new BooksRepository();
            _salesWorkflow = new SalesWorkflow();
            _booksWorkflow = new BooksWorkflow();
        }

        public void PrintMenu()
        {
            Console.WriteLine("menu general:");
            Console.WriteLine("1.look sales book's");
            Console.WriteLine("2.look all books");
            Console.WriteLine("3.exit");
        }        
        public void PrintBooksMenu()
        {
            Console.WriteLine("menu books:");
            Console.WriteLine("1.add");
            Console.WriteLine("2.remove");
            Console.WriteLine("3.upd");
            Console.WriteLine("4.turn back");
        }
        public void PrintSalesMenu()
        {
            Console.WriteLine("menu sales:");
            Console.WriteLine("1.add");
            Console.WriteLine("2.remove");
            Console.WriteLine("3.upd");
            Console.WriteLine("4.turn back");
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

        public SaleLength CalculateSaleLength() 
        {
            var sales = _salesRepository.GetAll();
            var books = _booksRepository.GetAll();

            SaleLength lengthSales = new();
            string str = "Sale ID";
            lengthSales.maxSaleIDLength = sales.Max(s => s.Sale_ID.ToString().Length) > str.Length ? sales.Max(s => s.Sale_ID.ToString().Length) : str.Length;
            str = "Title";
            lengthSales.maxTitleLength = books.Max(b => b.Title.Length) > str.Length ? books.Max(b => b.Title.Length) : str.Length;
            str = "Author";
            lengthSales.maxAuthorLength = books.Max(b => b.Author.Length) > str.Length ? books.Max(b => b.Author.Length) : str.Length;
            str = "Price";
            lengthSales.maxPriceLength = sales.Max(s => s.Price.ToString().Length) > str.Length ? sales.Max(s => s.Price.ToString().Length) : str.Length;
            str = "Number Of Sales";
            lengthSales.maxNumberOfSalesLength = sales.Max(s => s.Number_Of_Sales.ToString().Length) > str.Length ? sales.Max(s => s.Number_Of_Sales.ToString().Length) : str.Length;

            lengthSales.maxStringLength = lengthSales.maxSaleIDLength + lengthSales.maxTitleLength + lengthSales.maxAuthorLength + lengthSales.maxPriceLength + lengthSales.maxNumberOfSalesLength;
            return lengthSales;
        }
        public BookLength CalculateBookLength()
        {            
            var books = _booksRepository.GetAll();

            BookLength lengthBooks = new();

            string str = "Book ID";
            lengthBooks.maxBookIDLength = books.Max(s => s.Book_ID.ToString().Length) > str.Length ? books.Max(s => s.Book_ID.ToString().Length) : str.Length;
            str = "Title";
            lengthBooks.maxTitleLength = books.Max(b => b.Title.Length) > str.Length ? books.Max(b => b.Title.Length) : str.Length;
            str = "Author";
            lengthBooks.maxAuthorLength = books.Max(b => b.Author.Length) > str.Length ? books.Max(b => b.Author.Length) : str.Length;
            lengthBooks.maxStringLength = lengthBooks.maxBookIDLength + lengthBooks.maxTitleLength + lengthBooks.maxAuthorLength;
            return lengthBooks;
        }
        public void PrintTopSale(SaleLength lengthSales)
        {                    
            for (int i = 0; i <= lengthSales.maxStringLength + 5; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.Write("|");
            int dynamicIndent = lengthSales.maxSaleIDLength; // Динамічний відступ, який можна змінювати
            string text = "Sale ID";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthSales.maxTitleLength; // Динамічний відступ, який можна змінювати
            text = "Title";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthSales.maxAuthorLength; // Динамічний відступ, який можна змінювати
            text = "Author";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthSales.maxPriceLength; // Динамічний відступ, який можна змінювати
            text = "Price";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthSales.maxNumberOfSalesLength; // Динамічний відступ, який можна змінювати
            text = "Number Of Sales";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");
            Console.WriteLine();
        }
        
        public void PrintMiddleSale(SaleLength lengthSales)
        {
            var sales = _salesRepository.GetAll();
            var books = _booksRepository.GetAll();            

            foreach (var sale in sales)
            {
                PrintLowSale(lengthSales);
                Console.Write("|");
                var book = books.FirstOrDefault(b => b.Book_ID == sale.Book_ID);

                if (book == null)
                    throw new Exception("book not found");
                
                int dynamicIndent = lengthSales.maxSaleIDLength; // Динамічний відступ, який можна змінювати
                string text = Convert.ToString(sale.Sale_ID);
                Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

                dynamicIndent = lengthSales.maxTitleLength; // Динамічний відступ, який можна змінювати
                text = Convert.ToString(book.Title); ;
                Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

                dynamicIndent = lengthSales.maxAuthorLength; // Динамічний відступ, який можна змінювати
                text = Convert.ToString(book.Author); ;
                Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

                dynamicIndent = lengthSales.maxPriceLength; // Динамічний відступ, який можна змінювати
                text = Convert.ToString(sale.Price); ;
                Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

                dynamicIndent = lengthSales.maxNumberOfSalesLength; // Динамічний відступ, який можна змінювати
                text = Convert.ToString(sale.Number_Of_Sales); ;
                Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

                Console.WriteLine();
            }
        }
        public void PrintLowSale(SaleLength lengthSales)
        {                   
            for (int i = 0; i <= lengthSales.maxStringLength + 5; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }        
        public void PrintTopBook(BookLength lengthBooks)
        {                               
            
            for (int i = 0; i <= lengthBooks.maxStringLength + 3; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.Write("|");            

            int dynamicIndent = lengthBooks.maxBookIDLength; // Динамічний відступ, який можна змінювати
            string text = "Book ID";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthBooks.maxTitleLength; // Динамічний відступ, який можна змінювати
            text = "Title";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthBooks.maxAuthorLength; // Динамічний відступ, який можна змінювати
            text = "Author";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");
            Console.WriteLine();
        }

        public void PrintMiddleBook(BookLength lengthBooks)
        {            
            var books = _booksRepository.GetAll();
            
            foreach (var book in books)
            {
                PrintLowBook(lengthBooks);
                Console.Write("|");                

                if (book == null)
                    throw new Exception("book not found");

                int dynamicIndent = lengthBooks.maxBookIDLength; // Динамічний відступ, який можна змінювати
                string text = Convert.ToString(book.Book_ID);
                Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

                dynamicIndent = lengthBooks.maxTitleLength; // Динамічний відступ, який можна змінювати
                text = Convert.ToString(book.Title); ;
                Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

                dynamicIndent = lengthBooks.maxAuthorLength; // Динамічний відступ, який можна змінювати
                text = Convert.ToString(book.Author); ;
                Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

                Console.WriteLine();
            }

        }
        public void PrintLowBook(BookLength lengthBooks)
        {                     
            for (int i = 0; i <= lengthBooks.maxStringLength + 3; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
        public void PrintAllSales()
        {            
            PrintTopSale(CalculateSaleLength());
            PrintMiddleSale(CalculateSaleLength());
            PrintLowSale(CalculateSaleLength());
        }
        public void PrintAllBooks()
        {            
            PrintTopBook(CalculateBookLength());
            PrintMiddleBook(CalculateBookLength());
            PrintLowBook(CalculateBookLength());
        }

        public bool AddSale()
        {
            var newSale = new SaleDto();
            Console.WriteLine("enter author");
            newSale.Author = Console.ReadLine();

            Console.WriteLine("enter book name");
            newSale.Title = Console.ReadLine();

            Console.WriteLine("enter price");
            var price = Console.ReadLine();
            if (long.TryParse(price, out long result1))
                newSale.Price = result1;
            else
                DrawQuestion(price);

            Console.WriteLine("enter count");
            var number = Console.ReadLine();
            if (long.TryParse(number, out long result2))
                newSale.Number_Of_Sales = result2;
            else
                DrawQuestion(number);

            return _salesWorkflow.AddEntity(newSale);
        }
        public bool AddBook()
        {
            var newBook = new BookDto();
            Console.WriteLine("enter author");
            newBook.Author = Console.ReadLine();

            Console.WriteLine("enter book name");
            newBook.Title = Console.ReadLine();            

            return _booksWorkflow.AddEntity(newBook);
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
            Console.WriteLine("enter id book");
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int id)){
                Console.WriteLine($"Дані були введені некорректно: {input}. Повернення до головного меню");
                PrintSalesMenu();
            }

            return _salesWorkflow.DeleteEntity(id);
        }
        public bool RemoveBook()
        {
            Console.WriteLine("enter id book");
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine($"Дані були введені некорректно: {input}. Повернення до головного меню");
                PrintBooksMenu();
            }

            return _booksWorkflow.DeleteEntity(id);
        }

        public bool UpdateSale()
        {
            Console.WriteLine("enter Id sale");
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
            Console.WriteLine("enter new price");
            if (long.TryParse(Console.ReadLine(), out long result))
                sale.Price = result;
            else
                Console.WriteLine();

            Console.WriteLine("enter new count");
            if (long.TryParse(Console.ReadLine(), out long result1))
                sale.Number_Of_Sales = result1;
            else
                Console.WriteLine();

            Console.WriteLine("change ID of book?  1 - y, 2 - n");
            var input1 = ReadEnteredValue();
            if (input1 == 1)
            {
                Console.WriteLine("enter new if of book");
                var input2 = ReadEnteredValue();
                sale.Book_ID = input2;

            }

            return _salesWorkflow.UpdateSaleEntity(sale);
        }

        public bool UpdateBook()
        {
            Console.WriteLine("enter Id book");
            var idBook = Console.ReadLine();
            if (!long.TryParse(idBook, out long id))
            {
                Console.WriteLine($"Дані були введені некорректно: {idBook}. Повернення до головного меню");
                PrintBooksMenu();
                return false;
            }

            var book = _booksRepository.Get(id);
            if (book == null)
            {
                Console.WriteLine("Запис не знайдено");
                PrintBooksMenu();
                return false;
            }
            PrintBook(book);
            Console.WriteLine("enter new Title");
            string title = Console.ReadLine();
            book.Title = title;

            Console.WriteLine("enter new Author");
            string author = Console.ReadLine();
            book.Author = author;

            Console.WriteLine("change ID of book?  1 - y, 2 - n");
            var input1 = ReadEnteredValue();
            if (input1 == 1)
            {
                Console.WriteLine("enter new if of book");
                var input2 = ReadEnteredValue();
                book.Book_ID = input2;

            }

            return _booksWorkflow.UpdateSaleEntity(book);
        }
        private void PrintSale(Sale sale)
        {
            Console.WriteLine($"{sale.Sale_ID}, {sale.Book_ID}, {sale.Price}, {sale.Number_Of_Sales}");
        }
        private void PrintBook(Book book)
        {
            Console.WriteLine($"{book.Book_ID}, {book.Title}, {book.Author}");
        }
    }
}