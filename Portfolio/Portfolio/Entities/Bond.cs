
namespace Portfolio.Entities
{
    public class Bond : EntityBase
    {
        public string? Currency { get; set; }

        public string? Term { get; set; }

        public string? BondType { get; set; }

        public decimal Nominal { get; set; }

        public decimal Coupon { get; set; }

        public override string ToString() => $"Id: {Id}, Currency: {Currency}, BondType: {BondType}, Coupon: {Coupon}";
    }
}
