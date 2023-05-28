namespace Portfolio.Data.CsvReader.Models;

public class FixingBond
{
    public string Name { get; set; }
    public string Isin { get; set; }
    public DateTime Maturity { get; set; }
    public decimal Coupon { get; set; }

}
