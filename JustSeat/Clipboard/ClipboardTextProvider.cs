using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustSeat.Clipboard
{
    class ClipboardTextProvider : IProvideClipboardText
    {
        public string ClipboarText
        {
            get
            {
                var txt = System.Windows.Clipboard.GetText(System.Windows.TextDataFormat.Text);

                return txt;
            }
        }
    }
}
