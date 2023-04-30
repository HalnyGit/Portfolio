namespace Portfolio.Entities
{
    public class ZeroBond : Bond
    {
        public const string Coupon = "0";
        public override string ToString() => $"{base.ToString()}";
    }
}
