using Building;
using Effect;
using Enemy;

namespace BuildingTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DefaultConstructor()
        {
            var tower = new Tower();
            Assert.That(tower.Cost, Is.EqualTo(100));
            Assert.That(tower.Radius, Is.EqualTo(3));
            Assert.That(tower.Harm, Is.EqualTo(2));
            Assert.That(tower.Level, Is.EqualTo(1));
            Assert.That(tower.Endurance, Is.EqualTo(50));
        }

        [Test]
        public void LevelUptest()
        {
            var tower = new Tower();
            tower.Radius = 10;
            tower.LevelUp();
            Assert.That(tower.Radius, Is.EqualTo(11));
            Assert.That(tower.Harm, Is.EqualTo(12));
            Assert.That(tower.Level, Is.EqualTo(2));
        }

        [TestCase(100, 100, 5)]
        [TestCase(12, 35, 78)]
        [TestCase(100, 9, 7)]
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
                Assert.That(enemy.Health, Is.EqualTo(health - (harm + (int)(0.01 * force))));
            }
            else Assert.Throws<ArgumentOutOfRangeException>(() => tower.SpecialAction(enemy));


        }

        [TestCase(8, 9, 10, 19)]
        [TestCase(0, 0, 0, 0)]
        [TestCase(-1,-1,-1, -1)]
        public void SettersTest(int l, int c, int e, int h)
        {
            var tower = new Tower();
            if (l > 0)
            {
                tower.Level = l;
                Assert.That(tower.Level, Is.EqualTo(l));
            }
            else Assert.Throws<ArgumentOutOfRangeException>(() => tower.Level = l);
            if (c >= 0)
            {
                tower.Cost = c;
                Assert.That(tower.Cost, Is.EqualTo(c));
            }
            else Assert.Throws<ArgumentOutOfRangeException>(() => tower.Cost = c);
            if (e > 0)
            {
                tower.Endurance = e;
                Assert.That(tower.Endurance, Is.EqualTo(e));
            }
            else Assert.Throws<ArgumentOutOfRangeException>(() => tower.Endurance = e);
            if (h > 0)
            {
                tower.Harm = h;
                Assert.That(tower.Harm, Is.EqualTo(h));
            }
            else Assert.Throws<ArgumentOutOfRangeException>(() => tower.Harm = h);
        }
    }
}