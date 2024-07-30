using Building;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class CastleTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var castle = new Castle();
            Assert.AreEqual(castle.Name, "White house");
            Assert.AreEqual(castle.Endurance, 1000);
            Assert.AreEqual(castle.EnduranceMax, 1000);
            Assert.AreEqual(castle.Gold, 10000);

        }

        [TestMethod]
        [DataRow(100, 100)]
        [DataRow(13, 6)]
        [DataRow(20, 80)]
        public void MakeHarmTest(int _health, int _endurance)
        {
            var castle = new Castle
            {
                Endurance = _endurance
            };
            var enemy = new Enemy.Enemy
            {
                Health = _health
            };
            if (_health >= _endurance)
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => castle.MakeHarm(enemy));
            }
            else
            {
                castle.MakeHarm(enemy);
                Assert.AreEqual(castle.Endurance, _endurance - _health);
            }
        }

        [TestMethod]
        public void SetterGold()
        {
            var castle = new Castle
            {
                Gold = 15
            };
            Assert.AreEqual(castle.Gold, 15);
        }
    }
}
