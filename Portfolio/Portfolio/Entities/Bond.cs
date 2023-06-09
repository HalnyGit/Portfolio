﻿namespace Portfolio.Entities;

public class Bond : EntityBase
{
    public string? BondName { get; set; }
    public string? Currency { get; set; }
    public decimal? FaceValue { get; set; }
    public decimal? Coupon { get; set; }
    public DateTime Maturity { get; set; }
    public decimal? Price { get; set; }


    public Bond()
    {
    }

    public Bond(decimal? coupon)
    {
        Coupon = coupon;
    }

    public override string ToString() => $"Id: {Id}, BondName: {BondName}, Currency: {Currency}, Coupon: {Coupon}, Maturity:{Maturity.ToShortDateString()}";






}
