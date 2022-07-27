using System;

namespace ConsoleApp4
{
    public class MyEventArgs : EventArgs
    {
        public MyEventArgs()
        {
            WasWrittenT1 = false;
            WasWrittenT2 = false;
            WasWrittenT3 = false;
            WasWrittenT4 = false;


        }

        public void Reset()
        {
            WasWrittenT1 = false;
            WasWrittenT2 = false;
            WasWrittenT3 = false;
            WasWrittenT4 = false;
        }

        public void Setwritten(string name)
        {
            if (name.Equals("T1"))
                WasWrittenT1 = true;
            if (name.Equals("T2"))
                WasWrittenT2 = true;
            if (name.Equals("T3"))
                WasWrittenT3 = true;
            if (name .Equals("T4"))
                WasWrittenT4 = true;
        }
        public bool WasWrittenT1
        {
            get;
            private set;
        }
        public bool WasWrittenT2
        {
            get;
            private set;
        }
        public bool WasWrittenT3
        {
            get;
            private set;
        }
        public bool WasWrittenT4
        {
            get;
            private set;
        }
    }
}