using Building;
using Effect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class LairTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var lair = new Lair();
            Assert.AreEqual(lair.TimeExit, 10);
        }

        [TestMethod]
        public void ExitEnemy()
        {
            Enemy.Enemy enemy1 = new Enemy.Enemy();
            Enemy.EnemyAttack enemy2 = new Enemy.EnemyAttack(15, "Slon", 80, 20, 2, 0, 0);
            Enemy.Enemy enemy3 = new Enemy.Enemy("Split", 86, 13, 2, 0, 0);
            Enemy.Enemy[] lol = new Enemy.Enemy[] { enemy1, enemy2, enemy3 };
            List<Enemy.Enemy> enemies = new List<Enemy.Enemy>(lol);
            var lair = new Lair(enemies);
            Enemy.Enemy enemy4 = lair.release_enemy();
            Assert.IsTrue(enemy1 == enemy4);
        }

        [TestMethod]
        public void SetterTimeTest()
        {
            var lair = new Lair();
            lair.TimeExit = 18;
            Assert.AreEqual(lair.TimeExit, 18);
        }

        [TestMethod]
        public void CoordinatesTest()
        {
            var lair = new Lair();
            lair.X = 0;
            lair.Y = 75;
            Assert.AreEqual(lair.X, 0);
            Assert.AreEqual(lair.Y, 75);
        }

        [TestMethod]
        public void ReleaseEnemy()
        {
            var lair = new Lair();
            Assert.ThrowsException<Exception>(() => lair.release_enemy());
        }
    }
}
