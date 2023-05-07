using Microsoft.Extensions.Primitives;
using Portfolio.Entities;
using Portfolio.Repositories;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

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
}
