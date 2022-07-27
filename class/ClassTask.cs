using System.Collections.Generic;
using System.Linq;
using System;

namespace Class
{
    public class Rectangle{
        private double sideA;
        private double sideB;

        public Rectangle(double a, double b) {
            sideA = a;
            sideB = b;
        }
        public Rectangle(double a) {
            sideA = a;
            sideB = 5;
        
        }
        public Rectangle()
        {
            sideA = 4;
            sideB = 3;

        }
        public double GetSideA() {
            return sideA;
        }
        public double GetSideB()
        {
            return sideB;
        }
        public double Area()
        {
            return sideA * sideB;
        }
        public double Perimeter()
        {
            return 2*(sideA + sideB);
        }

        public bool IsSquare()
        {
            if (sideB.Equals(sideA))
            {
                return true;
            }
            return false;
        }

        public void ReplaceSides()
        {
            double side = sideA;
            sideA = sideB;
            sideB = side;

        }
    }

    public class ArrayRectangles {
        private readonly Rectangle[] rectangle_array;
        public ArrayRectangles(int n) {
            rectangle_array = new Rectangle[n];
        
        }
        public ArrayRectangles(IEnumerable<Rectangle> rectangles) {
            rectangle_array = rectangles.ToArray();
        
        }

        public bool AddRectangle(Rectangle rectangle) {
            for (int i = 0; i < rectangle_array.Length; i++)
            {
                if (rectangle_array[i] == null)
                {
                    rectangle_array[i] = rectangle;
                    return true;
                }
            }
            return false;
        }

        public int NumberMaxArea()
        {
            int maxIndex = 0;
            double maxArea = 0;
            for (int i = 0; i < rectangle_array.Length; i++)
            {
                if (rectangle_array[i].Area() > maxArea)
                {
                    maxIndex = i;
                    maxArea = rectangle_array[i].Area();


                }
            }
            return maxIndex;
        }

        public int NumberMinPerimeter()
        {
            int minIndex = 0;
            double minPerimetr = rectangle_array[0].Perimeter();
            for (int i = 0; i < rectangle_array.Length; i++)
            {
                if (rectangle_array[i].Perimeter() < minPerimetr)
                {
                    minIndex = i;
                    minPerimetr = rectangle_array[i].Perimeter();


                }
            }
            return minIndex;
        }

        public int NumberSquare()
        {
            int count = 0;
            for (int i = 0; i < rectangle_array.Length; i++)
            {
                if (rectangle_array[i].IsSquare())
                {
                    count++;
                }
            }
            return count;
        }
    }
}
