using CloudComputingPT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudComputingPT.DataAccess.Interfaces
{
    public interface ICacheAccess
    {
        void SaveData(PricesDictionary item);
        string FetchData(string key);
    }
}
