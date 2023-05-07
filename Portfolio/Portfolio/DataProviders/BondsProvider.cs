using Portfolio.Entities;
using Portfolio.Repositories;

namespace Portfolio.DataProviders;

public class BondsProvider : IBondsProvider
{
    private readonly IRepository<Bond> _bondsRepository;


    public BondsProvider(IRepository<Bond> bondsRepository)
    {
        _bondsRepository= bondsRepository;

    }

    public List<Bond> GetBonds()
    // if var _bonds is declared as a private field (variable) and it is initialized in the constructor with the value returned from the _bondsRepository.GetAll().ToList()
    // then this implementation causes an issue because the  following methods (for instance GetCurrency())are not aware of any changes made to _bonds after its initialization.
    // so, if new bonds are added to the repository after the initialization of _bonds, they will not be present in the _bonds list,
    // which can result in inconsistencies between the returned results and the actual data in the repository and in short _bonds can return null list
    {
        return _bondsRepository.GetAll().ToList();
    }


    // select
    public List<int> GetIds()
    {
        return GetBonds().Select(bond => bond.Id).Distinct().ToList();
    }

    public List<string?> GetCurrency()
    {
        return GetBonds().Select(bond => bond.Currency).Distinct().ToList();
    }
    public decimal? GetLowestCoupon()
    {
        return GetBonds().Select(bond => bond.Coupon).Min();
    }
    public decimal? GetHighestCoupon()
    {
        return GetBonds().Select(bond => bond.Coupon).Max();
    }

// order by
    public List<Bond> OrderyByCoupon()
{
    return GetBonds().OrderBy(bond => bond.Coupon).ToList();
}
    public List<Bond> OrderByCurrencyAndCoupon()
    {
        return GetBonds()
            .OrderBy(bond => bond.Currency)
            .ThenBy(bond => bond.Coupon).ToList();
    }

    // first
    public Bond? GetLowestCouponBond()
    {
        decimal? minCoupon = GetLowestCoupon();
        return GetBonds().FirstOrDefault(Bond => Bond.Coupon == minCoupon);
    }

    public Bond? GetHighestCouponBond()
    {
        decimal? maxCoupon = GetHighestCoupon();
        return GetBonds().FirstOrDefault(Bond => Bond.Coupon == maxCoupon);
    }

    // where
    public List<Bond> GetOneCurrencyBondsOnly(string currency)
    {
        return GetBonds().Where(Bond => Bond.Currency == currency).ToList();
    }

    // single
    public Bond? SingleOrDefaultById(int id)
    {
        return GetBonds().SingleOrDefault(Bond => Bond.Id == id);
    }


}
