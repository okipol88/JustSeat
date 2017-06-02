using JustSeat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustSeat.Serialization
{
    public class Event
    {
        public List<Guest> NotSeatedGuets { get; set; }

        public List<Table> Tables { get; set; }
    }
}
