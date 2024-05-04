using Infrastructure;
using Infrastructure.BusinessLogic;
using Infrastructure.Repositories;
using Microsoft.VisualBasic;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Reflection.Metadata.BlobBuilder;


namespace Common.DrawEngine
{
    public class DrawEngine
    {               
        private ISalesWorkflow _salesWorkflow;
        private IBooksWorkflow _booksWorkflow;
        public DrawEngine()
        {            
            _salesWorkflow = new SalesWorkflow();
            _booksWorkflow = new BooksWorkflow();
        }

        public void PrintMenu()
        {
            PrintBorder();
            Console.WriteLine("General menu:");
            Console.WriteLine("1.See the bookstore sales");
            Console.WriteLine("2.See all books");
            Console.WriteLine("3.Exit");
            PrintBorder();
        }        
        public void PrintBooksMenu()
        {
            PrintBorder();
            Console.WriteLine("Book menu:");
            Console.WriteLine("1.Add");
            Console.WriteLine("2.Remove");
            Console.WriteLine("3.Update");
            Console.WriteLine("4.Turn back");
            PrintBorder();
        }
        public void PrintSalesMenu()
        {
            PrintBorder();
            Console.WriteLine("Bookstore sales menu:");
            Console.WriteLine("1.Add");
            Console.WriteLine("2.Remove");
            Console.WriteLine("3.Update");
            Console.WriteLine("4.Turn back");
            PrintBorder();
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

        public SaleLength CalculateSalesLength() 
        {
            var sales = _salesWorkflow.GetAll();
            var books = _booksWorkflow.GetAll();

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
            var books = _booksWorkflow.GetAll();

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
        public void PrintSalesColumnsNames(SaleLength lengthSales)
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
            var sales = _salesWorkflow.GetAll();
            var books = _booksWorkflow.GetAll();

            foreach (var sale in sales)
            {
                PrintSalesLowerBorder(lengthSales);
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
        public void PrintSalesLowerBorder(SaleLength lengthSales)
        {                   
            for (int i = 0; i <= lengthSales.maxStringLength + 5; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }    
        public void PrintBorder()
        {
            for (int i = 0; i <= 26; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
        public void PrintBooksColumnsNames(BookLength lengthBooks)
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
            var books = _booksWorkflow.GetAll();
            
            foreach (var book in books)
            {
                PrintBooksLowerBorder(lengthBooks);
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
        public void PrintBooksLowerBorder(BookLength lengthBooks)
        {                     
            for (int i = 0; i <= lengthBooks.maxStringLength + 3; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
        public void PrintAllSales()
        {
            SaleLength saleLength = CalculateSalesLength();            
            PrintSalesColumnsNames(saleLength);
            PrintSalesContent(saleLength);
            PrintSalesLowerBorder(saleLength);
        }
        public void PrintAllBooks()
        {
            BookLength bookLength = CalculateBookLength();            
            PrintBooksColumnsNames(bookLength);
            PrintBooksContent(bookLength);
            PrintBooksLowerBorder(bookLength);
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
            var sales = _salesWorkflow.GetAll();
            var books = _booksWorkflow.GetAll();

            Console.WriteLine("Enter the book ID");
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int idBook))
            {
                Console.WriteLine($"The data was entered incorrectly: {input}. Select the menu item");
                PrintBooksMenu();
            }            

            _booksWorkflow.DeleteEntity(idBook);          
                       
            return true;            
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

            var sale = _salesWorkflow.Get(id);
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

            var book = _booksWorkflow.Get(id);
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

        public SaleLength CalculateSaleLength()
        {
            var sales = _salesWorkflow.GetAll();
            var books = _booksWorkflow.GetAll();

            SaleLength lengthSales = new();
            string str = "Sale ID";
            lengthSales.maxSaleIDLength = sales.Max(s => s.Sale_ID.ToString().Length) > str.Length ? sales.Max(s => s.Sale_ID.ToString().Length) : str.Length;

            str = "Book ID";
            lengthSales.maxBookIDLength = sales.Max(s => s.Book_ID.ToString().Length) > str.Length ? sales.Max(s => s.Book_ID.ToString().Length) : str.Length;

            str = "Price";
            lengthSales.maxPriceLength = sales.Max(s => s.Price.ToString().Length) > str.Length ? sales.Max(s => s.Price.ToString().Length) : str.Length;

            str = "Number Of Sales";
            lengthSales.maxNumberOfSalesLength = sales.Max(s => s.Number_Of_Sales.ToString().Length) > str.Length ? sales.Max(s => s.Number_Of_Sales.ToString().Length) : str.Length;

            lengthSales.maxStringLength = lengthSales.maxSaleIDLength + lengthSales.maxBookIDLength + lengthSales.maxPriceLength + lengthSales.maxNumberOfSalesLength;
            return lengthSales;
        }

        public void PrintSaleLowerBorder(SaleLength lengthSales)
        {
            for (int i = 0; i <= lengthSales.maxStringLength + 4; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }        
        public void PrintSaleColumnsNames(SaleLength lengthSales)
        {
            PrintSaleLowerBorder(lengthSales);            
            Console.Write("|");
            int dynamicIndent = lengthSales.maxSaleIDLength; // Динамічний відступ, який можна змінювати
            string text = "Sale ID";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthSales.maxBookIDLength; // Динамічний відступ, який можна змінювати
            text = "Book ID";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");            

            dynamicIndent = lengthSales.maxPriceLength; // Динамічний відступ, який можна змінювати
            text = "Price";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthSales.maxNumberOfSalesLength; // Динамічний відступ, який можна змінювати
            text = "Number Of Sales";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");
            Console.WriteLine();
        }
        public void PrintSaleContent(SaleLength lengthSales, int id)
        {
            var sales = _salesWorkflow.GetAll();         
                        
            var sale = sales.SingleOrDefault(s => s.Sale_ID == id);

            PrintSaleLowerBorder(lengthSales);
            Console.Write("|");

            int dynamicIndent = lengthSales.maxSaleIDLength; // Динамічний відступ, який можна змінювати
            string text = Convert.ToString(sale.Sale_ID);
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthSales.maxBookIDLength; // Динамічний відступ, який можна змінювати
            text = Convert.ToString(sale.Book_ID);
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthSales.maxPriceLength; // Динамічний відступ, який можна змінювати
            text = Convert.ToString(sale.Price); ;
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthSales.maxNumberOfSalesLength; // Динамічний відступ, який можна змінювати
            text = Convert.ToString(sale.Number_Of_Sales); ;
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            Console.WriteLine();
            PrintSaleLowerBorder(lengthSales);
        }

        public void PrintBookColumnsNames(BookLength lengthBook)
        {
            PrintBooksLowerBorder(lengthBook);
            Console.Write("|");            

            int dynamicIndent = lengthBook.maxBookIDLength; // Динамічний відступ, який можна змінювати
            string text = "Book ID";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthBook.maxTitleLength; // Динамічний відступ, який можна змінювати
            text = "Title";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthBook.maxAuthorLength; // Динамічний відступ, який можна змінювати
            text = "Author";
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");
            Console.WriteLine();
        }
        public void PrintBookContent(BookLength lengthBook, int id)
        {
            var books = _booksWorkflow.GetAll();

            var book = books.SingleOrDefault(s => s.Book_ID == id);

            PrintBooksLowerBorder(lengthBook);
            Console.Write("|");

            int dynamicIndent = lengthBook.maxBookIDLength; // Динамічний відступ, який можна змінювати
            string text = Convert.ToString(book.Book_ID);
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthBook.maxTitleLength; // Динамічний відступ, який можна змінювати
            text = Convert.ToString(book.Title);
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

            dynamicIndent = lengthBook.maxAuthorLength; // Динамічний відступ, який можна змінювати
            text = Convert.ToString(book.Author); ;
            Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");            

            Console.WriteLine();
            PrintBooksLowerBorder(lengthBook);
        }
        private void PrintSale(Sale sale)
        {
            SaleLength saleLength = CalculateSaleLength();
            PrintSaleColumnsNames(saleLength);
            PrintSaleContent(saleLength, (int)sale.Sale_ID);                     
        }
        private void PrintBook(Book book)
        {
            var bookLength = CalculateBookLength();
            PrintBookColumnsNames(bookLength);
            PrintBookContent(bookLength, (int)book.Book_ID);
        }
    }
}