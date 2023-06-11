using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IOffer
    {
        double Price { get; }
        int Volume { get; }
    }
}
