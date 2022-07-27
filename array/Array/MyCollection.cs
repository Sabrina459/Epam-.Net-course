﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Net_Lab1
{
    class MyCollection<T>: IEnumerable<T>
    {
        public event EventHandler<CollectionEventArgs> Event;
        Node<T> head;
        Node<T> tail;



        public int Count { get; private set; }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();
                Node<T> tempNode = this.head;
                for (int i = 0; i < index+1; ++i)
                    tempNode = tempNode.Next;
                return tempNode.Element;
            }

            set
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();
                Node<T> tempNode = this.head;
                for (int i = 0; i < index+1; ++i)
                    // переходим к следующему узлу списка
                    tempNode = tempNode.Next;
                tempNode.Element = value ;
            }
        }

        public void Add(T value)
        {
            if (head == null)
            {
                this.head = new Node<T>();
                this.head.Element = value;
                this.tail = this.head;
                this.head.SetNextNode(null);
            }
            else
            {
                Node<T> newNode = new Node<T>();
                this.tail.SetNextNode(newNode);
                this.tail = newNode;
                this.tail.Element = value;
                this.tail.SetNextNode(null);

            }

            Count++;
            OnEvent("Add", element: value);
        }
        public bool Insert(int index , T value)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException();
            Node<T> current = head;
            Node<T> previous = null;
            Node<T> insert = new Node<T>();
            insert.Element = value;
            int count = 0;
            while (current != null)
            {
                if (count == index)
                {
                    if (previous != null)
                    {
                        previous.SetNextNode(insert);
                        insert.SetNextNode(current);

                        if (current.Next == null)
                            tail = insert;
                    }
                    else
                    {
                        insert.SetNextNode(head);
                        head = insert;
                        if (head.Next == null)
                            tail = head;
                    }
                    Count++;
                    OnEvent("Insert", value, index);
                    return true;
                }

                previous = current;
                current = current.Next;
                count++;
            }
            return false;
        }
        public void Clear()
        {
            head = null;
            tail = null;
            Count = 0;
            OnEvent("Clear");

        }
        public bool Remove(T value)
        {
            Node<T> current = head;
            Node<T> previous = null;

            while (current != null)
            {
                if (current.Element.Equals(value))
                {
                    if (previous != null)
                    {
                        previous.SetNextNode( current.Next);

                        if (current.Next == null)
                            tail = previous;
                    }
                    else
                    {
                        head = head.Next;
                        if (head == null)
                            tail = null;
                    }
                    Count--;
                    OnEvent("Remove", element: value);
                    return true;
                }

                previous = current;
                current = current.Next;
            }
            return false;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.Element;
                current = current.Next;
            }
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException();
            Node<T> current = head;
            Node<T> previous = null;
            int count = 0;

            while (current != null)
            {
                if (count == index)
                {
                    if (previous != null)
                    {
                        previous.SetNextNode(current.Next);

                        if (current.Next == null)
                            tail = previous;
                    }
                    else
                    {
                        head = head.Next;
                        if (head == null)
                            tail = null;
                    }
                    Count--;
                    OnEvent("RemoveAt", index: count);
                }

                previous = current;
                current = current.Next;
                count++;
            }
        }
        public int FindIndex(T element)
        {
            Node<T> current = head;
            int count =0;

            while (current != null)
            {
                if (current.Element.Equals(element))
                {
                    return count;

                }
                current = current.Next;
                count++;
            }

            return -1;
        }
        public bool Contains(T element)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Element.Equals(element))
                    return true;
                current = current.Next;
            }
            return false;
        }
        public string toString()
        {
            string str ="";
            Node<T> current = head;
            while (current != null)
            {
                str += current.Element + " ";
                current = current.Next;

            }
            return String.Format("[{0}]",str);
        }

        private void OnEvent(string message, T element = default, int index = -1)
        {
            if (Event != null)
                Event(this, new CollectionEventArgs(message, index));
        }
    }
    

}
