using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public interface IDataStorageFactory
    {
        IDataStorage<IDeal> DeadFactory(ISecurity security);
    }
}
