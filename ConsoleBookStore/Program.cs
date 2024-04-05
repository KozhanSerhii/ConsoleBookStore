using ConsoleBookStore;

using (BookStoreContext db = new BookStoreContext())
{
    // отримуємо об'єкти з бази та виводимо
    var sales = db.Sales.ToList();
    Console.WriteLine("Список з бази:");
    foreach (Sale sale in sales)
    {
        Console.WriteLine($"{sale.Id}.{sale.Author}, {sale.Book}, {sale.Price}, {sale.Number_of_sales}");
    }
}