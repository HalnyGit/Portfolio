using Portfolio.DataProviders;
using Portfolio.Entities;
using Portfolio.FileManager;
using Portfolio.Repositories;
using Portfolio.UserCommunication;

namespace Portfolio;

public class App : IApp
{
    private readonly IRepository<Bond> _bondsRepository;
    private readonly IBondsProvider _bondsProvider;
    private readonly IUserCommunication _userCommunication;
    private readonly IFileManager _fileManager;

    public App(IRepository<Bond> bondsRepository,
                IBondsProvider bondsProvider,
                IUserCommunication userCommunication,
                IFileManager fileManager)
    {
        _bondsRepository = bondsRepository;
        _bondsProvider = bondsProvider;
        _userCommunication = userCommunication;
        _fileManager = fileManager;
    }

    public void Run()
    {
        _fileManager.CreateDirectoryIfNotExists();
        _fileManager.CreateFilesIfNotExist();
        _fileManager.LoadBondsFromFileToRepository(_bondsRepository);

        _bondsRepository.ItemAdded += BondRepositoryOnItemAdded;
        _bondsRepository.ItemRemoved += BondRepositoryOnItemRemoved;


        Console.WriteLine("====================================");

        _userCommunication.ShowMenu();

        bool closeApp = false;

        while(true)
        {
            var input = Console.ReadLine();
            if(input == "1") // display portfolio
            {
                _userCommunication.DisplayPortfolio(_bondsProvider.GetBonds());
            }
            else if (input == "2") // add bond
            {
                Console.WriteLine("Add bond");
                string bondName = _userCommunication.GetBondNameFromUser();
                string currency = _userCommunication.GetCurrencyFromUser();
                decimal faceValue = _userCommunication.GetFaceValueFromUser();

                Console.WriteLine("Choose bond type: \n (1) - for fix bond \n (2) - for zero bond:");
                var bondType = Console.ReadLine();

                var bond = new Bond();
                if (bondType == "1")
                {
                    decimal coupon = _userCommunication.GetCouponFromUser();
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
                _bondsRepository.Add(bond);
            }
            else if (input == "3") // remove bond
            {
                Console.WriteLine("Remove bond");

                int idToRemove = _userCommunication.GetBondIdFromUser();
                var existingIds = _bondsProvider.GetIds();
                if(existingIds.Count == 0)
                {
                    Console.WriteLine("No bonds in portfolio to remove");
                }
                else
                {
                    while (!existingIds.Contains(idToRemove))
                    {
                        Console.WriteLine($"ID:{idToRemove} does not exist in the repository");
                        idToRemove = _userCommunication.GetBondIdFromUser();
                    }
                    Bond bondToRemove = _bondsRepository.GetById(idToRemove);
                    _bondsRepository.Remove(bondToRemove);
                }              
            }

            if(input == "q") // quit application
            {
                //_bondsProvider.Save();
                closeApp = true;
                _userCommunication.DisplayMessage("Application closed");
                break;
            }
            else
            {
                _userCommunication.DisplayMessage("\nPlease continue, select an action from the menu:");
                _userCommunication.ShowMenu();
            }
        }

    }

    //public static List<Bond> GenerateSampleBonds()
    //{
    //    return new List<Bond>
    //    {
    //       new FixBond
    //       {
    //           BondName = "DS1023",
    //           Currency = "PLN",
    //           FaceValue = 1000,
    //           Coupon = 4.0M,
    //       },
    //       new FixBond
    //       {
    //           BondName = "PS0424",
    //           Currency = "PLN",
    //           FaceValue = 1000,
    //           Coupon = 2.5M,
    //       },
    //       new FixBond
    //       {
    //           BondName = "DS0725",
    //           Currency = "PLN",
    //           FaceValue = 1000,
    //           Coupon = 3.25M,
    //       },
    //       new FixBond
    //       {
    //           BondName = "DS1033",
    //           Currency = "PLN",
    //           FaceValue = 1000,
    //           Coupon = 6.0M,
    //       },

    //       new FixBond
    //       {
    //           BondName = "US0233",
    //           Currency = "USD",
    //           FaceValue = 1000,
    //           Coupon = 3.5M,
    //       },

    //       new FixBond
    //       {
    //           BondName = "DE0233",
    //           Currency = "EUR",
    //           FaceValue = 1000,
    //           Coupon = 2.3M,
    //       },

    //       new ZeroBond
    //       {
    //           BondName = "OK0724",
    //           Currency = "PLN",
    //           FaceValue = 1000,
    //       },
    //       new ZeroBond
    //       {
    //           BondName = "OK1025",
    //           Currency = "PLN",
    //           FaceValue = 1000,
    //       },
    //    };
    //}





}
