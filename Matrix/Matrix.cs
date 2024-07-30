using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    /// <summary>
    /// Template class of matrix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Matrix<T> 
    {
        int height;
        int width;
        T[,] data;

        /// <summary>
        /// default constructor
        /// </summary>
        public Matrix()
        {
            width = 30; height = 30;
            data = new T[30, 30];

        }
        /// <summary>
        /// constructor with initialization of size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <exception cref="ArgumentOutOfRangeException">Non-positive values of height and length</exception>
        public Matrix(int height, int width)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width), "Width cannot be less than zero");
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height), "Height cannot be less than zero");
            this.height = height;
            this.width = width;
            data = new T[height, width];
        }
        /// <summary>
        /// Change size of matrix
        /// </summary>
        /// <param name="h">new height</param>
        /// <param name="w">new width</param>
        /// <param name="elem">Default element for filling of empty cells </param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void Resize(int h, int w)
        {
            if (w <= 0) throw new ArgumentOutOfRangeException(nameof(w), "Width cannot be less than zero");
            if (h <= 0) throw new ArgumentOutOfRangeException(nameof(h), "Height cannot be less than zero");
            T[,] newdata = new T[h, w];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (i < height && j < width) newdata[i, j] = data[i, j];
                    else newdata[i, j] = default;
                }
            }
            data = newdata;
            this.Height = h;
            this.Width = w;
        }
        /// <summary>
        /// Сhecking for inequality
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool operator !=(Matrix<T> obj1, Matrix<T> obj2) { return !(obj1 == obj2); }
        /// <summary>
        /// Equality operator
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool operator ==(Matrix<T> obj1, Matrix<T> obj2)
        {
            if ((obj1.width == obj2.width) && (obj1.height == obj2.height))
            {
                for (int i = 0; i < obj1.height; i++)
                    for (int j = 0; j < obj1.width; j++)
                        if (!obj1[i, j].Equals(obj2[i, j])) return false;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Return by index
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T this[int i, int j]
        {
            get
            {
                if (i >= height || i < 0) throw new ArgumentOutOfRangeException(nameof(height), "Height is wrong");
                if (j >= width || j < 0) throw new ArgumentOutOfRangeException(nameof(width), "Width is wrong");
                return data[i, j];
            }
            set
            {
                if (i >= height || i < 0) throw new ArgumentOutOfRangeException(nameof(height), "Height is wrong");
                if (j >= width || j < 0) throw new ArgumentOutOfRangeException(nameof(width), "Width is wrong");
                data[i, j] = value;
            }
        }
        /// <summary>
        /// Getter of height
        /// </summary>
        /// <returns>height</returns>
        public int Height
        {
            get { return height; }
            set {
                if(value<=0) throw new ArgumentOutOfRangeException(nameof(value), "Value is wrong");
                height = value; 
            }
        }
        /// <summary>
        /// Getter of width
        /// </summary>
        /// <returns>width</returns>
        public int Width
        {
            get { return width; }
            set {
                if (value <= 0) throw new ArgumentOutOfRangeException("Value is wrong", nameof(value));
                width = value; 
            }
        }
        
        /// <summary>
        /// Addition column
        /// </summary>
        /// <param name="el"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void add_column(T[] el)
        {
            if (el.Length > height) throw new ArgumentOutOfRangeException("Number of elements is bigger than matrix' size", nameof(height));
            Resize(height, width + 1);
            for (int i = 0; i < el.Length; i++)
            {
                data[i, width-1] = el[i];
            }
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="el"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void add_row(T[] el)
        {
            if (el.Length > width) throw new ArgumentOutOfRangeException("Number of elements is bigger than matrix' size", nameof(width));
            Resize(height + 1, width);
            for (int i = 0; i < el.Length; i++) data[height-1, i] = el[i];
        }

        /// <summary>
        /// Delete column
        /// </summary>
        public void delete_column()
        {
            Resize(height, width - 1);
        }

        /// <summary>
        /// Delete row
        /// </summary>
        public void delete_row() { Resize(height - 1, width); }

        /// <summary>
        /// Iterator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j=0; j < width; j++) yield return data[i, j];
            }
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">Constructor that copies</param>
        public Matrix(Matrix<T> other)
        {
            height = other.Height;
            width = other.Width;
            data = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    data[i, j] = other.data[i, j];
                }
            }
        }
    }
}