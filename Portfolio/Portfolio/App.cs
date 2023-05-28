using Portfolio.Data.CsvReader;
using Portfolio.Data.CsvReader.Extensions;
using Portfolio.Data.DataProviders;
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
    private readonly MarketStats _marketStats;

    public App(IRepository<Bond> bondsRepository,
                IBondsProvider bondsProvider,
                IUserCommunication userCommunication,
                IFileManager fileManager,
                EventHandlers eventHandlers,
                ICsvReader csvReader,
                MarketStats marketStats)
    {
        _bondsRepository = bondsRepository;
        _bondsProvider = bondsProvider;
        _userCommunication = userCommunication;
        _fileManager = fileManager;
        _eventHandlers = eventHandlers;
        _csvReader = csvReader;
        _marketStats = marketStats;
    }

    public void Run()
    {
        _fileManager.CreateDirectoryIfNotExists();
        _fileManager.CreateFilesIfNotExist();
        _fileManager.LoadBondsFromFileToRepository(_bondsRepository);
        _bondsRepository.ItemAdded += _eventHandlers.BondAdded;
        _bondsRepository.ItemRemoved += _eventHandlers.BondRemoved;

        Console.WriteLine("====================================");

        _userCommunication.ShowMenu();

        bool closeApp = false;

        while (true)
        {
            var input = Console.ReadLine();
            if (input == "1") // display portfolio
            {
                _userCommunication.DisplayPortfolio(_bondsProvider.GetBonds());
            }
            else if (input == "2") // add bond
            {

                _bondsRepository.Add(_userCommunication.MakeBond());
            }
            else if (input == "3") // remove bond
            {
                Bond bondToRemove = _bondsRepository.GetById(_userCommunication.SelectBondToRemove(_bondsProvider));
                _bondsRepository.Remove(bondToRemove);
            }
            else if(input == "4") // show market
            {
                _marketStats.WriteMarketInfoToConsole();
            }
            else if (input == "5") // show best yielding bonds and save to xml
            {
                _userCommunication.ProcessMarketInfo(_marketStats);
            }

            if (input == "q") // quit application
            {
                //_bondsProvider.Save();
                _fileManager.SaveRepositoryToFile(_bondsRepository);
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
}
