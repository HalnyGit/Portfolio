using Portfolio.Data.CsvReader.Models;

namespace Portfolio.Data.CsvReader.Extensions;

public class MarketStats
{
    private List<FixingBond> _bonds;
    private List<Fixing> _fixings;
    private List<Stats> _turnover;


    public MarketStats()
    {
        CsvReader csvReader = new CsvReader();
        _bonds = csvReader.ProcessFixingBonds("Resources\\Files\\bonds.csv");
        _fixings = csvReader.ProcessFixing("Resources\\Files\\fixing.csv");
        _turnover = csvReader.ProcessStats("Resources\\Files\\stats.csv");
    }


    private List<BondInfo> ProcessBondsAndFixingsAndTurnover()
    {

        var result = new List<BondInfo>();
        var bondsAndFixings = _bonds.Join(_fixings,
            x => x.Name,
            x => x.Name,
            (_bonds, _fixings) => new
            {
                _bonds.Name,
                _bonds.Isin,
                _bonds.Maturity,
                _bonds.Coupon,
                _fixings.FixingYield
            });

        var bondsAndFixingsAndTurnover = bondsAndFixings.Join(_turnover,
            x => x.Name,
            x => x.Name,
            (bondsAndFixings, _turnover) => new BondInfo
            {
                Name = bondsAndFixings.Name,
                Isin = bondsAndFixings.Isin,
                Maturity = bondsAndFixings.Maturity,
                Coupon = bondsAndFixings.Coupon,
                FixingYield = bondsAndFixings.FixingYield,
                Volume = _turnover.Volume,
                NumberOfTransactions = _turnover.NumberOfTransactions
            });
        foreach (var bond in bondsAndFixingsAndTurnover)
        {
            result.Add(bond);
        }
        return result;
    }

    public void WriteMarketInfoToConsole()
    {
        var bondsInfo = ProcessBondsAndFixingsAndTurnover();
        foreach (var bondInfo in bondsInfo)
        {
            Console.WriteLine($"{bondInfo}\n");
        }
    }

    public BondInfo? GetBestYieldingBondWithinMaturitiesRange(int? lowerBound = null, int? upperBound = null)
    {
        var bonds = ProcessBondsAndFixingsAndTurnover();
        var filteredBonds = bonds;
        if (lowerBound.HasValue && !upperBound.HasValue)
        {
            filteredBonds = filteredBonds.Where(bond => bond.Maturity < DateTime.Now.AddYears(lowerBound.Value)).ToList();
        }
        if (lowerBound.HasValue && upperBound.HasValue)
        {
            filteredBonds = filteredBonds.Where(bond => bond.Maturity >= DateTime.Now.AddYears(lowerBound.Value)
                                                && bond.Maturity < DateTime.Now.AddYears(upperBound.Value)).ToList();
        }
        if (!lowerBound.HasValue && upperBound.HasValue)
        {
            filteredBonds = filteredBonds.Where(bond => bond.Maturity >= DateTime.Now.AddYears(upperBound.Value)).ToList();
        }

        var maxYieldingBond = filteredBonds
            .OrderByDescending(bond => bond.FixingYield)
            .FirstOrDefault();

        return maxYieldingBond;
    }

    public void WriteToConsoleBestYieldingBond(BondInfo? bondInfo)
    {

        if (bondInfo != null)
        {
            Console.WriteLine($"{bondInfo}\n");
        }
        else
        {
            Console.WriteLine("No bond found within the specified criteria");
        }
    }
}

