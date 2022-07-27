using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomArray
{
    public class CustomArray<T> : IEnumerable<T>
    {
        private T[] array;
        int first;
        int length;
        /// <summary>
        /// Should return first index of array
        /// </summary>
        public int First 
        {
            get
            {
                return first;
            }
            private set 
            {
                first = value;
            }
        }

        /// <summary>
        /// Should return last index of array
        /// </summary>
        public int Last 
        {
            get
            {
                return first + length - 1;
            } 
        }

        /// <summary>
        /// Should return length of array
        /// <exception cref="ArgumentException">Thrown when value was smaller than 0</exception>
        /// </summary>
        public int Length 
        {
            get
            {
                return length;
            }
            private set
            {
                length = value;
            }
        }

        /// <summary>
        /// Should return array 
        /// </summary>
        public T[] Array
        {
            get
            {
                return array;
            }
            set
            {
                array = value;
            }
        }


        /// <summary>
        /// Constructor with first index and length
        /// </summary>
        /// <param name="first">First Index</param>
        /// <param name="length">Length</param>         
        public CustomArray(int first, int length)
        {
            if (length <1)
            {
                throw new ArgumentException();
            }
            First = first;
            Length = length;
        }


        /// <summary>
        /// Constructor with first index and collection  
        /// </summary>
        /// <param name="first">First Index</param>
        /// <param name="list">Collection</param>
        ///  <exception cref="NullReferenceException">Thrown when list is null</exception>
        /// <exception cref="ArgumentException">Thrown when count is smaler than 0</exception>
        public CustomArray(int first, IEnumerable<T> list)
        {
            if (list ==null)
            {
                throw new NullReferenceException();
            }
            if (list.Any())
            {
                throw new ArgumentException();
            }
            First = first;
            Length = list.Count();
            array = list.ToArray();
        }

        /// <summary>
        /// Constructor with first index and params
        /// </summary>
        /// <param name="first">First Index</param>
        /// <param name="list">Params</param>
        ///  <exception cref="ArgumentNullException">Thrown when list is null</exception>
        /// <exception cref="ArgumentException">Thrown when list without elements </exception>
        public CustomArray(int first, params T[] list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }
            if (list.Length < 1)
            {
                throw new ArgumentException();
            }
            First = first;
            Length = list.Length;
            array = list;
        }

        /// <summary>
        /// Indexer with get and set  
        /// </summary>
        /// <param name="item">Int index</param>        
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown when index out of array range</exception> 
        /// <exception cref="ArgumentNullException">Thrown in set  when value passed in indexer is null</exception>
        public T this[int item]
        {
            get
            {
                if (item < First || item >= Length)
                    throw new ArgumentException();
                return Array[item-First];
            }
            set
            {
                if (item < First || item > Last)
                    throw new ArgumentException();
                if (value == null)
                    throw new ArgumentNullException();
                if (array == null)
                    array = new T[Length];
                Array[item-First] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Array.GetEnumerator();
        }
    }
}
