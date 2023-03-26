namespace Portfolio.Entities
{
    public class FixBond : Bond
    {
        public decimal? Coupon { get; set; }

        public override string ToString() => $"{base.ToString()} Coupon: {Coupon}";
    }
}
