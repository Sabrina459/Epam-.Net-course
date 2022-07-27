using System;
using System.Collections.Generic;
using System.Linq;
using Linq.Objects;

namespace Linq
{
    public static class Tasks
    {
        //The implementation of your tasks should look like this:
        public static string TaskExample(IEnumerable<string> stringList)
        {
            return stringList.Aggregate<string>((x, y) => x + y);
        }

        #region Low

        public static IEnumerable<string> Task1(char c, IEnumerable<string> stringList)
        {
            return stringList.Where(el => el.StartsWith(c) && el.EndsWith(c)&& el.Length >1).Select(x => x);
            
        }

        public static IEnumerable<int> Task2(IEnumerable<string> stringList)
        {
            return stringList.Select(x => x.Length).OrderBy(x => x);
        }

        public static IEnumerable<string> Task3(IEnumerable<string> stringList)
        {
            return stringList.Select(x => x[0].ToString()+x[x.Length-1].ToString());
        }

        public static IEnumerable<string> Task4(int k, IEnumerable<string> stringList)
        {
            return stringList.OrderBy(x => x).Where(x => x.Length == k && Char.IsDigit(x[^1])).Select(x => x);
        }

        public static IEnumerable<string> Task5(IEnumerable<int> integerList)
        {
            return integerList.OrderBy(x => x).Where(x => x % 2 != 0).Select(x => x.ToString());
        }

        #endregion

        #region Middle

        public static IEnumerable<string> Task6(IEnumerable<int> numbers, IEnumerable<string> stringList)
        {
            return numbers.Select(n => stringList.FirstOrDefault(el => Char.IsDigit(el[0]) && el.Length == n) ?? "Not found");
        }

        public static IEnumerable<int> Task7(int k, IEnumerable<int> integerList)
        {
            return integerList.Where(i => i % 2 == 0 && !integerList.Skip(k).Contains(i)).Reverse();
        }
        
        public static IEnumerable<int> Task8(int k, int d, IEnumerable<int> integerList)
        {

            return integerList.TakeWhile(x => x <= d).Union(integerList.Skip(k)).OrderByDescending(x => x);
        }

        public static IEnumerable<string> Task9(IEnumerable<string> stringList)
        {
            return stringList.GroupBy(el => el[0]).Select(el => el.Sum(e => e.Length).ToString() + "-" + el.Key.ToString()).OrderByDescending(x=>int.Parse(x.Split("-")[0])).ThenBy(x=>x[^1]);
        }

        public static IEnumerable<string> Task10(IEnumerable<string> stringList)
        {
            return stringList.OrderBy(s => s).GroupBy(s => s.Length).Select(k => new string(k.Select(s => char.ToUpper(s[s.Length - 1])).ToArray())).OrderByDescending(s => s.Length);
        }

        #endregion

        #region Advance

        public static IEnumerable<YearSchoolStat> Task11(IEnumerable<Entrant> nameList)
        {
            return nameList.GroupBy(x => x.Year).Select(x => new YearSchoolStat
            {
                NumberOfSchools = x.GroupBy(n => n.SchoolNumber).Count(),
                Year = x.Key
            }).OrderBy(x => x.NumberOfSchools).ThenBy(x => x.Year);
        }

        public static IEnumerable<NumberPair> Task12(IEnumerable<int> integerList1, IEnumerable<int> integerList2)
        {
            var result = integerList1.Select((f, s) => integerList2.Where(s => f.ToString()[^1] == s.ToString()[^1]));

            return result.Select(x => new NumberPair {Item1 = x.First(), Item2 = x.Last() });
        }

        public static IEnumerable<YearSchoolStat> Task13(IEnumerable<Entrant> nameList, IEnumerable<int> yearList)
        {
            return yearList.Select(y => new YearSchoolStat() 
            {
                Year = y,
                NumberOfSchools = nameList.TakeWhile(n => n.Year == y).Count()
            }).OrderBy(x => x.NumberOfSchools).ThenBy(x => x.Year);
        }

        public static IEnumerable<MaxDiscountOwner> Task14(IEnumerable<Supplier> supplierList,
                IEnumerable<SupplierDiscount> supplierDiscountList)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<CountryStat> Task15(IEnumerable<Good> goodList,
            IEnumerable<StorePrice> storePriceList)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
