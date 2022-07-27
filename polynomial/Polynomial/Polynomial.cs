using System;
using System.Collections.Generic;
using PolynomialObject.Exceptions;
using System.Linq;

namespace PolynomialObject
{
    public sealed class Polynomial
    {
        private readonly List<PolynomialMember> polynomialMembers;
        public Polynomial()
        {
            polynomialMembers = new List<PolynomialMember>();
        }

        public Polynomial(PolynomialMember member):this()
        {
            polynomialMembers.Add(member);

        }

        public Polynomial(IEnumerable<PolynomialMember> members)
        {
            if (members !=null)

                polynomialMembers = new List<PolynomialMember>(members);
        }

        public Polynomial((double degree, double coefficient) member):this()
        {
            polynomialMembers.Add(new PolynomialMember(member.degree, member.coefficient));
        }

        public Polynomial(IEnumerable<(double degree, double coefficient)> members):this()
        {
            if (members != null)
            {
                foreach (var member in members)
                {
                    AddMember(member);
                }
            }
        }

        /// <summary>
        /// The amount of not null polynomial members in polynomial 
        /// </summary>
        public int Count
        {
            get
            {
                int count = 0;
                foreach (var monom in polynomialMembers)
                {
                    if (monom!=null)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// The biggest degree of polynomial member in polynomial
        /// </summary>
        public double Degree
        {
            get
            {
                double degree = double.NegativeInfinity;
                foreach (var monom in polynomialMembers)
                {
                    if (monom != null && monom.Degree > degree)
                    {
                        degree = monom.Degree;
                    }
                }
                return degree;
            }
        }

        /// <summary>
        /// Adds new unique member to polynomial 
        /// </summary>
        /// <param name="member">The member to be added</param>
        /// <exception cref="PolynomialArgumentException">Throws when member to add with such degree already exist in polynomial</exception>
        /// <exception cref="PolynomialArgumentNullException">Throws when trying to member to add is null</exception>
        public void AddMember(PolynomialMember member)
        {
            if (member == null)
            {
                throw new PolynomialArgumentNullException();
            }
            foreach (var monom in polynomialMembers)
            {
                if (monom.Degree == member.Degree)
                {
                    throw new PolynomialArgumentException();
                }
            }
            if (member.Coefficient == 0)
            {
                throw new PolynomialArgumentException();
            }

            polynomialMembers.Add(member);

            
        }

        /// <summary>
        /// Adds new unique member to polynomial from tuple
        /// </summary>
        /// <param name="member">The member to be added</param>
        /// <exception cref="PolynomialArgumentException">Throws when member to add with such degree already exist in polynomial</exception>
        public void AddMember((double degree, double coefficient) member)
        {
            foreach (var monom in polynomialMembers)
            {
                if (monom.Degree == member.degree)
                {
                    throw new PolynomialArgumentException();
                }
            }
            if (member.coefficient == 0)
                throw new PolynomialArgumentException();
            polynomialMembers.Add(new PolynomialMember(member.degree,member.coefficient));

        }

        /// <summary>
        /// Removes member of specified degree
        /// </summary>
        /// <param name="degree">The degree of member to be deleted</param>
        /// <returns>True if member has been deleted</returns>
        public bool RemoveMember(double degree)
        {
            foreach (var monom in polynomialMembers)
            {
                if (monom != null && monom.Degree == degree)
                {
                    polynomialMembers.Remove(monom);
                    
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Searches the polynomial for a method of specified degree
        /// </summary>
        /// <param name="degree">Degree of member</param>
        /// <returns>True if polynomial contains member</returns>
        public bool ContainsMember(double degree)
        {
            foreach (var monom in polynomialMembers)
            {
                if (monom != null && monom.Degree == degree)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Finds member of specified degree
        /// </summary>
        /// <param name="degree">Degree of member</param>
        /// <returns>Returns the found member or null</returns>
        public PolynomialMember Find(double degree)
        {
            foreach (var monom in polynomialMembers)
            {
                if (monom != null && monom.Degree == degree)
                {
                    return monom;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets and sets the coefficient of member with provided degree
        /// If there is no null member for searched degree - return 0 for get and add new member for set
        /// </summary>
        /// <param name="degree">The degree of searched member</param>
        /// <returns>Coefficient of found member</returns>
        public double this[double degree]
        {
            get
            {
                foreach (var monom in polynomialMembers)
                {
                    if (monom != null && monom.Degree == degree)
                    {
                        return monom.Coefficient;
                    }
                }
                return 0;
            }
            set 
            {
                bool _flag = false;
                foreach (var monom in polynomialMembers)
                {
                    if (monom != null && monom.Degree == degree)
                    {
                        monom.Coefficient = value;
                        _flag = true;
                    }
                }
                if (value != 0 && _flag==false)
                {
                    AddMember(new PolynomialMember(degree, value));
                }
                if (value == 0 && _flag == true) RemoveMember(degree);

            }
        }

        /// <summary>
        /// Convert polynomial to array of included polynomial members 
        /// </summary>
        /// <returns>Array with not null polynomial members</returns>
        public PolynomialMember[] ToArray()
        {
            int i = 0;
            PolynomialMember[] array = new PolynomialMember[polynomialMembers.Count];
            foreach (var item in polynomialMembers)
            {
                array[i] = item;
                i++;
            }
            return array;
        }

        /// <summary>
        /// Adds two polynomials
        /// </summary>
        /// <param name="a">The first polynomial</param>
        /// <param name="b">The second polynomial</param>
        /// <returns>New polynomial after adding</returns>
        /// <exception cref="PolynomialArgumentNullException">Throws if either of provided polynomials is null</exception>
        public static Polynomial operator +(Polynomial a, Polynomial b)
        {

            if (a == null || b == null)
            {
                throw new PolynomialArgumentNullException();
            }
            Polynomial result = new Polynomial(a.ToArray());

            foreach (var item in b.ToArray())
            {
                if (!result.ContainsMember(item.Degree) && item.Coefficient != 0)
                {
                    result.AddMember(item);
                }
                else
                {
                    result[item.Degree] += item.Coefficient;
                }
            }
            foreach (var item in result.ToArray())
            {
                if (item.Coefficient == 0)
                {
                    result.RemoveMember(item.Degree);
                }
            }

            return result;
        }

        /// <summary>
        /// Subtracts two polynomials
        /// </summary>
        /// <param name="a">The first polynomial</param>
        /// <param name="b">The second polynomial</param>
        /// <returns>Returns new polynomial after subtraction</returns>
        /// <exception cref="PolynomialArgumentNullException">Throws if either of provided polynomials is null</exception>
        public static Polynomial operator -(Polynomial a, Polynomial b)
        {
            Polynomial result = new Polynomial(a.ToArray());

            if (a == null || b == null)
            {
                throw new PolynomialArgumentNullException();
            }

            foreach (var item in b.ToArray())
            {

                result[item.Degree] -= item.Coefficient;
            }
            foreach (var item in result.ToArray())
            {
                if (item.Coefficient ==0)
                {
                    result.RemoveMember(item.Degree);
                }
            }

            return result;
        }

        /// <summary>
        /// Multiplies two polynomials
        /// </summary>
        /// <param name="a">The first polynomial</param>
        /// <param name="b">The second polynomial</param>
        /// <returns>Returns new polynomial after multiplication</returns>
        /// <exception cref="PolynomialArgumentNullException">Throws if either of provided polynomials is null</exception>
        public static Polynomial operator *(Polynomial a, Polynomial b)
        {
            Polynomial result = new Polynomial();

            if (a == null || b == null)
            {
                throw new PolynomialArgumentNullException();
            }

            foreach (var itemB in b.ToArray())
            {
                foreach (var itemA in a.ToArray())
                {
                    if (!result.ContainsMember(itemA.Degree + itemB.Degree) && itemA.Coefficient * itemB.Coefficient!=0)
                    {
                        result.AddMember(new PolynomialMember(itemA.Degree + itemB.Degree, itemA.Coefficient * itemB.Coefficient));

                    }
                    else
                    {
                        result[itemA.Degree + itemB.Degree] += itemA.Coefficient * itemB.Coefficient;
                    }


                }
            }


            return result;
        }

        /// <summary>
        /// Adds polynomial to polynomial
        /// </summary>
        /// <param name="polynomial">The polynomial to add</param>
        /// <returns>Returns new polynomial after adding</returns>
        /// <exception cref="PolynomialArgumentNullException">Throws if provided polynomial is null</exception>
        public Polynomial Add(Polynomial polynomial)
        {
            if (polynomial == null)
            {
                throw new PolynomialArgumentNullException();
            }
            return this + polynomial;
        }

        /// <summary>
        /// Subtracts polynomial from polynomial
        /// </summary>
        /// <param name="polynomial">The polynomial to subtract</param>
        /// <returns>Returns new polynomial after subtraction</returns>
        /// <exception cref="PolynomialArgumentNullException">Throws if provided polynomial is null</exception>
        public Polynomial Subtraction(Polynomial polynomial)
        {
            if (polynomial == null)
            {
                throw new PolynomialArgumentNullException();
            }
            return this - polynomial;
        }

        /// <summary>
        /// Multiplies polynomial with polynomial
        /// </summary>
        /// <param name="polynomial">The polynomial for multiplication </param>
        /// <returns>Returns new polynomial after multiplication</returns>
        /// <exception cref="PolynomialArgumentNullException">Throws if provided polynomial is null</exception>
        public Polynomial Multiply(Polynomial polynomial)
        {
            if (polynomial == null)
            {
                throw new PolynomialArgumentNullException();
            }
            return this * polynomial;
        }

        /// <summary>
        /// Adds polynomial and tuple
        /// </summary>
        /// <param name="a">The polynomial</param>
        /// <param name="b">The tuple</param>
        /// <returns>Returns new polynomial after adding</returns>
        public static Polynomial operator +(Polynomial a, (double degree, double coefficient) b)
        {

            if (a == null)
            {
                throw new PolynomialArgumentNullException();
            }
            Polynomial polynomial = new Polynomial(a.ToArray());

            if (!polynomial.ContainsMember(b.degree) && b.coefficient !=0)
            {
                polynomial.AddMember(new PolynomialMember(b.degree,b.coefficient));
            }
            else
            {
                polynomial[b.degree] += b.coefficient;
            }
            foreach (var item in polynomial.ToArray())
            {
                if (item.Coefficient == 0)
                {
                    polynomial.RemoveMember(item.Degree);
                }
            }
            return polynomial;
        }

        /// <summary>
        /// Subtract polynomial and tuple
        /// </summary>
        /// <param name="a">The polynomial</param>
        /// <param name="b">The tuple</param>
        /// <returns>Returns new polynomial after subtraction</returns>
        public static Polynomial operator -(Polynomial a, (double degree, double coefficient) b)
        {

            if (a == null)
            {
                throw new PolynomialArgumentNullException();
            }
            Polynomial polynomial = new Polynomial(a.ToArray());

            if (!polynomial.ContainsMember(b.degree)&& b.coefficient!=0)
            {
                polynomial.AddMember(new PolynomialMember(b.degree, -b.coefficient));
            }
            else
            {
                polynomial[b.degree] -= b.coefficient;
            }
            foreach (var item in polynomial.ToArray())
            {
                if (item.Coefficient == 0)
                {
                    polynomial.RemoveMember(item.Degree);
                }
            }
            return polynomial;
        }

        /// <summary>
        /// Multiplies polynomial and tuple
        /// </summary>
        /// <param name="a">The polynomial</param>
        /// <param name="b">The tuple</param>
        /// <returns>Returns new polynomial after multiplication</returns>
        public static Polynomial operator *(Polynomial a, (double degree, double coefficient) b)
        {

            if (a == null)
            {
                throw new PolynomialArgumentNullException();
            }
            Polynomial result = new Polynomial();

            foreach (var itemA in a.ToArray())
            {
                if (!result.ContainsMember(itemA.Degree + b.degree) && itemA.Coefficient * b.coefficient != 0)
                {
                    result.AddMember(new PolynomialMember(itemA.Degree + b.degree, itemA.Coefficient * b.coefficient));

                }
                else
                {
                    result[itemA.Degree + b.degree] += itemA.Coefficient * b.coefficient;
                }


            }
            


            return result;

        }

        /// <summary>
        /// Adds tuple to polynomial
        /// </summary>
        /// <param name="member">The tuple to add</param>
        /// <returns>Returns new polynomial after adding</returns>
        public Polynomial Add((double degree, double coefficient) member)
        {
            return this + member;
        }

        /// <summary>
        /// Subtracts tuple from polynomial
        /// </summary>
        /// <param name="member">The tuple to subtract</param>
        /// <returns>Returns new polynomial after subtraction</returns>
        public Polynomial Subtraction((double degree, double coefficient) member)
        {
            return this - member;
        }

        /// <summary>
        /// Multiplies tuple with polynomial
        /// </summary>
        /// <param name="member">The tuple for multiplication </param>
        /// <returns>Returns new polynomial after multiplication</returns>
        public Polynomial Multiply((double degree, double coefficient) member)
        {
            return this * member;
        }
    }
}
