using Building;

namespace UnitTest
{
    [TestFixture]
    internal class LairTest
    {
        [Test]
        public void DefaultConstructor()
        {
            var lair = new Lair();
            Assert.That(lair.TimeExit, Is.EqualTo(10));
        }

        [Test]
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

        [Test]
        public void SetterTimeTest()
        {
            var lair = new Lair();
            lair.TimeExit = 18;
            Assert.That(lair.TimeExit, Is.EqualTo(18));
        }

        [Test]
        public void CoordinatesTest()
        {
            var lair = new Lair();
            lair.X = 0;
            lair.Y = 75;
            Assert.That(lair.X, Is.EqualTo(0));
            Assert.That(lair.Y, Is.EqualTo(75));
        }
    }
}