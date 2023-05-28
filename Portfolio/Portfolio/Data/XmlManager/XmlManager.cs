
using Portfolio.Data.CsvReader.Models;
using System.Xml.Linq;

namespace Portfolio.Data.XmlManager;

public class XmlManager : IXmlManager
{

    public void CreateXmlDocWithBestYieldingBonds(List<BondInfo> bondInfo)
    {
        var document = new XDocument();
        var bestBonds = new XElement("Bond", bondInfo
            .Select(bond =>
                    new XElement("Bond",
                    new XAttribute("Name", bond.Name),
                    new XAttribute("Coupon", bond.Coupon),
                    new XAttribute("Maturity", bond.Maturity),
                    new XAttribute("YTM", bond.FixingYield),
                    new XAttribute("Volume", bond.Volume),
                    new XAttribute("Number_Of_Transactions", bond.NumberOfTransactions))
            ));
        document.Add(bestBonds);
        document.Save("bestBonds.xml");
    }
}
