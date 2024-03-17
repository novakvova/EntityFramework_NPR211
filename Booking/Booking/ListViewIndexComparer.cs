using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    internal class ListViewIndexComparer : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            return ((ListViewItem)x).Index - ((ListViewItem)y).Index;
        }
    }
}
