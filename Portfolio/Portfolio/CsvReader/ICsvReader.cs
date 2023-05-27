using Portfolio.CsvReader.Models;

namespace Portfolio.CsvReader;

public interface ICsvReader
{

    List<FixingBond> ProcessFixingBonds(string filePath);
    List<Fixing> ProcessFixing(string filePath);
    List<Stats> ProcessStats(string filePath);


}
