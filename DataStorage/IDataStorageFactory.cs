using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public interface IDataStorageFactory
    {
        IDealStorage DeadFactory(ISecurity security);
    }
}
