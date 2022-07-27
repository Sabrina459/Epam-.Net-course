using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
     public class NumberPair
    {
        public int Item1 { get; set; }

        public int Item2 { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var i in Task12(new[] { 1, 12, 4, 5, 78 },
                new[] { 1, 42, 75, 65, 8, 97 })) 
            {
                Console.WriteLine(i.Item1.ToString(), i.Item2.ToString());
            }
            Console.ReadLine();
        }
        public static IEnumerable<NumberPair> Task12(IEnumerable<int> integerList1, IEnumerable<int> integerList2)
        {
            var result = integerList1.Select((f, s) => integerList2.Where(s => f.ToString()[^1] == s.ToString()[^1]));


            return result.Select(x => new NumberPair { Item1 = x.First(), Item2 = x.Last() });
        }
    }
}
