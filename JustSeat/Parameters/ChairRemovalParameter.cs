using JustSeat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JustSeat.Parameters
{
    public class ChairRemovalParameter
    {
        public Table Table { get; set; }
        public Chair ChairToRemove { get; set; }
    }
}
