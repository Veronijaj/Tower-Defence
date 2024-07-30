using Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestFixture]
    internal class EnemyTest
    {
        [Test]
        public void DefaultConstructor()
        {
            Enemy.Enemy enemy1 = new Enemy.Enemy();
            Assert.That(enemy1.Name, Is.EqualTo("Villian"));
            Assert.That(enemy1.Health, Is.EqualTo(100));
            Assert.That(enemy1.Gold, Is.EqualTo(0));
            Assert.That(enemy1.Velocity, Is.EqualTo(1));
            Assert.That(enemy1.Coordinates.Item1, Is.EqualTo(0));
            Assert.That(enemy1.Coordinates.Item2, Is.EqualTo(0));
        }

        [Test]
        public void TestAdditionEffect()
        {
            var enemy = new Enemy.Enemy();
            Effect.Effect effect = new Effect.Effect();
            enemy.add_effect(effect);
            Assert.IsTrue(enemy.geteffect(0) == effect);
        }
        [Test]
        public void TestGetterVelocity()
        {
            var enemy = new Enemy.Enemy();
            Effect.Effect effect = new Effect.Effect(200, 10, 0);
            enemy.add_effect(effect);
            Assert.AreEqual(enemy.Velocity, 0);
        }

        [TestCase(20)]
        [TestCase(300)]
        public void TestCalculateHealth(int f)
        {
            var enemy = new Enemy.Enemy();
            Effect.Effect effect = new Effect.Effect(f, 10, 1);
            enemy.add_effect(effect);
            enemy.calculate_health();
            if (f == 20) Assert.That(enemy.Health, Is.EqualTo(80));
            else Assert.That(enemy.Health, Is.EqualTo(0));
        }

        [Test]
        public void TestMakeMove()
        {
            var enemy = new Enemy.Enemy();
            enemy.make_move(5, 12);
            Effect.Effect effect1 = new Effect.Effect(20, 10, 1);
            Effect.Effect effect2 = new Effect.Effect(20, 1, 1);
            enemy.add_effect(effect1);
            enemy.add_effect(effect2);
            enemy.make_move(8, 12);
            Assert.AreEqual(enemy.Coordinates.Item1, 8);
            Assert.AreEqual(enemy.Coordinates.Item2, 12);
            Assert.That(enemy.Health, Is.EqualTo(60));
            Assert.AreEqual(enemy.geteffect(0).Duration, 9);
        }

        [TestCase(7)]
        [TestCase(0)]
        [TestCase(-5)]
        public void TestSetters(int value)
        {
            var enemy = new Enemy.Enemy();
            if (value >= 0)
            {
                enemy.Health = value;
                enemy.Velocity = value;
                enemy.Gold = value;
                Assert.AreEqual(enemy.Health, value);
                Assert.AreEqual(enemy.Velocity, value);
                Assert.AreEqual(enemy.Gold, value);
            }
            else
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => enemy.Health = value);
                Assert.Throws<ArgumentOutOfRangeException>(() => enemy.Velocity = value);
                Assert.Throws<ArgumentOutOfRangeException>(() => enemy.Gold = value);
            }
        }

        [Test]
        public void TestGetEffect()
        {
            var enemy = new Enemy.Enemy();
            Assert.Throws<ArgumentOutOfRangeException>(() => enemy.geteffect(1));
            Effect.Effect effect1 = new Effect.Effect(20, 10, 1);
            enemy.add_effect(effect1);
            Assert.Throws<ArgumentOutOfRangeException>(() => enemy.geteffect(12));
        }
    }

    [TestFixture]
    class EnemyDoubleTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DefaultConstructor()
        {
            var enemy = new EnemyDouble();
            Assert.That(enemy.Name, Is.EqualTo("Villian"));
            Assert.That(enemy.Health, Is.EqualTo(100));
            Assert.That(enemy.Gold, Is.EqualTo(0));
            Assert.That(enemy.Velocity, Is.EqualTo(1));
            Assert.That(enemy.Coordinates.Item1, Is.EqualTo(0));
            Assert.That(enemy.Coordinates.Item2, Is.EqualTo(0));
            Assert.AreEqual(enemy.DevideTime, 30);
        }

        [Test]
        public void Setter()
        {
            var enemy = new EnemyDouble();
            enemy.DevideTime = 70;
            Assert.AreEqual(enemy.DevideTime, 70);
        }

        [Test]
        public void Copy()
        {
            var enemy1 = new EnemyDouble(12, "Krah");
            var enemy2 = enemy1.copy_enemy();
            Assert.IsTrue(enemy1.Equals(enemy2));
        }
    }

    [TestFixture]
    class EnemyAttackTest
    {
        [Test]
        public void DefaultConstructor()
        {
            var enemy = new EnemyAttack();
            enemy.Harm = 12;
            enemy.Radius = 10;
            Assert.That(enemy.Name, Is.EqualTo("Villian"));
            Assert.That(enemy.Health, Is.EqualTo(100));
            Assert.That(enemy.Gold, Is.EqualTo(0));
            Assert.That(enemy.Velocity, Is.EqualTo(1));
            Assert.That(enemy.Coordinates.Item1, Is.EqualTo(0));
            Assert.That(enemy.Coordinates.Item2, Is.EqualTo(0));
            Assert.That(enemy.Harm, Is.EqualTo(12));
            Assert.That(enemy.Radius, Is.EqualTo(10));
        }
    }
}
