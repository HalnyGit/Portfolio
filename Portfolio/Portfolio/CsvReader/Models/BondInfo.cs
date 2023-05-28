namespace Portfolio.CsvReader.Models;

public class BondInfo
{
    public string? Name { get; set; }
    public decimal Volume { get; set; }
    public decimal Value { get; set; }
    public int NumberOfTransactions { get; set; }
    public DateTime Maturity { get; set; }
    public decimal Coupon { get; set; }
    public decimal FixingPrice { get; set; }
    public decimal FixingYield { get; set; }
    public string? Isin { get; set; }

    public override string ToString() => $"BondName: {Name}, Coupon: {Coupon}, Maturity:{Maturity.ToShortDateString()}, YTM:{FixingYield:N2}" +
                                         $"\nVolume traded:{Volume:N0} k, Number of Transactions:{NumberOfTransactions:N0}";
}
