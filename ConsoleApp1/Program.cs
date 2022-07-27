using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] m = new int[,] { { 1, 0, 0 }, { 1, 1, 0 }, { 1, 1, 1 } };
            ChangeMatrixDiagonally(m);
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {

                        Console.Write(m[i, j] + " ");
                }
                Console.WriteLine();

            }
        }
        public static int Task2(int n)
        {
            string numbers = n.ToString();

            Console.WriteLine(numbers);
            char max = numbers[0] ;
            string permutation = "";
            while (numbers != null)
            {
                foreach (char c in numbers)
                {
                    if (c > max)
                    {
                        max = c;
                    }
                }
                numbers.Remove(max);
                
                permutation += max.ToString();
                Console.WriteLine(permutation);
            }

            permutation = numbers.ToString();
            Console.WriteLine(permutation);
            return Int32.Parse(permutation);
        }
        
    }
}
