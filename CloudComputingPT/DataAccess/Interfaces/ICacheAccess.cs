using CloudComputingPT.Models;

namespace CloudComputingPT.DataAccess.Interfaces
{
    public interface ICacheAccess
    {
        void SaveData(PricesDictionary item);
        string FetchData(string key);
    }
}
