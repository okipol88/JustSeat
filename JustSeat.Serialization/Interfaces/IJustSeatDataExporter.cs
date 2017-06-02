using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustSeat.Serialization.Interfaces
{
    public interface IJustSeatDataExporter
    {
        string Export(JustSeat justSeatModel);
    }
}
