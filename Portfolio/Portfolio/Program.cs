using Microsoft.Extensions.DependencyInjection;
using Portfolio;
using Portfolio.DataProviders;
using Portfolio.Entities;
using Portfolio.Repositories;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Bond>, ListRepository<Bond>>();
services.AddSingleton<IBondsProvider, BondsProvider>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();
app.Run();



//var b1 = new FixBond
//{
//    BondName = "DS1023",
//    Currency = "PLN",
//    FaceValue = 1000,
//    Coupon = 6.5M
//};

//var b2 = new ZeroBond
//{
//    BondName = "OK0724",
//    Currency = "PLN",
//    FaceValue = 1000,
//};

//Console.WriteLine(b1);
//Console.WriteLine(b2); 













//using Portfolio.Repositories;
//using Portfolio.Entities;
//using Portfolio.Data;
//using Portfolio.Repositories.RepositoryExtensions;
//using System.Text.Json;
//using System.Runtime.CompilerServices;
//using System.Reflection.Metadata.Ecma335;
//using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

//try
//{
//    if (!Directory.Exists(Constants.path))
//    {
//        Directory.CreateDirectory(Constants.path);
//    }
//}
//catch (Exception exception)
//{
//    Console.WriteLine(exception.Message);
//}


//foreach (string file in Constants.files)
//{
//    if (!CheckFileExists(file))
//    {
//        using (FileStream fs = File.Create(file)) ;
//    }
//}

//ShowMenu();

//var bondRepository = new SqlRepository<Bond>(new PortfolioDbContext());
//bondRepository.ItemAdded += BondRepositoryOnItemAdded;
//bondRepository.ItemRemoved += BondRepositoryOnItemRemoved;

//string[] lines = File.ReadAllLines($"{Constants.path}\\portfolio.txt");
//var numberOfLines = lines.Length;
//if (numberOfLines > 0)
//{
//    foreach (string line in lines)
//    {
//        Bond bond = JsonSerializer.Deserialize<Bond>(line);
//        bondRepository.Add(bond);
//        bondRepository.Save();
//    }
//    Console.WriteLine("Portfolio content has been loaded");
//}

//bool closeApp = false;

//while (true)
//{
//    var input = Console.ReadLine();
//    if (input == "1")
//    {
//        Console.WriteLine("Portfolio content:");
//        var bonds = bondRepository.GetAll();
//        foreach (Bond bond in bonds)
//        {
//            Console.WriteLine(bond);
//        }
//    }
//    else if (input == "2")
//    {
//        Console.WriteLine("Add bond -> Enter bond name");
//        string bondName = Console.ReadLine();

//        Console.WriteLine("Choose bond type: \n (1) - for fix bond \n (2) - for zero bond:");

//        string bondType = Console.ReadLine();
//        if (bondType == "1")
//        {
//            Console.WriteLine("Enter coupon of the bond");
//            string couponString = Console.ReadLine();
//            decimal coupon;
//            if(Decimal.TryParse(couponString, out coupon))
//            {
//                FixBond fixbond = new FixBond { BondName = bondName, Currency = "PLN", FaceValue = 1000, Coupon = coupon };
//                bondRepository.Add(fixbond);
//            }
//            else
//            {
//                Console.WriteLine("Incorrect coupon data type");
//            }
//        }
//        else if (bondType == "2")
//        {
//            ZeroBond zerobond = new ZeroBond { BondName = bondName, Currency = "PLN", FaceValue = 1000, Coupon = 0};
//            bondRepository.Add(zerobond);
//        }
//        bondRepository.Save();
//        SaveRepoToFile(bondRepository, Constants.files[0]);

//    }
//    else if (input == "3")
//    {
//        Console.WriteLine("Remove bond -> Enter bond Id");
//        string idToRemove = Console.ReadLine();
//        Bond bondToRemove = bondRepository.GetById(int.Parse(idToRemove));
//        bondRepository.Remove(bondToRemove);
//        bondRepository.Save();

//    }

//    if (input == "q")
//    {
//        SaveRepoToFile(bondRepository, Constants.files[0]);
//        closeApp = true;
//        Console.WriteLine("Application closed");
//        break;
//    }
//    else
//    {
//        Console.WriteLine("\nPlease continue, select an action from the menu:");
//        ShowMenu();
//    }
//}

//static void ShowMenu()
//{
//    Console.WriteLine("-----------Bond portfolio-----------");
//    Console.WriteLine("----------------Menu----------------");
//    Console.WriteLine("(1) Display content of the portfolio");
//    Console.WriteLine("(2) Add Bond");
//    Console.WriteLine("(3) Remove Bond");
//    Console.WriteLine("(q) Quit");
//    Console.WriteLine("------------------------------------");
//}
//static void SaveRepoToFile(IRepository<Bond> repository, string file)
//{
//    var bonds = repository.GetAll();
//    File.WriteAllText(file, String.Empty);
//    foreach (var bond in bonds)
//    {
//        var json = JsonSerializer.Serialize(bond);
//        using (var writer = File.AppendText(file))
//        {
//            writer.WriteLine(json);
//        }
//    }
//}

//static void SaveToLog(string message)
//{
//    using (var writer = File.AppendText(Constants.files[1]))
//    {
//        writer.WriteLine(message);
//    }
//}

//static bool CheckFileExists(string fileName)
//{
//    return File.Exists(fileName);
//}

//static void BondRepositoryOnItemAdded(object? sender, Bond e)
//{
//    Console.WriteLine($"Bond {e} - added - from {sender?.GetType().Name}");
//    SaveToLog($"{DateTime.Now.ToString()} - Bond {e} - Added");
//}

//static void BondRepositoryOnItemRemoved(object? sender, Bond e)
//{
//    Console.WriteLine($"Bond {e}, removed, from {sender?.GetType().Name}");
//    SaveToLog($"{DateTime.Now.ToString()} - Bond {e} - Removed");
//}
