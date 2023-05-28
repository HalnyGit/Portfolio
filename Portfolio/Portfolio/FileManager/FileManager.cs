using Portfolio.Entities;
using Portfolio.Repositories;
using System.Text.Json;

namespace Portfolio.FileManager;

public class FileManager : IFileManager
{
    public void CreateFilesIfNotExist()
    {
        foreach (string file in Constants.files)
        {
            if (!File.Exists(file))
            {
                using (FileStream fs = File.Create(file));
            }
        }
    }

    public void CreateDirectoryIfNotExists()
    {
        try
        {
            if (!Directory.Exists(Constants.path))
            {
                Directory.CreateDirectory(Constants.path);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }

    public void LoadBondsFromFileToRepository(IRepository<Bond> bondsRepository)
    {
        string[] lines = File.ReadAllLines($"{Constants.files[0]}");
        var numberOfLines = lines.Length;
        if (numberOfLines > 0)
        {
            foreach (string line in lines)
            {
                Bond bond = JsonSerializer.Deserialize<Bond>(line);
                bondsRepository.Add(bond);
                bondsRepository.Save();
            }
            Console.WriteLine("Portfolio content has been loaded");
        }
    }

    public void SaveRepositoryToFile(IRepository<Bond> bondsRepository)
    {
        var bonds = bondsRepository.GetAll();
        File.WriteAllText(Constants.files[0], String.Empty);
        foreach (var bond in bonds)
        {
            var json = JsonSerializer.Serialize(bond);
            using (var writer = File.AppendText(Constants.files[0]))
            {
                writer.WriteLine(json);
            }
        }
    }

    public void SaveToLog(string message)
    {
        using (var writer = File.AppendText(Constants.files[1]))
        {
            writer.WriteLine(message);
        }
    }
}
