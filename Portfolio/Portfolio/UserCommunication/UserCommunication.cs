using Portfolio.DataProviders;
using Portfolio.Entities;

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

    public Bond MakeBond()
    {
        Console.WriteLine("Add bond");
        string bondName = GetBondNameFromUser();
        string currency = GetCurrencyFromUser();
        decimal faceValue = GetFaceValueFromUser();

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
                FaceValue = faceValue,
                Coupon = coupon
            };
        }
        else if (bondType == "2")
        {
            bond = new ZeroBond
            {
                BondName = bondName,
                Currency = currency,
                FaceValue = faceValue,
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
}
