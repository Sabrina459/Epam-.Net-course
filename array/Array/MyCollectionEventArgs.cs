using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net_Lab1
{
    class CollectionEventArgs : EventArgs
    {
        public CollectionEventArgs(string message, int index)
        {
            Message = message;
            Index = index;
        }

        public string Message { get; private set; }
        public int Index { get; private set; }
    }
}
