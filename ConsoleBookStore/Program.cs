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
                    if (engine.AddSale() == true)                    
                        Console.WriteLine("Record successfully added");                    
                    else
                        Console.WriteLine("The entry could not be added");
                    break;
                }
                case 2:
                {
                    if (engine.RemoveSale() == true)                    
                        Console.WriteLine("Record successfully deleted");
                    else
                        Console.WriteLine("The record could not be deleted");
                    break;
                }
                case 3:
                {
                    if (engine.UpdateSale() == true)
                        Console.WriteLine("Record successfully changed");
                    else
                        Console.WriteLine("The record could not be updated");
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
            engine.PrintAllBooks();
            engine.PrintBooksMenu();
            choice = engine.ReadEnteredValue();
            switch (choice)
            {
                case 1:
                    {
                        if (engine.AddBook() == true)
                            Console.WriteLine("Record successfully added");
                        else
                            Console.WriteLine("The recording failed additionally");
                        break;
                    }
                case 2:
                    {
                        if (engine.RemoveBook() == true)
                            Console.WriteLine("Record successfully deleted");
                        else
                            Console.WriteLine("The record could not be deleted");
                        break;
                    }
                case 3:
                    {
                        if (engine.UpdateBook() == true)
                            Console.WriteLine("Record successfully changed");
                        else
                            Console.WriteLine("The record could not be updated");
                        break;
                    }
                case 4:
                    {
                        break;
                    }
            }
            break;           
        case 3:
            return 0;       
    }

   
}