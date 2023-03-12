using Portfolio.Repositories;
using Portfolio.Entities;
using Portfolio.Data;


var cashRepository = new SqlRepository<Cash>(new PortfolioDbContext());

AddCash(cashRepository);
WriteAllToConsole(cashRepository);


static void AddCash(IRepository<Cash> repository)
{
    repository.Add(new CashInHand { Currency = "PLN", Nominal = 100 });
    repository.Add(new Cash { Currency = "USD", Nominal = 300 });
    repository.Add(new Cash { Currency = "EUR", Nominal = 400 });
    repository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}