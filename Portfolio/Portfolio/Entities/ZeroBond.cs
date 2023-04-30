namespace Portfolio.Entities
{
    public class ZeroBond : Bond
    {
        public new string Coupon { get; set; } = "0";
        public override string ToString() => $"{base.ToString()}";
    }
}
