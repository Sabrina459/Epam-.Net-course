using System;

namespace Function
{
    public enum SortOrder { Ascending, Descending }
    public static class Function
    {
        //TODO :Define public static method 'IsSorted'  that indicate  correctness of sorting array with a given sort order .The values should be passed into the method in such order : array ,order. 
        public static bool IsSorted(int[] array, SortOrder order)
        {
            switch (order)
	        {
		        case SortOrder.Ascending:
                    for (int i = 0; i < array.Length-1; i++)
			        {
                        if (array[i]>array[i+1])
	                    {
                            return false;
	                    }

			        }
                    return true;
                case SortOrder.Descending:
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        if (array[i] < array[i + 1])
                        {
                            return false;
                        }

                    }
                    return true;
            }
            return false;
            
        }

        // TODO :Define public static  method 'Transform' -  that will increase each array element by its index, if array IsSorted in  SortOrder  . The values should be passed into the method in such order : array ,order .

        public static void Transform(int[] array, SortOrder order)
        {
            if (IsSorted(array,order))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = array[i] + i;

                }
            }
        }

        //TODO :Define public static  method 'MultArithmeticElements' that calculate and return   multiply   for  first n members of arifmetic  progression . The values should be passed into the method in such order : a ,t ,n . 

        public static double MultArithmeticElements(double a, double t, int n) 
        {
            double mult = 1;
            for (int i = 0; i < n; i++)
            {
                mult *= a;
                a += t;
            }
            return mult;
        }
        //TODO :Define public static method 'SumGeometricElements'  that calculate and return   sum for members of geometric progression ,while  element is smaller than limit value. The values should be passed into the method in such order: a ,t ,alim .  
        public static double SumGeometricElements(double a, double t, double alim)
        {
            double sum = 0;
            if (t<0 || t>=1)
            {
                return double.PositiveInfinity;
            }
            while(a > alim)
            {
                sum += a;
                a *= t;
            }
            return sum;

        }
    }
}
