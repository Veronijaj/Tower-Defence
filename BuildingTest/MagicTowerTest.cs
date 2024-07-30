using Building;
using Effect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingTest
{
    [TestFixture]
    internal class MagicTowerTest
    {
        [Test]
        public void DefaultConstructor()
        {
            var effect = new Effect.Effect();
            var tower = new Magic_Tower(effect);
            Assert.That(tower.Cost, Is.EqualTo(100));
            Assert.That(tower.Radius, Is.EqualTo(3));
            Assert.That(tower.Harm, Is.EqualTo(2));
            Assert.That(tower.Level, Is.EqualTo(1));
            Assert.That(tower.Endurance, Is.EqualTo(50));
            Assert.That(tower.SpeedShooting, Is.EqualTo(3));
            Assert.That(tower.Strategy_, Is.EqualTo(Strategy.Closet_to_castle));
            Assert.IsTrue(tower.MagicEffect == effect);
        }

        [Test]
        public void EffectTower() {
            var effect1 = new Effect.Effect();
            var tower = new Magic_Tower(effect1);
            var effect2 = new Effect.Effect(12, 4, 2);
            tower.MagicEffect = effect2;
            Assert.IsTrue(tower.MagicEffect == effect2);
        }

        [Test]
        public void AddEffect()
        {
            var effect = new Effect.Effect();
            var tower = new Magic_Tower(effect);
            var enemy = new Enemy.Enemy();
            tower.SpecialAction(enemy);
            Assert.IsTrue(enemy.geteffect(0) == effect);
        }
    }
}
