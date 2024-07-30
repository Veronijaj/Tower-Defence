using Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class MadicTrapTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var effect = new Effect.Effect();
            var tower = new Magic_Trap(effect);
            Assert.AreEqual(tower.Cost, 100);
            Assert.AreEqual(tower.Radius, 3);
            Assert.AreEqual(tower.Harm, 2);
            Assert.AreEqual(tower.Level, 1);
            Assert.AreEqual(tower.Endurance, 50);
            Assert.IsTrue(tower.MagicEffect == effect);
        }

        [TestMethod]
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
