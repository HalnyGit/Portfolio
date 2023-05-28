using Portfolio.Data.CsvReader.Extensions;
using Portfolio.Data.CsvReader.Models;
using Portfolio.Data.DataProviders;
using Portfolio.Entities;
using Portfolio.Data.XmlManager;
using System.Collections.Generic;

namespace Portfolio.UserCommunication;

public class UserCommunication : IUserCommunication
{
    public void ShowMenu()
    {
        Console.WriteLine("-----------Bond portfolio-----------");
        Console.WriteLine("----------------Menu----------------");
        Console.WriteLine("(1) Display content of the portfolio");
        Console.WriteLine("(2) Add Bond");
        Console.WriteLine("(3) Remove Bond");
        Console.WriteLine("(4) Show market");
        Console.WriteLine("(5) Show best yielding bonds in the market");
        Console.WriteLine("(q) Quit");
        Console.WriteLine("------------------------------------");
    }

    public void DisplayPortfolio(IEnumerable<Bond> bonds)
    {
        if(bonds.ToList().Count == 0)
        {
            Console.WriteLine("Portfolio content: empty");
        }
        else
        {
            Console.WriteLine("Portfolio content:");
            foreach (var bond in bonds)
            {
                Console.WriteLine(bond);
            }
        }
    }

    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    public int GetBondIdFromUser()
    {
        int id = 0;
        bool canParse = false;
        while (!canParse)
        {
            Console.WriteLine("Add bond -> Enter bond ID (must be number):");
            var input = Console.ReadLine();
            canParse = int.TryParse(input, out id);
        }
        return id;
    }

    public string GetBondNameFromUser()
    {
        Console.WriteLine("Add bond -> Enter bond name:");
        return Console.ReadLine();
    }

    public string GetCurrencyFromUser()
    {
        Console.WriteLine("Add bond -> Enter currency name (ISO code):");
        return Console.ReadLine();
    }

    public decimal GetFaceValueFromUser()
    {
        decimal fv = 0;
        bool canParse = false;
        while (!canParse)
        {
            Console.WriteLine("Add bond -> Enter bond Face Value (must be number):");
            var input = Console.ReadLine();
            canParse = Decimal.TryParse(input, out fv);
        }
        return fv;
    }

    public decimal GetCouponFromUser()
    {
        decimal coupon = 0;
        bool canParse = false;
        while (!canParse)
        {
            Console.WriteLine("Add bond -> Enter bond coupon (must be number):");
            var input = Console.ReadLine();
            canParse = Decimal.TryParse(input, out coupon);
        }
        return coupon;
    }

    public Bond MakeBond()
    {
        Console.WriteLine("Add bond");
        string bondName = GetBondNameFromUser();
        string currency = GetCurrencyFromUser();
        DateTime maturityDate = GetMaturityDateFromUser();

        Console.WriteLine("Choose bond type: \n (1) - for fix bond \n (2) - for zero bond:");
        var bondType = Console.ReadLine();

        var bond = new Bond();
        if (bondType == "1")
        {
            decimal coupon = GetCouponFromUser();
            bond = new FixBond
            {
                BondName = bondName,
                Currency = currency,
                Coupon = coupon,
                Maturity = maturityDate
            };
        }
        else if (bondType == "2")
        {
            bond = new ZeroBond
            {
                BondName = bondName,
                Currency = currency,
                Maturity = maturityDate
            };
        }
        return bond;
    }

    public int SelectBondToRemove(IBondsProvider bondsProvider)
    {
        Console.WriteLine("Remove bond");

        int idToRemove = GetBondIdFromUser();
        var existingIds = bondsProvider.GetIds();
        if (existingIds.Count == 0)
        {
            Console.WriteLine("No bonds in portfolio to remove");
        }
        else
        {
            while (!existingIds.Contains(idToRemove))
            {
                Console.WriteLine($"ID:{idToRemove} does not exist in the repository");
                idToRemove = GetBondIdFromUser();
            }       
        }
        return idToRemove;
    }

    public DateTime GetMaturityDateFromUser()
    {
        DateTime maturityDate = default(DateTime);
        bool canParse = false;
        while (!canParse)
        {
            Console.WriteLine("Input maturity date");

            int year = intParser("Input year (must be number)");
            int month = intParser("Input month (must be number)");
            int day = intParser("Input day (must be number)");

            try
            {
                maturityDate = new DateTime(year, month, day);
                canParse = true;
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("This date is not valid. Please try again.");
            }
        }
        return maturityDate;
    }

    public int intParser(string message)
    {
        int i = 0;
        bool canParse = false;
        while (!canParse)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();
            canParse = int.TryParse(input, out i);
        }
        return i;
    }

    public void ProcessMarketInfo(MarketStats marketStats)
    {
        List<BondInfo> bondList = new List<BondInfo>();

        Console.WriteLine("Best yielding bonds up to 2 years:");
        var bestYieldingBondUp2y = marketStats.GetBestYieldingBondWithinMaturitiesRange(2);
        marketStats.WriteToConsoleBestYieldingBond(bestYieldingBondUp2y);
        if(bestYieldingBondUp2y != null)
        {
            bondList.Add(bestYieldingBondUp2y);
        }

        Console.WriteLine("\nBest yielding bonds from 2 to 5 years:");
        var bestYieldingBondBetween2And5y = marketStats.GetBestYieldingBondWithinMaturitiesRange(2, 5);
        marketStats.WriteToConsoleBestYieldingBond(bestYieldingBondBetween2And5y);
        if (bestYieldingBondBetween2And5y != null)
        {
            bondList.Add(bestYieldingBondBetween2And5y);
        }

        Console.WriteLine("\nBest yielding bonds from 5 years:");
        var bestYieldingBondAfter5y = marketStats.GetBestYieldingBondWithinMaturitiesRange(null, 5);
        marketStats.WriteToConsoleBestYieldingBond(bestYieldingBondAfter5y);
        if (bestYieldingBondAfter5y != null)
        {
            bondList.Add(bestYieldingBondAfter5y);
        }

        Console.WriteLine("\nExport data to xml file? y - for yes, any key - for no");
        var saveToXml = Console.ReadLine();
        if (saveToXml == "y")
        {
            var xmlManager = new XmlManager();
            xmlManager.CreateXmlDocWithBestYieldingBonds(bondList);
            Console.WriteLine("Data exported to xml");
        }
    }
}

