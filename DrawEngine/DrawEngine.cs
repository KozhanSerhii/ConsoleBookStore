using Infrastructure;


namespace DrawEngine
{
    public class Draw
    {       
        public void Menu()
        {
            Console.WriteLine("Оберіть пункт меню:");
            Console.WriteLine("1.Переглянути продажі книгарні");            
            Console.WriteLine("2.Вийти з програми");
        }
        public void Sales()
        {
            Console.WriteLine("Оберіть пункт меню:");
            Console.WriteLine("1.Додати запис до таблиці");
            Console.WriteLine("2.Видалити запис з таблиці");
            Console.WriteLine("3.Редактувати запис з таблиці");
            Console.WriteLine("4.Повернутися назад");
        }
        public void Print()
        {
            using (ConsoleBookStoreContext db = new ConsoleBookStoreContext())
            {
                var booksAndSales = from book in db.Books
                                    join sale in db.Sales on book.Book_ID equals sale.Sale_ID into bookSales
                                    from sale in bookSales.DefaultIfEmpty()
                                    select new { BookTitle = book.Title, BookAuthor = book.Author, SalePrice = sale != null ? sale.Price : 0, SaleNumberOfSales = sale != null ? sale.Number_Of_Sales : 0 };

                foreach (var item in booksAndSales)
                {
                    Console.WriteLine($"{item.BookTitle}, {item.BookAuthor}, {item.SalePrice}, {item.SaleNumberOfSales}");
                }
            }
        }

    }

}