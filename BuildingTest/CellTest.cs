using Building;
using Landscape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingTest
{
    [TestFixture]
    internal class CellTest
    {
        [Test]
        public void DefaultConstructor()
        {
            var cell = new Cell();
            Assert.Multiple(() =>
            {
                Assert.That(cell.CellType, Is.EqualTo(TypeCell.Forest));
                Assert.That(cell.Building, Is.EqualTo(null));
            });
        }

        [Test]
        public void InitializateConstructor()
        {
            var lair = new Lair();
            var cell = new Cell(TypeCell.Field, lair);
            Assert.Multiple(() =>
            {
                Assert.That(cell.CellType, Is.EqualTo(TypeCell.Field));
                Assert.That(cell.Building, Is.EqualTo(lair));
            });
        }

        [Test]
        public void CoparisonOperator()
        {
            var lair1 = new Lair();
            var lair2 = new Lair();
            var cell1 = new Cell(TypeCell.Field, lair1);
            var cell2 = new Cell(TypeCell.Field, lair2);
            var cell3 = new Cell(TypeCell.Field, lair1);
            var cell4 = new Cell(TypeCell.Road, lair1);
          //  Assert.That(cell1 != cell2, Is.True);
            Assert.That(cell1 == cell3, Is.True);
            Assert.That(cell1 != cell4, Is.True);

        }
    }
}
