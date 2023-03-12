namespace Portfolio.Entities
{
    public class Deposit : EntityBase
    {
        public string? Currency { get; set; }

        public string? Term { get; set; }

        public decimal Nominal { get; set; }

        public decimal Rate { get; set; }

        public override string ToString() => $"Id: {Id}, Currency: {Currency}, Nominal: {Nominal}, Term: {Term}, Rate: {Rate}";

    }
}
