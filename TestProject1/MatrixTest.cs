using Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            int size = 30;
            var matrix = new Matrix<int>();
            Assert.AreEqual(matrix.Height, size);
            Assert.AreEqual(matrix.Width, size);
        }

        [TestMethod]
        [DataRow(8, 9)]
        public void ConstructorWithInitialization(int height, int width)
        {
            var matrix = new Matrix<int>(height, width);
            Assert.AreEqual(matrix.Height, height);
            Assert.AreEqual(matrix.Width, width);
            Matrix<int> matrix2;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => matrix2 = new Matrix<int>(-9, width));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => matrix2 = new Matrix<int>(height, -9));
        }

        [TestMethod]
        public void CopyConstructor()
        {
            var matrix1 = new Matrix<int>(5, 7);
            var matrix2 = new Matrix<int>(matrix1);
            Assert.IsTrue(matrix1 == matrix2);
        }

        [TestMethod]
        [DataRow(15, 17)]
        [DataRow(40, 45)]
        public void ResizeTest(int height, int width)
        {
            var matrix = new Matrix<int>();
            matrix.Resize(height, width);
        }

        [TestMethod]
        [DataRow(-9, 10)]
        [DataRow(35, -12)]
        public void ResizeException(int height, int width)
        {
            var matrix = new Matrix<char>();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => matrix.Resize(height, width));
        }

        [TestMethod]
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
            Assert.IsTrue(matrix1 == matrix2);
        }

        [TestMethod]
        [DataRow(5, 5, 5, 7, 6, 6)]
        [DataRow(5, 5, 7, 5, 6, 6)]
        [DataRow(5, 5, 5, 5, 6, 8)]
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

        [TestMethod]
        [DataRow(3, -5)]
        [DataRow(-5, 3)]
        [DataRow(8, 3)]
        [DataRow(5, 9)]
        public void OperatorIndexGet(int i, int j)
        {
            var matrix1 = new Matrix<int>(6, 7);
            int k;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => k = matrix1[i, j]);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => k = matrix1[i, j]);
        }

        [TestMethod]
        [DataRow(3, -5)]
        [DataRow(-5, 3)]
        [DataRow(8, 3)]
        [DataRow(5, 9)]
        public void OperatorIndexSet(int i, int j)
        {
            var matrix1 = new Matrix<int>(6, 7);
            int k = 5;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => matrix1[i, j] = k);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => matrix1[i, j] = k);
        }

        [TestMethod]
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

        [TestMethod]
        public void AddRow()
        {
            var matrix1 = new Matrix<int>(5, 6);
            int[] array = new int[6] { 5, 5, 5, 5, 5, 5 };
            matrix1.add_row(array);
            var matrix2 = new Matrix<int>(6, 6);
            for (int i = 0; i < matrix2.Width; i++) { matrix2[5, i] = 5; }
            int summ = 0;
            foreach (int value in matrix1) summ += value;
            Assert.IsTrue(matrix1 == matrix2);
            matrix1.delete_row();
            var matrix3 = new Matrix<int>(5, 6);
            Assert.IsTrue(matrix1 == matrix3);
            Assert.AreEqual(summ, 5 * 6);
        }
    }
}