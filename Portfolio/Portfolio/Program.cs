using Portfolio.Repositories;
using Portfolio.Entities;
using Portfolio.Data;
using Portfolio.Repositories.RepositoryExtensions;
using System.Text.Json;


var bondRepository = new SqlRepository<Bond>(new PortfolioDbContext(), BondAdded);
bondRepository.ItemAdded += BondRepositoryOnItemAdded;

static void BondRepositoryOnItemAdded(object? sender, Bond e)
{
    Console.WriteLine($"Bond {e} added, from {sender?.GetType().Name}");
}

AddBond(bondRepository);

static void BondAdded(object item)
{
    var bond = (Bond)item;
    Console.WriteLine($"{bond.BondName} added");
}

static void AddBond(IRepository<Bond> repository)
{
    var bonds = new Bond[]
    {
    new ZeroBond { BondName = "OK1025", Currency = "PLN", FaceValue = 1000 },
    new FixBond { BondName = "PS0425", Currency = "PLN", FaceValue = 1000, Coupon = 0.75M },
    new FixBond { BondName = "WS0428", Currency = "PLN", FaceValue = 1000, Coupon = 2.75M },
    new FixBond { BondName = "DS1030", Currency = "PLN", FaceValue = 1000, Coupon = 1.25M }
    };
    repository.AddBatch(bonds);
}

string serializedBondRepository = JsonSerializer.Serialize(bondRepository);
Console.Write(serializedBondRepository);

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

//string newPath = $"{Directory.GetCurrentDirectory()}\\Portfolios";

//try
//{
//    if (!Directory.Exists(newPath))
//    {
//        Directory.CreateDirectory(newPath);
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//string FileName = $"{newPath}\\book.txt";

//using (var writer = File.AppendText(FileName))
//{
//    writer.Write(serializedSqlRepository);
//}
