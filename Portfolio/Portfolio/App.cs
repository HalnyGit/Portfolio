using Portfolio.CsvReader;
using Portfolio.DataProviders;
using Portfolio.Entities;
using Portfolio.FileManager;
using Portfolio.Repositories;
using Portfolio.Repositories.EventHandlers;
using Portfolio.UserCommunication;

namespace Portfolio;

public class App : IApp
{
    private readonly IRepository<Bond> _bondsRepository;
    private readonly IBondsProvider _bondsProvider;
    private readonly IUserCommunication _userCommunication;
    private readonly IFileManager _fileManager;
    private readonly EventHandlers _eventHandlers;
    private readonly ICsvReader _csvReader;

    public App(IRepository<Bond> bondsRepository,
                IBondsProvider bondsProvider,
                IUserCommunication userCommunication,
                IFileManager fileManager,
                EventHandlers eventHandlers,
                ICsvReader csvReader)
    {
        _bondsRepository = bondsRepository;
        _bondsProvider = bondsProvider;
        _userCommunication = userCommunication;
        _fileManager = fileManager;
        _eventHandlers = eventHandlers;
        _csvReader = csvReader;
    }

    public void Run()
    {
        //var fixingBonds = _csvReader.ProcessFixingBonds("Resources\\Files\\bonds.csv");
        //var fixings = _csvReader.ProcessFixing("Resources\\Files\\fixing.csv");
        //var stats = _csvReader.ProcessStats("Resources\\Files\\stats.csv");

        //_fileManager.CreateDirectoryIfNotExists();
        //_fileManager.CreateFilesIfNotExist();
        //_fileManager.LoadBondsFromFileToRepository(_bondsRepository);
        //_bondsRepository.ItemAdded += _eventHandlers.BondAdded;
        //_bondsRepository.ItemRemoved += _eventHandlers.BondRemoved;

        //Console.WriteLine("====================================");

        //_userCommunication.ShowMenu();

        //bool closeApp = false;

        //while(true)
        //{
        //    var input = Console.ReadLine();
        //    if(input == "1") // display portfolio
        //    {
        //        _userCommunication.DisplayPortfolio(_bondsProvider.GetBonds());
        //    }
        //    else if (input == "2") // add bond
        //    {

        //        _bondsRepository.Add(_userCommunication.MakeBond());
        //    }
        //    else if (input == "3") // remove bond
        //    {
        //        Bond bondToRemove = _bondsRepository.GetById(_userCommunication.SelectBondToRemove(_bondsProvider));
        //        _bondsRepository.Remove(bondToRemove);   
        //    }

        //    if (input == "q") // quit application
        //    {
        //        //_bondsProvider.Save();
        //        _fileManager.SaveRepositoryToFile(_bondsRepository);
        //        closeApp = true;
        //        _userCommunication.DisplayMessage("Application closed");
        //        break;
        //    }
        //    else
        //    {
        //        _userCommunication.DisplayMessage("\nPlease continue, select an action from the menu:");
        //        _userCommunication.ShowMenu();
        //    }
        //}
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
