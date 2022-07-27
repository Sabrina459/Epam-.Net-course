using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

// A=max(C)*(Z*C)*Z+D*(MX*MS)

namespace ConsoleApp4
{
    class Program
    {
        public static Semaphore sem { get; set; }
        public static int N;
        public static int P = 4;
        public static int H;
        public static int[] C;
        public static int[] A;
        public static int[] Z;
        public static int[] D;
        public static int[,] MX;
        public static int[,] MS;
        public static int CZ;
        public static int maxC;
        public static int[] CZCZ;
        public static int[] DMXMS;
        public static int[,] MXMS;
        public static MyEventArgs ev;


        public static event EventHandler<MyEventArgs> Writed;
        public static EventWaitHandle wait = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            Writed += scan_Event;
            ev = new MyEventArgs();
            sem = new Semaphore(1, 1);
            Console.WriteLine("Enter size of vector and matrix");
            try
            {
                N = int.Parse(Console.ReadLine());
            }
            catch (NullReferenceException)
            {
            }
            MXMS = new int[N, N];
            DMXMS = new int[N];
            C = new int[N];
            CZCZ = new int[N];
            D = new int[N];
            Z = new int[N];
            MS = new int[N, N];
            A = new int[N];
            H = N / P;
            Thread T1 = new Thread(new Thread1().run);
            Thread T2 = new Thread(new Thread2().run);
            Thread T3 = new Thread(new Thread3().run);
            Thread T4 = new Thread(new Thread4().run);

            T1.Start();
            T2.Start();
            T3.Start();
            T4.Start();

            T1.Join();
            T2.Join();
            T3.Join();
            T4.Join();

        }

        public static void scan_Event(object sender, MyEventArgs e)
        {
            if (ev.WasWrittenT1 && ev.WasWrittenT3 && ev.WasWrittenT2 && ev.WasWrittenT4)
            {
                wait.Set();
                wait.Set();
                wait.Set();
                wait.Set();
                ev.Reset();
            }
            maxC = Max(C);
        }


        private class Thread1
        {
            public static int Range =H;


            public void run()
            {
                try
                {
                    OnZWrited();
                    wait.WaitOne();
                    multiplyByMatrix(MXMS, Range, MX, MS);
                    multiplyVectorByMatrix(DMXMS, Range, D , MXMS);
                    multiplyByVector(Range,Z, C);
                    multiplyConstByVector(Range, maxC*CZ, Z);
                    vectorSum(Range, CZCZ, DMXMS);
                    OnZWrited();
                    wait.WaitOne();

                    
                }
                catch (FormatException)
                {
                }
            }
            public void OnZWrited()
            {
                ev.Setwritten("T1");
                if (Writed != null) Writed(this, ev);
            }
        }

        private class Thread2
        {
            public static int Range =H*2;
            public void run()
            {
                try
                {
                    lock ("input")
                    {
                        MX = new int[N, N];
                        Console.WriteLine("Enter MX matrix (enter 'auto' to auto input or any key to manual)");
                        inputMatrix(MX, N);
                        OnZWrited();
                    }

                    wait.WaitOne();
                    multiplyByMatrix(MXMS, Range, MX, MS);
                    multiplyVectorByMatrix(DMXMS, Range, D , MXMS);
                    multiplyByVector(Range,Z, C);
                    multiplyConstByVector(Range, maxC*CZ, Z);
                    vectorSum(Range, CZCZ, DMXMS);
                    OnZWrited();
                    wait.WaitOne();
                    sem.WaitOne();
                    Console.WriteLine(toString(A));
                    sem.Release();
                }
                catch (FormatException)
                {
                }
            }
            public void OnZWrited()
            {
                ev.Setwritten("T2");
                if (Writed != null) Writed(this, ev);
            }
        }

        private class Thread3
        {
            public static int Range =H*3;

            public void run()
            {
                try
                {
                    lock ("input")
                    {

                        Console.WriteLine("Enter Z vector (enter 'auto' to auto input or any key to manual)");
                        inputVector(Z, N);
                        Console.WriteLine("Enter D vector (enter 'auto' to auto input or any key to manual)");
                        inputVector(D, N);
                        OnZWrited();
                    }
                    wait.WaitOne();
                    multiplyByMatrix(MXMS,Range,MX, MS);
                    multiplyVectorByMatrix(DMXMS, Range, D , MXMS);
                    multiplyByVector(Range,Z, C);
                    multiplyConstByVector(Range, maxC*CZ, Z);
                    vectorSum(Range, CZCZ, DMXMS);
                    OnZWrited();
                    wait.WaitOne();
                }
                catch (FormatException)
                {
                }
            }

            public void OnZWrited()
            {
                ev.Setwritten("T3");

                if (Writed != null) Writed(this, ev);
            }
        }

        private class Thread4
        {
            public static int Range =H*4;

            public void run()
            {
                try
                {
                    lock ("input")
                    {
                        Console.WriteLine("Enter C vector (enter 'auto' to auto input or any key to manual)");
                        inputVector(C, N);
                        Console.WriteLine("Enter MS matrix (enter 'auto' to auto input or any key to manual)");
                        inputMatrix(MS, N);
                        OnCWrited();
                    }
                    wait.WaitOne();
                    multiplyByMatrix(MXMS, Range, MX, MS);
                    multiplyVectorByMatrix(DMXMS, Range, D , MXMS);
                    multiplyByVector(Range,Z, C);
                    multiplyConstByVector(Range, maxC*CZ, Z);
                    vectorSum(Range, CZCZ, DMXMS);
                    OnCWrited();
                    wait.WaitOne();

                }
                catch (FormatException)
                {
                }
            }

            public void OnCWrited()
            {
                ev.Setwritten("T4");

                if (Writed != null) Writed(this, ev);
            }
        }

        public static void inputMatrix(int[,] matrix, int length)
        {
            try
            {
                if (Console.ReadLine().Equals("auto"))
                {
                    Random ran = new Random();
                    int min = -20;
                    int max = 20;
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < length; j++)
                        {
                            matrix[i, j] = ran.Next() * (max - min) + min;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < length; i++)
                    {
                        Console.WriteLine("Enter the " + (i + 1) + "matrix row with " + length + "columns");
                        for (int j = 0; j < length; j++)
                        {
                            matrix[i, j] = int.Parse(Console.ReadLine());
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Please don't enter anything beside number or 'auto'");
                inputMatrix(matrix, length);
            }
        }

        public static void inputVector(int[] vector, int length)
        {
            try
            {
                if (Console.ReadLine().Equals("auto"))
                {
                    Random ran = new Random();
                    int min = -20;
                    int max = 20;
                    for (int i = 0; i < length; i++)
                    {
                        vector[i] = ran.Next() * (max - min) + min;
                    }
                }
                else
                {
                    for (int i = 0; i < length; i++)
                    {
                        Console.WriteLine("Enter the " + (i + 1) + "vector coord");
                        vector[i] = int.Parse(Console.ReadLine());
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Please don't enter anything beside number or 'auto'");
                inputVector(vector, length);
            }
        }

        public static int Max(int[] vector)
        {
            int max = vector[0];
            foreach (int v in vector)
            {
                if (max < v) max = v;
            }

            return max;
        }

        public static void multiplyByMatrix(int[,] mResult, int range, int[,] m1, int[,] m2)
        {
            for (int i = range-H; i < range; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        mResult[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
        }
        public static void multiplyConstByVector(int range, int c1, int[] v2)
        {
            for (int i = range-H; i < range; i++)
            {
                CZCZ[i]=c1 * v2[i];
            }

        }
        public static void multiplyByVector(int range, int[] v1, int[] v2)
        {
            for (int i = range-H; i < range; i++)
            {
                Interlocked.Add(ref CZ,v1[i] * v2[i]);
            }
            
        }

        public static void vectorSum(int range,int[] vector1, int[] vector2)
        {
            for (int i = range-H; i < range; i++) A[i] = vector1[i] + vector2[i];
        }

        public static void multiplyVectorByMatrix(int[] mResult,int range, int[] v, int[,] m)
        {
            int vLength = v.Length;
            for (int i = range-H; i < range; i++)
            {
                for (int j = 0; j < vLength; j++)
                {
                    mResult[i] += v[j] * m[j, i];
                }
            }

        }

        public static string toString(int[] v)
        {
            string result = "";
            for (int i = 0; i < v.Length; i++)
            {
                result += "   " + v[i];
            }

            return result;
        }
    }
}