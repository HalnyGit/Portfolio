﻿
namespace Portfolio.Entities
{
    public class Bond : EntityBase
    {
        public string? BondName { get; set; }
        public string? Currency { get; set; }
        public decimal? FaceValue { get; set; }
        public string? Coupon { get; set; }

        public override string ToString() => $"Id: {Id}, BondName: {BondName}, Currency: {Currency}, FaceValue: {FaceValue}, Coupon: {Coupon}";
    }
}