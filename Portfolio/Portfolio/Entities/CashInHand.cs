namespace Portfolio.Entities
{
    public class CashInHand : Cash
    { 
        public override string ToString() => $"{base.ToString()} (In Hand)";
    }
}
