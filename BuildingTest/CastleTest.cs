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
    internal class CastleTest
    {
        [Test]
        public void DefaultConstructor()
        {
            var castle = new Castle();
            Assert.Multiple(() =>
            {
                Assert.That(castle.Name, Is.EqualTo("White house"));
                Assert.That(castle.Endurance, Is.EqualTo(1000));
                Assert.That(castle.EnduranceMax, Is.EqualTo(1000));
                Assert.That(castle.Gold, Is.EqualTo(10000));
            });
        }

        [TestCase(100, 100)]
        [TestCase(13, 6)]
        [TestCase(20, 80)]
        public void MakeHarmTest(int _health, int _endurance)
        {
            var castle = new Castle
            {
                Endurance = _endurance
            };
            var enemy = new Enemy.Enemy
            {
                Health = _health
            };
            if (_health >= _endurance)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => castle.MakeHarm(enemy));
            }
            else
            {
                castle.MakeHarm(enemy);
                Assert.That(castle.Endurance, Is.EqualTo(_endurance - _health));
            }
        }

        [Test]
        public void SetterGold()
        {
            var castle = new Castle
            {
                Gold = 15
            };
            Assert.That(castle.Gold, Is.EqualTo(15));
        }
    }
}
