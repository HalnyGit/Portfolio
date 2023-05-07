using Portfolio.Entities;
using Portfolio.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace Portfolio.DataProviders;

public class BondsProvider : IBondsProvider
{
    private readonly IRepository<Bond> _bondsRepository;
    private readonly List<Bond> _bonds;

    public BondsProvider(IRepository<Bond> bondsRepository)
    {
        _bondsRepository= bondsRepository;
        _bonds = _bondsRepository.GetAll().ToList();
    }


    // select
    public List<string> GetCurrency()
    {
        return _bonds.Select(bond => bond.Currency).Distinct().ToList();
    }

    public decimal? GetLowestCoupon()
    {
        return _bonds.Select(bond => bond.Coupon).Min();
    }

    public decimal? GetHighestCoupon()
    {
        return _bonds.Select(bond => bond.Coupon).Max();
    }

    // order by
    public List<Bond> OrderyByCoupon()
    {
        return _bonds.OrderBy(bond => bond.Coupon).ToList();
    }

    public List<Bond> OrderByCurrencyAndCoupon()
    {
        return _bonds
            .OrderBy(bond => bond.Currency)
            .ThenBy(bond => bond.Coupon).ToList();
    }

    // first
    public Bond? GetLowestCouponBond()
    {
        decimal? minCoupon = GetLowestCoupon();
        return _bonds.FirstOrDefault(Bond => Bond.Coupon == minCoupon);
    }

    public Bond? GetHighestCouponBond()
    {
        decimal? maxCoupon = GetLowestCoupon();
        return _bonds.FirstOrDefault(Bond => Bond.Coupon == maxCoupon);
    }

    // where
    public List<Bond> GetOneCurrencyBondsOnly(string currency)
    {
        return _bonds.Where(Bond => Bond.Currency == currency).ToList();
    }

    // single
    public Bond? SingleOrDefaultById(int id)
    {
        return _bonds.SingleOrDefault(Bond => Bond.Id == id);
    }



}
