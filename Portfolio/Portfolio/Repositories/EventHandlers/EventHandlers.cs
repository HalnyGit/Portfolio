using Portfolio.Entities;
using Portfolio.FileManager;

namespace Portfolio.Repositories.EventHandlers;

public class EventHandlers
{
    private readonly IFileManager _fileManager;
    public EventHandlers(IFileManager fileManager)
    {
        _fileManager = fileManager;
    }
    public void BondAddedEventHandler(object? sender, Bond e)
    {
        Console.WriteLine($"Bond {e} - added - from {sender?.GetType().Name}");
        _fileManager.SaveToLog($"{DateTime.Now.ToString()} - Bond {e} - Added");
    }

    public void BondRepositoryOnItemRemoved(object? sender, Bond e)
    {
        Console.WriteLine($"Bond {e}, removed, from {sender?.GetType().Name}");
        _fileManager.SaveToLog($"{DateTime.Now.ToString()} - Bond {e} - Removed");
    }
}
