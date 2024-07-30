using Microsoft.VisualStudio.TestTools.UnitTesting;
using Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class TowerTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var tower = new Tower();
            Assert.AreEqual(tower.Cost, 100);
            Assert.AreEqual(tower.Radius, 3);
            Assert.AreEqual(tower.Harm, 2);
            Assert.AreEqual(tower.Level, 1);
            Assert.AreEqual(tower.Endurance, 50);
        }

        [TestMethod]
        public void LevelUptest()
        {
            var tower = new Tower();
            tower.Radius = 10;
            tower.LevelUp();
            Assert.AreEqual(tower.Radius, 11);
            Assert.AreEqual(tower.Harm, 12);
            Assert.AreEqual(tower.Level, 2);
        }

        [TestMethod]
        [DataRow(100, 100, 5)]
        [DataRow(12, 35, 78)]
        [DataRow(100, 9, 7)]
        public void SpecialActionTest(int force, int harm, int health)
        {
            var tower = new Tower();
            tower.Harm = harm;
            var enemy = new Enemy.Enemy();
            enemy.Health = health;
            Effect.Effect effect = new Effect.Effect(force, 10, 2);
            enemy.add_effect(effect);
            if (harm + (int)(0.01 * force) < health)
            {
                tower.SpecialAction(enemy);
                Assert.AreEqual(enemy.Health, health - (harm + (int)(0.01 * force)));
            }
            else Assert.ThrowsException<ArgumentOutOfRangeException>(() => tower.SpecialAction(enemy));
        }

        [TestMethod]
        [DataRow(8, 9, 10, 19)]
        [DataRow(0, 0, 0, 0)]
        [DataRow(-1, -1, -1, -1)]
        public void SettersTest(int l, int c, int e, int h)
        {
            var tower = new Tower();
            if (l > 0)
            {
                tower.Level = l;
                Assert.AreEqual(tower.Level, l);
            }
            else Assert.ThrowsException<ArgumentOutOfRangeException>(() => tower.Level = l);
            if (c >= 0)
            {
                tower.Cost = c;
                Assert.AreEqual(tower.Cost, c);
            }
            else Assert.ThrowsException<ArgumentOutOfRangeException>(() => tower.Cost = c);
            if (e > 0)
            {
                tower.Endurance = e;
                Assert.AreEqual(tower.Endurance, e);
            }
            else Assert.ThrowsException<ArgumentOutOfRangeException>(() => tower.Endurance = e);
            if (h > 0)
            {
                tower.Harm = h;
                Assert.AreEqual(tower.Harm, h);
            }
            else Assert.ThrowsException<ArgumentOutOfRangeException>(() => tower.Harm = h);
        }
    }
}
