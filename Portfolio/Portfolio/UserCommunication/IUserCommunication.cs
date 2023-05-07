
using Portfolio.Entities;
using Portfolio.Repositories;

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
}
