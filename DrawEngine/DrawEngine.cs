using Infrastructure;
using Infrastructure.BusinessLogic;
using Infrastructure.Repositories;
using Microsoft.VisualBasic;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
            var number = Console.ReadLine();

            if (int.TryParse(number, out int result1))
                return result1;
            else
                DrawQuestion(number);

            return result1;                    
        }

        public SaleLength CalculateSalesLength() 
        {
            var sales = _salesWorkflow.GetAll();
            var books = _booksWorkflow.GetAll();
            if (sales.Count == 0 )
            {
                return new SaleLength
                { maxSaleIDLength = "Sale ID".Length, maxTitleLength = "Title".Length, maxAuthorLength = "Author".Length, maxPriceLength = "Price".Length, maxNumberOfSalesLength = "Number Of Sales".Length, maxStringLength = "Sale ID".Length+ "Title".Length+ "Author".Length+ "Price".Length+ "Number Of Sales".Length + 12, maxTotalAmountLength = 12};
            }
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

            str = "Total amount";
            lengthSales.maxTotalAmountLength =  str.Length;

            lengthSales.maxStringLength = lengthSales.maxSaleIDLength + lengthSales.maxTitleLength + lengthSales.maxAuthorLength + lengthSales.maxPriceLength + lengthSales.maxNumberOfSalesLength + lengthSales.maxTotalAmountLength;
            return lengthSales;
        }
        public BookLength CalculateBookLength()
        {
            var books = _booksWorkflow.GetAll();
            if (books.Count == 0)
            {
                return new BookLength
                { maxBookIDLength = "Book ID".Length, maxTitleLength = "Title".Length, maxAuthorLength = "Author".Length, maxStringLength = 18 };
            }
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
            for (int i = 0; i <= lengthSales.maxStringLength + 6; i++)
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

            dynamicIndent = lengthSales.maxTotalAmountLength; // Динамічний відступ, який можна змінювати
            text = "Total Amount";
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

                dynamicIndent = lengthSales.maxTotalAmountLength; // Динамічний відступ, який можна змінювати
                int result = (Convert.ToInt32(sale.Price) * Convert.ToInt32(sale.Number_Of_Sales));
                text = result.ToString();
                Console.Write(text + new string(' ', dynamicIndent - text.Length) + "|");

                Console.WriteLine();
            }
        }
        public void PrintSalesLowerBorder(SaleLength lengthSales)
        {                   
            for (int i = 0; i <= lengthSales.maxStringLength + 6; i++)
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

            string author = Console.ReadLine();
            if (author != string.Empty)
                newSale.Author = author;
            else
            {
                DrawQuestion(author);
                return false;
            }


            Console.WriteLine("Enter the title of the book");
            string title = Console.ReadLine();            
            if (title != string.Empty)
                newSale.Title = title;
            else
            {
                DrawQuestion(title);
                return false;
            }

            Console.WriteLine("Enter the price of the book");
            var price = Console.ReadLine();
            if (long.TryParse(price, out long result1) && result1 > 0)
                newSale.Price = result1;
            else
            {
                DrawQuestion(price);
                return false;
            }
                

            Console.WriteLine("Enter the number of copies of the book sold");
            var number = Console.ReadLine();
            if (long.TryParse(number, out long result2) && result2 > 0)
                newSale.Number_Of_Sales = result2;
            else
            {
                DrawQuestion(number);
                return false;
            }

            return _salesWorkflow.AddEntity(newSale);
        }
        public bool AddBook()
        {
            var newBook = new BookDto();

            Console.WriteLine("Enter the author of the book");
            string author = Console.ReadLine();
            if (author != string.Empty)            
                newBook.Author = author;
            else
            {
                DrawQuestion(author);
                return false;
            }

            Console.WriteLine("Enter the title of the book");
            string title = Console.ReadLine();
            if (title != string.Empty)
                newBook.Title = title;
            else
            {
                DrawQuestion(title);
                return false;
            }

            return _booksWorkflow.AddEntity(newBook);
        }

        private void DrawQuestion(object? obj)
        {
            var value = obj is null ? "Incorrect format" : obj.ToString();
            Console.WriteLine($"The data was entered incorrectly: {value}");            
            Console.WriteLine("You will return to the general menu");                         
        }

        public bool RemoveSale()
        {
            Console.WriteLine("Enter the Sale ID");
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int id)){
                Console.WriteLine($"The data was entered incorrectly: {input}. Select the menu item");
                PrintSalesMenu();
            }

            return _salesWorkflow.DeleteEntity(id);
        }
        public bool RemoveBook()
        {         
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

            Console.WriteLine("Change ID of book?  1 - yes, 2 - no");
            var input1 = Convert.ToInt32(Console.ReadLine());
            if (input1 == 1)
            {
                Console.WriteLine("Enter a new book ID");
                if (long.TryParse(Console.ReadLine(), out long result3))
                {
                    sale.Book_ID = result3;                    
                }
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
            if (title != string.Empty)
                book.Title = title;
            else
            {
                Console.WriteLine($"The data was entered incorrectly: '{title}'. Select the menu item");                
                return false;
            }                

            Console.WriteLine("Enter a new book author");
            string author = Console.ReadLine();
            if (author != string.Empty)
                book.Author = author;
            else
            {
                Console.WriteLine($"The data was entered incorrectly: '{author}'. Select the menu item");                
                return false;
            }           

            return _booksWorkflow.UpdateBookEntity(book);
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

            if (book == null) 
            {
                throw new Exception("The Book not found");
            }

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