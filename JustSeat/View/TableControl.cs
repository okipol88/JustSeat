using JustSeat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JustSeat.View
{
    public partial class TableControl : Control
    {
        public TableControl()
        {
            InitializeComponent();
        }

        public Table Table { get; set; }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            var table = Table;
            if (table == null)
                return;
        }
    }
}
