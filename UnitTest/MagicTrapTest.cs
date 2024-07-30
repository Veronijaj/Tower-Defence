using Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestFixture]
    internal class MagicTrapTest
    {
        [Test]
        public void DefaultConstructor()
        {
            var effect = new Effect.Effect();
            var tower = new Magic_Trap(effect);
            Assert.That(tower.Cost, Is.EqualTo(100));
            Assert.That(tower.Radius, Is.EqualTo(3));
            Assert.That(tower.Harm, Is.EqualTo(2));
            Assert.That(tower.Level, Is.EqualTo(1));
            Assert.That(tower.Endurance, Is.EqualTo(50));
            Assert.IsTrue(tower.MagicEffect == effect);
        }

        [Test]
        public void SpecialAction()
        {
            var effect = new Effect.Effect();
            var effect1 = new Effect.Effect(3, 2, 0);
            var tower = new Magic_Trap(effect);
            tower.MagicEffect = effect1;
            Assert.IsTrue(tower.MagicEffect == effect1);
            var enemy = new Enemy.Enemy();
            tower.SpecialAction(enemy);
            Assert.IsTrue(enemy.geteffect(0) == tower.MagicEffect);
        }
    }
}
