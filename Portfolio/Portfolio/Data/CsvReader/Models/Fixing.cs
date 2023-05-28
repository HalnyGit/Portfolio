namespace Portfolio.Data.CsvReader.Models;

public class Fixing
{
    public string Name { get; set; }
    public string Isin { get; set; }
    public decimal FixingPrice { get; set; }
    public decimal FixingYield { get; set; }
}
