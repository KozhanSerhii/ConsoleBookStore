using Infrastructure;
using Common.DrawEngine;
using Infrastructure.BusinessLogic;


while (1 > 0)
{
    DrawEngine engine = new DrawEngine();
    engine.PrintMenu();
    int choice = engine.ReadEnteredValue();
    switch(choice)
    { 
        case 1:
        {
            engine.PrintAllSales();
            engine.PrintSalesMenu();
            choice = engine.ReadEnteredValue();
            switch (choice)
            {   
                case 1: 
                {
                    SalesDto newSale = new SalesDto();
                    engine.AddSale(newSale);
                    SalesWorkflow addSales = new SalesWorkflow();
                    if (addSales.AddEntity(newSale) == true)                    
                        Console.WriteLine("Запис успішно доданий");                    
                    else
                        Console.WriteLine("Запис не вдалося додати");
                    break;
                }
                case 2:
                {
                    if (engine.RemoveSale() == true)                    
                        Console.WriteLine("Запис успішно видалений");
                    else
                        Console.WriteLine("Такої книжки немає в продажах");
                    break;
                }
                case 3:
                {
                    break;
                }
                case 4:
                {
                    break;
                }                
            }
            break;
        }
        case 2:
            return 0;       
    }

   
}
































//Sale sale1 = new Sale();
//Book book1 = new Book();

//Console.WriteLine("Введіть номер id книги");
//if (long.TryParse(Console.ReadLine(), out long result3))
//{ 
//    sale1.Book_ID = result3;
//    book1.Book_ID = result3;
//}
//else
//    Console.WriteLine();

//Console.WriteLine("Введіть автора книги");
//book1.Author = Console.ReadLine();

//Console.WriteLine("Введіть назву книги");
//book1.Title = Console.ReadLine();

//Console.WriteLine("Введіть ціну книги");
//if (long.TryParse(Console.ReadLine(), out long result1))
//    sale1.Price = result1;
//else
//    Console.WriteLine();

//Console.WriteLine("Введіть кількість проданих примірників книги");
//if (long.TryParse(Console.ReadLine(), out long result2))
//    sale1.Number_Of_Sales = result2;
//else
//    Console.WriteLine();


//SalesRepository add = new SalesRepository();
//add.Add(sale1, book1);

using (ConsoleBookStoreContext db = new ConsoleBookStoreContext())
{
    // отримуємо об'єкти з бази та виводимо
    var books = db.Books.ToList();
    Console.WriteLine("Список з бази:");
    foreach (Book book in books)
    {
        Console.WriteLine($"{book.Book_ID}, {book.Author}, {book.Title}");
    }
}
using (ConsoleBookStoreContext db = new ConsoleBookStoreContext())
{
    // отримуємо об'єкти з бази та виводимо
    var sales = db.Sales.ToList();
    Console.WriteLine("Список з бази:");
    foreach (Sale sale in sales)
    {
        Console.WriteLine($"{sale.Sale_ID}, {sale.Price}, {sale.Number_Of_Sales}");
    }
}
