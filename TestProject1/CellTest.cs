using Microsoft.VisualStudio.TestTools.UnitTesting;
using Building;
using Landscape;
using Effect;

namespace TestProject1
{
    [TestClass]
    public class CellTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var cell = new Cell();

            Assert.AreEqual(cell.CellType, TypeCell.Forest);
            Assert.AreEqual(cell.Building, null);

        }

        [TestMethod]
        public void InitializateConstructor()
        {
            var lair = new Lair();
            var cell = new Cell(TypeCell.Field, lair);
            Assert.AreEqual(cell.CellType, TypeCell.Field);
            Assert.AreEqual(cell.Building, lair);

        }

        [TestMethod]
        public void CoparisonOperator()
        {
            var lair1 = new Lair();
            var lair3 = new Lair();
            var lair2 = new Castle();
            var lair4 = new Lair();
            var cell1 = new Cell(TypeCell.Field, lair1);
            var cell2 = new Cell(TypeCell.Field, lair2);
            var cell3 = new Cell();
            var cell5 = new Cell();
            var cell4 = new Cell(TypeCell.Road, lair4);
            var cell6 = new Cell(TypeCell.Field, lair3);
            Assert.IsTrue(cell1 != cell2);
            Assert.IsTrue(cell1 != cell3);
            Assert.IsTrue(cell1 != cell4);
            Assert.IsTrue(cell3 == cell5);
            Assert.IsTrue(cell1 == cell6);
            Assert.IsTrue(cell3 != cell6);
            Assert.IsTrue(cell6 != cell3);

        }

        [TestMethod]
        public void CompareTest()
        {
            var cell = new Cell();
            var cell1 = new Cell();
          //  Assert.AreEqual(cell.Equals(cell1), 0);
        }

        [TestMethod]
        public void Setters()
        {
            var lair = new Lair();
            var cell = new Cell();
            cell.CellType = TypeCell.Field;
            cell.Building = lair;
            Assert.AreEqual(cell.CellType, TypeCell.Field);
            Assert.AreEqual(cell.Building, lair);
        }
    }
}
