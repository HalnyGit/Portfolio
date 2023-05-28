using Portfolio.Entities;

namespace Portfolio.Data.DataProviders;

public interface IBondsProvider
{
    List<Bond> GetBonds();

    // select
    List<string?> GetCurrency();
    List<int> GetIds();
    decimal? GetLowestCoupon();
    decimal? GetHighestCoupon();

    // order by
    List<Bond> OrderyByCoupon();

    List<Bond> OrderByCurrencyAndCoupon();

    // first
    Bond? GetLowestCouponBond();
    Bond? GetHighestCouponBond();

    // where
    List<Bond> GetOneCurrencyBondsOnly(string currency);

    // single
    Bond? SingleOrDefaultById(int id);

}
