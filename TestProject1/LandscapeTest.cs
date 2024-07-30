using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Landscape;
using Building;
using Effect;
using Game;

namespace TestProject1
{
    [TestClass]
    public class LandscapeTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var landscape = new Landscape.Landscape();
            var place = new Matrix.Matrix<Cell>();
            var array = new int[landscape.Width * landscape.Height];
            for (int i = 0; i < landscape.Height; i++)
            {
                for (int j = 0; j < landscape.Width; j++)
                {
                    landscape.Place[i, j] = new Cell();
                    place[i, j] = new Cell();
                }
            }
            Assert.IsTrue(landscape.Place == place);
        }

        public Landscape.Landscape Loading( ref Matrix.Matrix<Cell> place, ref Castle castle, ref List<Lair> lairs, String name)
        {
            //  Matrix.Matrix<Cell> place;
           //   Castle castle;
           //   var lairs = new List<Lair>();
            string path = name;
            using (StreamReader reader = new(path))
            {
                string? line;
                line = reader.ReadLine();
                string[] line2 = line.Split(' ');
                Matrix.Matrix<Cell> place1 = new Matrix.Matrix<Cell>(Int32.Parse(line2[0]), Int32.Parse(line2[1]));
                place = place1;
                line = reader.ReadLine();
                line2 = line.Split(' ');
                for (int i = 0; i < place.Height; i++)
                {
                    for (int j = 0; j < place.Width; j++) place[i, j] = new Cell((TypeCell)Int32.Parse(line2[j + i * place.Width]));
                }
                line = reader.ReadLine();
                line2 = line.Split(' ');
                castle = new Castle(line2[0], Int32.Parse(line2[1]), Int32.Parse(line2[2]), Int32.Parse(line2[3]), Int32.Parse(line2[4]));
                line = reader.ReadLine();
                line2 = line.Split(' ');
                int count = Int32.Parse(line2[0]);
                for (int i = 0; i < count; i++)
                {
                    line = reader.ReadLine();
                    line2 = line.Split(' ');
                    lairs.Add(new Lair(Int32.Parse(line2[0]), Int32.Parse(line2[1]), Int32.Parse(line2[2])));

                }
            }
            var landscape = new Landscape.Landscape(place, castle, lairs);
            return landscape;
        }

        [TestMethod]
        [DataRow("C:\\Users\\user\\Documents\\Tower_Defence#\\Game\\TestProject1\\TextFile1.txt")]
        [DataRow("C:\\Users\\user\\Documents\\Tower_Defence#\\Game\\TestProject1\\TextFile2.txt")]
        public void ConstructorWithInitializationAsync(String name)
        {
            Matrix.Matrix<Cell> place = new Matrix.Matrix<Cell>();
            Castle castle = new();
            var lairs = new List<Lair>();
            var landscape = Loading(ref place, ref castle, ref lairs,name);
            Assert.IsTrue(landscape.Place == place);
            Assert.IsTrue(landscape.Castle == castle);
            Assert.IsTrue(landscape.Lairs == lairs);
        }

        [TestMethod]
        [DataRow("C:\\Users\\user\\Documents\\Tower_Defence#\\Game\\TestProject1\\TextFile1.txt")]
        public void WayTest(String name)
        {
            Matrix.Matrix<Cell> place = new Matrix.Matrix<Cell>();
            Castle castle = new();
            var lairs = new List<Lair>();
            var landscape = Loading(ref place, ref castle, ref lairs, name);
            landscape.CheckCorrect();
        }

        [TestMethod]
        [DataRow("C:\\Users\\user\\Documents\\Tower_Defence#\\Game\\TestProject1\\TextFile2.txt")]
        [DataRow("C:\\Users\\user\\Documents\\Tower_Defence#\\Game\\TestProject1\\TextFile3.txt")]
        public void WayTestException(String name)
        {
            Matrix.Matrix<Cell> place = new Matrix.Matrix<Cell>();
            Castle castle = new();
            var lairs = new List<Lair>();
            var landscape = Loading(ref place, ref castle, ref lairs, name);
            Assert.ThrowsException<ArgumentException>(() => landscape.CheckCorrect());
        }

        [TestMethod]
        [DataRow("C:\\Users\\user\\Documents\\Tower_Defence#\\Game\\TestProject1\\TextFile4.txt")]
        public void WayTestException2(String name)
        {
            Matrix.Matrix<Cell> place = new Matrix.Matrix<Cell>();
            Castle castle = new();
            var lairs = new List<Lair>();
            var landscape = Loading(ref place, ref castle, ref lairs, name);
            Assert.ThrowsException<NullReferenceException>(() => landscape.CheckCorrect());
        }

        [TestMethod]
        [DataRow("C:\\Users\\user\\Documents\\Tower_Defence#\\Game\\TestProject1\\TextFile1.txt")]
        public void AddTowerTest(String name)
        {
            Matrix.Matrix<Cell> place = new Matrix.Matrix<Cell>();
            Castle castle = new();
            var lairs = new List<Lair>();
            var landscape = Loading(ref place, ref castle, ref lairs, name);
            var effect1 = new Effect.Effect();
            var tower1 = new Magic_Tower(effect1);
            var effect2 = new Effect.Effect();
            var tower2 = new Magic_Trap(effect2, 100, 3, 2, 50, 0, 3);
            landscape.AddTower(tower1);
            landscape.AddTower(tower2);
        }
    }
}
