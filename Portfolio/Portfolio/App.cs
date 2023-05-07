using Portfolio.DataProviders;
using Portfolio.Entities;
using Portfolio.Repositories;

namespace Portfolio;

public class App : IApp
{
    private readonly IRepository<Bond> _bondsRepository;
    private readonly IBondsProvider _bondsProvider;

    public App(IRepository<Bond> bondsRepository,
                IBondsProvider bondsProvider)
    {
        _bondsRepository = bondsRepository;
        _bondsProvider = bondsProvider;
    }

    public void Run()
    {

        var bonds = GenerateSampleBonds();

        foreach (var bond in bonds)
        {
            _bondsRepository.Add(bond);
        }

        // reading
       var items = _bondsRepository.GetAll();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }


        foreach (var item in _bondsProvider.GetCurrency())
        {
            Console.WriteLine($"Currency {item}");
        }

        Console.WriteLine(_bondsProvider.GetLowestCouponBond());


    }

    public static List<Bond> GenerateSampleBonds()
    {
        return new List<Bond>
        {
           new FixBond
           {
               BondName = "DS1023",
               Currency = "PLN",
               FaceValue = 1000,
               Coupon = 4.0M,
           },
           new FixBond
           {
               BondName = "PS0424",
               Currency = "PLN",
               FaceValue = 1000,
               Coupon = 2.5M,
           },
           new FixBond
           {
               BondName = "DS0725",
               Currency = "PLN",
               FaceValue = 1000,
               Coupon = 3.25M,
           },
           new FixBond
           {
               BondName = "DS1033",
               Currency = "PLN",
               FaceValue = 1000,
               Coupon = 6.0M,
           },

           new FixBond
           {
               BondName = "US0233",
               Currency = "USD",
               FaceValue = 1000,
               Coupon = 3.5M,
           },

           new FixBond
           {
               BondName = "DE0233",
               Currency = "EUR",
               FaceValue = 1000,
               Coupon = 2.3M,
           },

           new ZeroBond
           {
               BondName = "OK0724",
               Currency = "PLN",
               FaceValue = 1000,
           },
           new ZeroBond
           {
               BondName = "OK1025",
               Currency = "PLN",
               FaceValue = 1000,
           },
        };
    }





}
