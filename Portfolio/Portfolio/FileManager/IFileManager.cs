using Portfolio.Entities;
using Portfolio.Repositories;

namespace Portfolio.FileManager;

public interface IFileManager
{
    void CreateFilesIfNotExist();
    void CreateDirectoryIfNotExists();
    void LoadBondsFromFileToRepository(IRepository<Bond> bondsRepository);
    void SaveRepositoryToFile(IRepository<Bond> bondsRepository);
    void SaveToLog(string message);
}
