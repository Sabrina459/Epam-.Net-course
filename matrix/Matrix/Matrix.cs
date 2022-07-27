using System;

namespace MatrixLibrary
{

    [Serializable]
    public class MatrixException : Exception
    {

        public  MatrixException(string message): base(message)
        {
        }
        public MatrixException(Exception ex) : base(ex.Message)
        {
        }
    }


    public class Matrix : ICloneable
    {
        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows
        {
            get => Array.GetLength(0);
        }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Columns
        {
            get => Array.GetLength(1);
        }
        
        /// <summary>
        /// Gets an array of floating-point values that represents the elements of this Matrix.
        /// </summary>
        public double[,] Array
        {
            get; 
            set;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Matrix(int rows, int columns)
        {
            if (rows < 1 || columns <1)
            {
                throw new ArgumentOutOfRangeException();
            }
            Array = new double[rows, columns];
            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    Array[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class with the specified elements.
        /// </summary>
        /// <param name="array">An array of floating-point values that represents the elements of this Matrix.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Matrix(double[,] array)
        {
            if (array==null)
            {
                throw new ArgumentNullException();
            }
            Array = new double[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    Array[i, j] = array[i,j];
                }
            }
        }

        /// <summary>
        /// Allows instances of a Matrix to be indexed just like arrays.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <exception cref="ArgumentException"></exception>
        public double this[int row, int column]
        {
            get
            {
                if (row >= Rows || column >= Columns || row < 0 || column < 0)
                    throw new ArgumentException();
                return Array[row, column];
            }
            set {
                if (row >= Rows || column >= Columns || row < 0 || column < 0)
                    throw new ArgumentException();
                Array[row, column] = value;
            }
            
        }

        /// <summary>
        /// Creates a deep copy of this Matrix.
        /// </summary>
        /// <returns>A deep copy of the current object.</returns>
        public object Clone()
        {

            Matrix newMatrix = new Matrix(new double[Rows,Columns]);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    newMatrix[i, j] = this[i, j];
                }
            }
            return newMatrix;
        }

        /// <summary>
        /// Adds two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is sum of two matrices.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null || matrix2 == null)
            {
                throw new ArgumentNullException();
            }
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns!= matrix2.Columns)
            {
                throw new MatrixException("Matrixs are not the same size");
            }
            Matrix newMatrix = new Matrix(matrix1.Rows, matrix1.Columns);

            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0; j < matrix1.Columns; j++)
                {
                    newMatrix[i, j] = matrix1[i, j]+ matrix2[i, j];
                }
            }
            return newMatrix;
        }

        /// <summary>
        /// Subtracts two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is subtraction of two matrices</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null || matrix2 == null)
            {
                throw new ArgumentNullException();
            }
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
            {
                throw new MatrixException("Matrixs are not the same size");
            }
            Matrix newMatrix = new Matrix(matrix1.Rows, matrix1.Columns);

            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0; j < matrix1.Columns; j++)
                {
                    newMatrix[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }
            return newMatrix;
        }

        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is multiplication of two matrices.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null || matrix2 == null)
            {
                throw new ArgumentNullException();
            }
            if (matrix1.Columns != matrix2.Rows)
            {
                throw new MatrixException("Matrixs are not of the apropriate size");
            }
            Matrix newMatrix = new Matrix(matrix1.Rows, matrix2.Columns);
            double temp;
            for (int mi = 0; mi < matrix1.Rows; mi++)
            {
                for (int mj = 0; mj < matrix2.Columns; mj++)
                {
                    temp = 0;
                    for (int j = 0; j < matrix1.Columns; j++)
                    {
                        temp += matrix1[mi, j] * matrix2[j, mj];
                    }
                    newMatrix[mi, mj] = temp;
                 
                }
            }
            return newMatrix;
        }

        /// <summary>
        /// Adds <see cref="Matrix"/> to the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for adding.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Add(Matrix matrix)
        {
            return this + matrix;
        }

        /// <summary>
        /// Subtracts <see cref="Matrix"/> from the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for subtracting.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Subtract(Matrix matrix)
        {
            return this - matrix;
        }

        /// <summary>
        /// Multiplies <see cref="Matrix"/> on the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for multiplying.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Multiply(Matrix matrix)
        {
            return this * matrix;
        }

        /// <summary>
        /// Tests if <see cref="Matrix"/> is identical to this Matrix.
        /// </summary>
        /// <param name="obj">Object to compare with. (Can be null)</param>
        /// <returns>True if matrices are equal, false if are not equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType()!=this.GetType())
            {
                return false;
            }

            Matrix matrix = (Matrix)obj;
            if (matrix.Rows != Rows || matrix.Columns != Columns)
            {
                return false;
            }
            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Columns; j++)
                    if (matrix[i, j] != this[i, j])
                        return false;
            return true;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
