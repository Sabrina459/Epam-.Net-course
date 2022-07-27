using System;
using System.Collections.Generic;

namespace Condition
{
    public static class Condition
    {
        /// <summary>
        /// Implement code according to description of  task 1
        /// </summary>        
        public static int Task1(int n)
        {
            //TODO :Delete line below and write your own solution 
            if (n >= 0)
            {
                return n * n;
            }
            else
            {
                return -n;
            }
        }

        /// <summary>
        /// Implement code according to description of  task 2
        /// </summary>  
        public static int Task2(int n)
        {
            char[] numbers = n.ToString().ToCharArray();
            Console.WriteLine(numbers.ToString());
            int max ;
            string permutation = "";
            for (int i = 0; i < numbers.Length; i++)
            {
                max = numbers[0] - '0';
                if (numbers[i]-'0' > max)
                {
                    numbers[0] = numbers[i];
                    numbers[i] = Char.Parse(max.ToString());

                }
                
            }
            permutation += numbers.ToString();
            Int32.TryParse(permutation, out max);
            return max;
        }
    }
}
