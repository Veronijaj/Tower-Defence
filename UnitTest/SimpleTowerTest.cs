using Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestFixture]
    internal class SimpleTowerTest
    {
        [Test]
        public void DefaultConstructor()
        {
            var tower = new Simple_Tower();
            Assert.That(tower.Cost, Is.EqualTo(100));
            Assert.That(tower.Radius, Is.EqualTo(3));
            Assert.That(tower.Harm, Is.EqualTo(2));
            Assert.That(tower.Level, Is.EqualTo(1));
            Assert.That(tower.Endurance, Is.EqualTo(50));
            Assert.That(tower.SpeedShooting, Is.EqualTo(3));
            Assert.That(tower.Strategy_, Is.EqualTo(Strategy.Closet_to_castle));
        }

        [TestCase(4, 2)]
        [TestCase(0, 0)]
        [TestCase(-1, 23)]
        [TestCase(-6, -8)]
        public void Setters(int ss, int s)
        {
            var tower = new Simple_Tower();
            if (ss > 0)
            {
                tower.SpeedShooting = ss;
                Assert.That(tower.SpeedShooting, Is.EqualTo(ss));
            }
            else Assert.Throws<ArgumentOutOfRangeException>(() => tower.SpeedShooting = ss);
            if (s >= 0 && s < 5)
            {
                tower.Strategy_ = (Strategy)s;
                Assert.That(tower.Strategy_, Is.EqualTo((Strategy)s));
            }
            else Assert.Throws<ArgumentOutOfRangeException>(() => tower.Strategy_ = (Strategy)s);
        }
    }
}
