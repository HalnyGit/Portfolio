namespace Portfolio.Entities
{
    public class Cash : EntityBase
    {
        public string? Currency { get; set; }

        public decimal Nominal { get; set; }

        public override string ToString() => $"Id: {Id}, Currency: {Currency}, Nominal: {Nominal}";

    }
}
