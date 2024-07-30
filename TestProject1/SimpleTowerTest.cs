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
    public class SimpleTowerTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var tower = new Simple_Tower();
            Assert.AreEqual(tower.Cost, 100);
            Assert.AreEqual(tower.Radius, 3);
            Assert.AreEqual(tower.Harm, 2);
            Assert.AreEqual(tower.Level, 1);
            Assert.AreEqual(tower.Endurance, 50);
            Assert.AreEqual(tower.SpeedShooting, 3);
            Assert.AreEqual(tower.Strategy_, Strategy.Closet_to_castle);
        }

        [TestMethod]
        [DataRow(4, 2)]
        [DataRow(0, 0)]
        [DataRow(-1, 23)]
        [DataRow(-6, -8)]
        public void Setters(int ss, int s)
        {
            var tower = new Simple_Tower();
            if (ss > 0)
            {
                tower.SpeedShooting = ss;
                Assert.AreEqual(tower.SpeedShooting, ss);
            }
            else Assert.ThrowsException<ArgumentOutOfRangeException>(() => tower.SpeedShooting = ss);
            if (s >= 0 && s < 5)
            {
                tower.Strategy_ = (Strategy)s;
                Assert.AreEqual(tower.Strategy_, (Strategy)s);
            }
            else Assert.ThrowsException<ArgumentOutOfRangeException>(() => tower.Strategy_ = (Strategy)s);
        }

    }
}
