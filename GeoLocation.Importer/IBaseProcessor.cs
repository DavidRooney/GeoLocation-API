using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocation.Importer
{
    public interface IBaseProcessor
    {
        bool Import();
    }
}
