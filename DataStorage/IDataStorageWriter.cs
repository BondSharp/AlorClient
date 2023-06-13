using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataStorage
{
    public interface IDataStorageWriter
    {
        void Write(ISecurity security, IDeal deal);
        void Write(ISecurity security, IOrderBook orderBook);


    }
}
