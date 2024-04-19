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
                        Console.WriteLine("Запис не вдалося видалити");
                    break;
                }
                case 3:
                {
                    if (engine.UpdateSale() == true)
                        Console.WriteLine("Запис успішно змінений");
                    else
                        Console.WriteLine("Запис не вдалося оновити");
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