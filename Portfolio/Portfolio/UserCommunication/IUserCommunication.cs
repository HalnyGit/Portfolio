using Portfolio.Data.CsvReader.Extensions;
using Portfolio.Data.DataProviders;
using Portfolio.Entities;


namespace Portfolio.UserCommunication;

public interface IUserCommunication
{
    void ShowMenu();
    void DisplayPortfolio(IEnumerable<Bond> bonds);
    int GetBondIdFromUser();
    string GetBondNameFromUser();
    string GetCurrencyFromUser();
    decimal GetFaceValueFromUser();
    decimal GetCouponFromUser();
    void DisplayMessage(string message);
    Bond MakeBond();
    int SelectBondToRemove(IBondsProvider bondsProvider);
    DateTime GetMaturityDateFromUser();
    void ProcessMarketInfo(MarketStats marketStats);
}
