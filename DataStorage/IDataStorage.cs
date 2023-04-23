using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public interface IDataStorage<T>
    {
        void Insert(T read);
    }
}
