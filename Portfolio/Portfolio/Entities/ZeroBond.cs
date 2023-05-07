namespace Portfolio.Entities;

public class ZeroBond : Bond
{
    public const decimal zeroCoupon = 0;

    public ZeroBond() : base(zeroCoupon)
    {
    }
    public override string ToString() => $"{base.ToString()}";

}
