

using Portfolio.CsvReader.Models;
using System.Globalization;

namespace Portfolio.CsvReader.Extensions;

public static class FixingBondExtensions
{
    public static IEnumerable<FixingBond> ToFixingBond(this IEnumerable<string> source)
    {
        foreach (var line in source)
        {
            var columns = line.Split(',');

            yield return new FixingBond
            {
                Name = columns[0],
                Isin = columns[1],
                Maturity = DateTime.ParseExact(columns[2], "dd-MM-yyyy", CultureInfo.InvariantCulture),
                Coupon = decimal.Parse(columns[3], CultureInfo.InvariantCulture),
            };
        }
    }
}
