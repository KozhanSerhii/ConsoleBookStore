using Infrastructure;
using Infrastructure.Repositories;


using (ConsoleBookStoreContextToBook db = new ConsoleBookStoreContextToBook())
{
    // отримуємо об'єкти з бази та виводимо
    var books = db.Books.ToList();
    Console.WriteLine("Список з бази:");
    foreach (Book book in books)
    {
        Console.WriteLine($"{book.Book_ID}, {book.Author}, {book.Title}");
    }  
}
using (ConsoleBookStoreContextToSale db = new ConsoleBookStoreContextToSale())
{
    // отримуємо об'єкти з бази та виводимо
    var sales = db.Sales.ToList();
    Console.WriteLine("Список з бази:");
    foreach (Sale sale in sales)
    {
        Console.WriteLine($"{sale.Book_ID}, {sale.Price}, {sale.Number_Of_Sales}");
    }
}

SalesRepository add = new SalesRepository();
add.Add();

using (ConsoleBookStoreContextToBook db = new ConsoleBookStoreContextToBook())
{
    // отримуємо об'єкти з бази та виводимо
    var books = db.Books.ToList();
    Console.WriteLine("Список з бази:");
    foreach (Book book in books)
    {
        Console.WriteLine($"{book.Book_ID}, {book.Author}, {book.Title}");
    }
}
using (ConsoleBookStoreContextToSale db = new ConsoleBookStoreContextToSale())
{
    // отримуємо об'єкти з бази та виводимо
    var sales = db.Sales.ToList();
    Console.WriteLine("Список з бази:");
    foreach (Sale sale in sales)
    {
        Console.WriteLine($"{sale.Book_ID}, {sale.Price}, {sale.Number_Of_Sales}");
    }
}