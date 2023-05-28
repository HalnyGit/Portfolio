using Portfolio.Data.CsvReader.Models;

namespace Portfolio.Data.XmlManager;

public interface IXmlManager
{
    void CreateXmlDocWithBestYieldingBonds(List<BondInfo> bondInfo);
}
