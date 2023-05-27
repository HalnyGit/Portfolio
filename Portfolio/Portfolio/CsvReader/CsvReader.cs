
using Portfolio.CsvReader.Extensions;
using Portfolio.CsvReader.Models;
using System.Globalization;

namespace Portfolio.CsvReader;

public class CsvReader : ICsvReader
{
    public List<FixingBond> ProcessFixingBonds(string filePath)
    {
        if(!File.Exists(filePath))
        {
            return new List<FixingBond>();
        }

        var fixingBonds = File.ReadAllLines(filePath)
            .Skip(1)
            .Where(x => x.Length > 0)
            .ToFixingBond();

        return fixingBonds.ToList();
    }

    public List<Fixing> ProcessFixing(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<Fixing>();
        }

        var fixings = File.ReadAllLines(filePath)
            .Skip(1)
            .Where(line => line.Length > 0)
            .Select(line =>
            {
                var columns = line.Split(',');
                return new Fixing
                {
                      Name = columns[0],
                      Isin = columns[1],
                      FixingPrice = decimal.Parse(columns[6], CultureInfo.InvariantCulture),
                      FixingYield = decimal.Parse(columns[7], CultureInfo.InvariantCulture)
                };
            });

        return fixings.ToList();
    }

    public List<Stats> ProcessStats(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<Stats>();
        }

        var stats = File.ReadAllLines(filePath)
            .Skip(1)
            .Where(line => line.Length > 0)
            .Select(line =>
            {
                var columns = line.Split(',');
                return new Stats
                {
                    Name = columns[0],
                    Volume = decimal.Parse(columns[1].Replace(" ",""), CultureInfo.InvariantCulture),
                    Value = decimal.Parse(columns[2].Replace(" ", ""), CultureInfo.InvariantCulture),
                    NumberOfTransactions = int.Parse(columns[3])
                };
            });

        return stats.ToList();
    }
}
