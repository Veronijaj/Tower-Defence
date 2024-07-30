using Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject2
{
    [TestFixture]
    public class MatrixTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DefaultConstructor()
        {
            int size = 30;
            Matrix<int> matrix = new Matrix<int>();
            Assert.That(matrix.Height, Is.EqualTo(size));
            Assert.That(matrix.Width, Is.EqualTo(size));
        }

        [TestCase(8, 9)]
        public void ConstructorWithInitialization(int height, int width)
        {
            var matrix = new Matrix<int>(height, width);
            Assert.That(matrix.Height, Is.EqualTo(height));
            Assert.That(matrix.Width, Is.EqualTo(width));
            Matrix<int> matrix2;
            Assert.Throws<ArgumentOutOfRangeException>(() => matrix2 = new Matrix<int>(-9, width));
            Assert.Throws<ArgumentOutOfRangeException>(() => matrix2 = new Matrix<int>(height, -9));
        }

        [Test]
        public void CopyConstructor()
        {
            var matrix1 = new Matrix<int>(5, 7);
            var matrix2 = new Matrix<int>(matrix1);
            Assert.IsTrue(matrix1 == matrix2);
        }

        [TestCase(15, 17)]
        [TestCase(40, 45)]
        public void ResizeTest(int height, int width)
        {
            int el = 5;
            Matrix<int> matrix = new Matrix<int>();
            matrix.Resize(height, width);
        }

        [TestCase(-9, 10, '3')]
        [TestCase(35, -12, '7')]
        public void ResizeException(int height, int width, char sign)
        {
            Matrix<char> matrix = new Matrix<char>();
            Assert.Throws<ArgumentOutOfRangeException>(() => matrix.Resize(height, width));
        }

        [Test]
        public void OperatorEquality()
        {
            var matrix1 = new Matrix<int>(2, 3);
            var matrix2 = new Matrix<int>(2, 3);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix1[i, j] = 2;
                    matrix2[i, j] = 2;
                }
            }
            Assert.That(matrix1 == matrix2);

        }

        [TestCase(5, 5, 5, 7, 6, 6)]
        [TestCase(5, 5, 7, 5, 6, 6)]
        [TestCase(5, 5, 5, 5, 6, 8)]
        public void OperatorNoEquality(int height1, int width1, int height2, int width2, int el1, int el2)
        {
            var matrix1 = new Matrix<int>(height1, width1);
            var matrix2 = new Matrix<int>(height2, width2);
            int minh = Math.Min(height1, height2);
            int minw = Math.Min(width1, width2);
            for (int i = 0; i < minh; i++)
            {
                for (int j = 0; j < minw; j++)
                {
                    matrix1[i, j] = el1;
                    matrix2[i, j] = el2;
                }
            }
            Assert.IsTrue(matrix1 != matrix2);

        }

        [TestCase(3, -5)]
        [TestCase(-5, 3)]
        [TestCase(8, 3)]
        [TestCase(5, 9)]
        public void OperatorIndexGet(int i, int j)
        {
            var matrix1 = new Matrix<int>(6, 7);
            int k;
            Assert.Throws<ArgumentOutOfRangeException>(() => k = matrix1[i, j]);
            Assert.Throws<ArgumentOutOfRangeException>(() => k = matrix1[i, j]);
        }

        [TestCase(3, -5)]
        [TestCase(-5, 3)]
        [TestCase(8, 3)]
        [TestCase(5, 9)]
        public void OperatorIndexSet(int i, int j)
        {
            var matrix1 = new Matrix<int>(6, 7);
            int k = 5;
            Assert.Throws<ArgumentOutOfRangeException>(() => matrix1[i, j] = k);
            Assert.Throws<ArgumentOutOfRangeException>(() => matrix1[i, j] = k);
        }

        [Test]
        public void AddDeleteColumn()
        {
            var matrix1 = new Matrix<int>(5, 6);
            int[] array = new int[5] { 5, 5, 5, 5, 5 };
            matrix1.add_column(array);
            var matrix2 = new Matrix<int>(5, 7);
            for (int i = 0; i < matrix2.Height; i++) { matrix2[i, 6] = 5; }
            Assert.IsTrue(matrix1 == matrix2);
            matrix1.delete_column();
            var matrix3 = new Matrix<int>(5, 6);
            Assert.IsTrue(matrix1 == matrix3);
        }

        [Test]
        public void AddRow()
        {
            var matrix1 = new Matrix<int>(5, 6);
            int[] array = new int[6] { 5, 5, 5, 5, 5, 5 };
            matrix1.add_row(array);
            var matrix2 = new Matrix<int>(6, 6);
            for (int i = 0; i < matrix2.Width; i++) { matrix2[5, i] = 5; }
            int summ = 0;
            foreach (int value in matrix1) summ+=value;
            Assert.IsTrue(matrix1 == matrix2);
            matrix1.delete_row();
            var matrix3 = new Matrix<int>(5, 6);
            Assert.IsTrue(matrix1 == matrix3);
            Assert.That(summ, Is.EqualTo(5 * 6));
        }
    }
}



