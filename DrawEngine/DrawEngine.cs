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
            Console.WriteLine("General menu:");
            Console.WriteLine("1.See the bookstore sales");
            Console.WriteLine("2.See all books");
            Console.WriteLine("3.Exit");
        }        
        public void PrintBooksMenu()
        {
            Console.WriteLine("Book menu:");
            Console.WriteLine("1.Add");
            Console.WriteLine("2.Remove");
            Console.WriteLine("3.Update");
            Console.WriteLine("4.Turn back");
        }
        public void PrintSalesMenu()
        {
            Console.WriteLine("Bookstore sales menu:");
            Console.WriteLine("1.Add");
            Console.WriteLine("2.Remove");
            Console.WriteLine("3.Update");
            Console.WriteLine("4.Turn back");
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
        public void PrintSaleColumnsNames(SaleLength lengthSales)
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
        
        public void PrintSalesContent(SaleLength lengthSales)
        {
            var sales = _salesRepository.GetAll();
            var books = _booksRepository.GetAll();            

            foreach (var sale in sales)
            {
                PrintSaleLowerBorder(lengthSales);
                Console.Write("|");
                var book = books.FirstOrDefault(b => b.Book_ID == sale.Book_ID);

                if (book == null)
                    throw new Exception("The Book not found");

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
        public void PrintSaleLowerBorder(SaleLength lengthSales)
        {                   
            for (int i = 0; i <= lengthSales.maxStringLength + 5; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }        
        public void PrintBookColumnsNames(BookLength lengthBooks)
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

        public void PrintBooksContent(BookLength lengthBooks)
        {            
            var books = _booksRepository.GetAll();
            
            foreach (var book in books)
            {
                PrintBookLowerBorder(lengthBooks);
                Console.Write("|");                

                if (book == null)
                    throw new Exception("The Book not found");

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
        public void PrintBookLowerBorder(BookLength lengthBooks)
        {                     
            for (int i = 0; i <= lengthBooks.maxStringLength + 3; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
        public void PrintAllSales()
        {
            SaleLength saleLength = new();
            saleLength = CalculateSaleLength();
            PrintSaleColumnsNames(saleLength);
            PrintSalesContent(saleLength);
            PrintSaleLowerBorder(saleLength);
        }
        public void PrintAllBooks()
        {
            BookLength bookLength = new();
            bookLength = CalculateBookLength();
            PrintBookColumnsNames(bookLength);
            PrintBooksContent(bookLength);
            PrintBookLowerBorder(bookLength);
        }

        public bool AddSale()
        {
            var newSale = new SaleDto();
            Console.WriteLine("Enter the author of the book");
            newSale.Author = Console.ReadLine();

            Console.WriteLine("Enter the title of the book");
            newSale.Title = Console.ReadLine();

            Console.WriteLine("Enter the price of the book");
            var price = Console.ReadLine();
            if (long.TryParse(price, out long result1))
                newSale.Price = result1;
            else
                DrawQuestion(price);

            Console.WriteLine("Enter the number of copies of the book sold");
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
            Console.WriteLine("Enter the author of the book");
            newBook.Author = Console.ReadLine();

            Console.WriteLine("Enter the title of the book");
            newBook.Title = Console.ReadLine();            

            return _booksWorkflow.AddEntity(newBook);
        }

        private void DrawQuestion(object? obj)
        {
            var value = obj is null ? "Incorrect format" : obj.ToString();
            Console.WriteLine($"The data was entered incorrectly: {value} .Select the menu item:");
            Console.WriteLine("1.Repeat the input");
            Console.WriteLine("2.Return to the main menu");
            var answer = ReadEnteredValue();
            if (answer == 1)
                AddSale();
            else 
                PrintSalesMenu();
        }

        public bool RemoveSale()
        {
            Console.WriteLine("Enter the book ID");
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int id)){
                Console.WriteLine($"The data was entered incorrectly: {input}. Select the menu item");
                PrintSalesMenu();
            }

            return _salesWorkflow.DeleteEntity(id);
        }
        public bool RemoveBook()
        {
            var sales = _salesRepository.GetAll();
            var books = _booksRepository.GetAll();

            Console.WriteLine("Enter the book ID");
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int idBook))
            {
                Console.WriteLine($"The data was entered incorrectly: {input}. Select the menu item");
                PrintBooksMenu();
            }            

            _booksWorkflow.DeleteEntityBook(idBook);            

            foreach (var sale in sales)
            {
                return _booksWorkflow.DeleteEntitySales(idBook);
            }
            return false;
            
        }

        public bool UpdateSale()
        {
            Console.WriteLine("Enter the sales ID");
            var idSale = Console.ReadLine();
            if (!long.TryParse(idSale, out long id))
            {
                Console.WriteLine($"The data was entered incorrectly: {idSale}. Select the menu item");
                PrintSalesMenu();
                return false;
            }

            var sale = _salesRepository.Get(id);
            if (sale == null)
            {
                Console.WriteLine("No record found");
                PrintSalesMenu();
                return false;
            }
            PrintSale(sale);
            Console.WriteLine("Enter a new price");
            if (long.TryParse(Console.ReadLine(), out long result))
                sale.Price = result;
            else
                Console.WriteLine();

            Console.WriteLine("Enter the new number of copies of the book sold");
            if (long.TryParse(Console.ReadLine(), out long result1))
                sale.Number_Of_Sales = result1;
            else
                Console.WriteLine();

            Console.WriteLine("Сhange ID of book?  1 - yes, 2 - no");
            var input1 = ReadEnteredValue();
            if (input1 == 1)
            {
                Console.WriteLine("Enter a new book ID");
                var input2 = ReadEnteredValue();
                sale.Book_ID = input2;

            }
            return _salesWorkflow.UpdateSaleEntity(sale);
        }

        public bool UpdateBook()
        {
            Console.WriteLine("Enter the book ID");
            var idBook = Console.ReadLine();
            if (!long.TryParse(idBook, out long id))
            {
                Console.WriteLine($"The data was entered incorrectly: {idBook}. Select the menu item");
                PrintBooksMenu();
                return false;
            }

            var book = _booksRepository.Get(id);
            if (book == null)
            {
                Console.WriteLine("No record found");
                PrintBooksMenu();
                return false;
            }
            PrintBook(book);
            Console.WriteLine("Enter a new book title");
            string title = Console.ReadLine();
            book.Title = title;

            Console.WriteLine("Enter a new book author");
            string author = Console.ReadLine();
            book.Author = author;

            Console.WriteLine("Change ID of book?  1 - yes, 2 - no");
            var input1 = ReadEnteredValue();
            if (input1 == 1)
            {
                Console.WriteLine("Enter a new book ID");
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