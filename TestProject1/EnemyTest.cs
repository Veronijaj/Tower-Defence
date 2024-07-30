using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class EnemyTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            Enemy.Enemy enemy1 = new Enemy.Enemy();
            Assert.AreEqual(enemy1.Name, "Villian");
            Assert.AreEqual(enemy1.Health, 100);
            Assert.AreEqual(enemy1.Gold, 0);
            Assert.AreEqual(enemy1.Velocity, 1);
            Assert.AreEqual(enemy1.Coordinates.Item1, 0);
            Assert.AreEqual(enemy1.Coordinates.Item2, 0);
        }

        [TestMethod]
        public void TestAdditionEffect()
        {
            var enemy = new Enemy.Enemy();
            Effect.Effect effect = new Effect.Effect();
            enemy.add_effect(effect);
            Assert.IsTrue(enemy.geteffect(0) == effect);
        }
        [TestMethod]
        public void TestGetterVelocity()
        {
            var enemy = new Enemy.Enemy();
            Effect.Effect effect = new Effect.Effect(200, 10, 0);
            enemy.add_effect(effect);
            Assert.AreEqual(enemy.Velocity, 0);
        }

        [TestMethod]
        [DataRow(20)]
        [DataRow(300)]
        public void TestCalculateHealth(int f)
        {
            var enemy = new Enemy.Enemy();
            Effect.Effect effect = new Effect.Effect(f, 10, 1);
            enemy.add_effect(effect);
            enemy.calculate_health();
            if (f == 20) Assert.AreEqual(enemy.Health, 80);
            else Assert.AreEqual(enemy.Health, 0);
        }

        [TestMethod]
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
            Assert.AreEqual(enemy.Health, 60);
            Assert.AreEqual(enemy.geteffect(0).Duration, 9);
        }

        [TestMethod]
        [DataRow(7)]
        [DataRow(0)]
        [DataRow(-5)]
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
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => enemy.Health = value);
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => enemy.Velocity = value);
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => enemy.Gold = value);
            }
        }

        [TestMethod]
        public void TestGetEffect()
        {
            var enemy = new Enemy.Enemy();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => enemy.geteffect(1));
            Effect.Effect effect1 = new Effect.Effect(20, 10, 1);
            enemy.add_effect(effect1);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => enemy.geteffect(12));
        }
    }

    [TestClass]
    public class EnemyDoubleTest
    {

        [TestMethod]
        public void DefaultConstructor()
        {
            var enemy = new Enemy.EnemyDouble();
            Assert.AreEqual(enemy.Name, "Villian");
            Assert.AreEqual(enemy.Health, 100);
            Assert.AreEqual(enemy.Gold, 0);
            Assert.AreEqual(enemy.Velocity, 1);
            Assert.AreEqual(enemy.Coordinates.Item1, 0);
            Assert.AreEqual(enemy.Coordinates.Item2, 0);
            Assert.AreEqual(enemy.DevideTime, 30);
        }

        [TestMethod]
        public void Setter()
        {
            var enemy = new Enemy.EnemyDouble();
            enemy.DevideTime = 70;
            Assert.AreEqual(enemy.DevideTime, 70);
        }

        [TestMethod]
        public void Copy()
        {
            var enemy1 = new Enemy.EnemyDouble(12, "Krah");
            var enemy2 = enemy1.copy_enemy();
            Assert.IsTrue(enemy1.DevideTime == enemy2.DevideTime);
            Assert.IsTrue(enemy1.Health == enemy2.Health);
        }
    }

    [TestClass]
    public class EnemyAttackTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var enemy = new Enemy.EnemyAttack();
            enemy.Harm = 12;
            enemy.Radius = 10;
            Assert.AreEqual(enemy.Name, "Villian");
            Assert.AreEqual(enemy.Health, 100);
            Assert.AreEqual(enemy.Gold, 0);
            Assert.AreEqual(enemy.Velocity, 1);
            Assert.AreEqual(enemy.Coordinates.Item1, 0);
            Assert.AreEqual(enemy.Coordinates.Item2, 0);
            Assert.AreEqual(enemy.Harm, 12);
            Assert.AreEqual(enemy.Radius, 10);
        }
    }
}
