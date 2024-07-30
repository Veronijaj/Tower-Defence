using Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class MagicTower
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var effect = new Effect.Effect();
            var tower = new Magic_Tower(effect);
            Assert.AreEqual(tower.Cost, 100);
            Assert.AreEqual(tower.Radius, 3);
            Assert.AreEqual(tower.Harm, 2);
            Assert.AreEqual(tower.Level, 1);
            Assert.AreEqual(tower.Endurance, 50);
            Assert.AreEqual(tower.SpeedShooting, 3 );
            Assert.AreEqual(tower.Strategy_, Strategy.Closet_to_castle);
            Assert.IsTrue(tower.MagicEffect == effect);
        }

        [TestMethod]
        public void EffectTower()
        {
            var effect1 = new Effect.Effect();
            var tower = new Magic_Tower(effect1);
            var effect2 = new Effect.Effect(12, 4, 2);
            tower.MagicEffect = effect2;
            Assert.IsTrue(tower.MagicEffect == effect2);
        }

        [TestMethod]
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
