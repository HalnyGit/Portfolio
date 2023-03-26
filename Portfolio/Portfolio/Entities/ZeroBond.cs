namespace Portfolio.Entities
{
    public class ZeroBond : Bond
    {
        public const decimal Coupon = 0;
        public override string ToString() => $"{base.ToString()} Coupon: {Coupon}";
    }
}
