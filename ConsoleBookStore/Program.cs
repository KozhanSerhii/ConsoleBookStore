using Infrastructure;
using Infrastructure.Repositories;
using DrawEngine;


while (1 > 0)
{
    Sale sale1 = new Sale();
    Book book1 = new Book();
    Draw engine = new Draw();
    engine.Menu();
    int choice = Convert.ToInt32(Console.ReadLine());
    switch (choice)
    {
        case 1:
            {
                engine.Print();
                engine.Sales();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1: 
                        {
                            Console.WriteLine("Введіть автора книги");
                            book1.Author = Console.ReadLine();

                            Console.WriteLine("Введіть назву книги");
                            book1.Title = Console.ReadLine();

                            Console.WriteLine("Введіть ціну книги");
                            if (long.TryParse(Console.ReadLine(), out long result1))
                                sale1.Price = result1;
                            else
                                Console.WriteLine();

                            Console.WriteLine("Введіть кількість проданих примірників книги");
                            if (long.TryParse(Console.ReadLine(), out long result2))
                                sale1.Number_Of_Sales = result2;
                            else
                                Console.WriteLine();

                            SalesRepository addSale = new SalesRepository();
                            BooksRepository addBook = new BooksRepository();
                            
                            if (addSale.Add(sale1) == true && addBook.Add(book1) == true)
                                Console.WriteLine("Запис успішно доданий");

                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Введіть назву книжки яку хочете видалити");
                            string tittleRemove = Console.ReadLine().Trim().ToLower().Replace(" ", "");
                            BooksRepository removeBook = new BooksRepository();
                            SalesRepository removeSale = new SalesRepository();                            
                           
                            if (removeBook.Remove(tittleRemove, out long book_ID) == true && removeSale.Remove(book_ID) == true)                            
                                Console.WriteLine("Запис успішно видалений");                           
                            
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Введіть назву книжки запис якої хочете редагувати");
                            string tittleUpdate = Console.ReadLine().Trim().ToLower().Replace(" ", "");
                            BooksRepository updateBook = new BooksRepository();
                            SalesRepository updateSale = new SalesRepository();

                            using (var context = new ConsoleBookStoreContext())
                            {
                                var entityToUpdate = context.Books.FirstOrDefault(b => b.Title.Trim().ToLower().Replace(" ", "") == tittleUpdate);
                                long book_ID = entityToUpdate != null ? entityToUpdate.Book_ID : 0;
                                Console.WriteLine("Введіть оновленого автора книги");
                                entityToUpdate.Author = Console.ReadLine();

                                Console.WriteLine("Введіть оновлену назву книги");
                                entityToUpdate.Title = Console.ReadLine();

                                var entityToUpdate2 = context.Sales.FirstOrDefault(s => s.Sale_ID == book_ID);

                                Console.WriteLine("Введіть оновлену ціну книги");
                                if (long.TryParse(Console.ReadLine(), out long result1))
                                    entityToUpdate2.Price = result1;
                                else
                                    Console.WriteLine();

                                Console.WriteLine("Введіть оновлену кількість проданих примірників книги");
                                if (long.TryParse(Console.ReadLine(), out long result2))
                                    entityToUpdate2.Number_Of_Sales = result2;
                                else
                                    Console.WriteLine();

                                if (updateBook.Update(entityToUpdate) == true && updateSale.Update(entityToUpdate2) == true)
                                    Console.WriteLine("Запис успішно змінений");                                
                            } 
                            break;
                        }
                    case 4:                        
                        break;
                    default: 
                        {
                            break;
                        }
                }
                break;
            }
        case 2:
            {
                Console.WriteLine("Робота програми завершена");
                return 0;
            }
        default:
            break;
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
