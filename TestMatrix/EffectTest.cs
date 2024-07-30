using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Effect;
using Matrix;

namespace Tests
{
    [TestFixture]
    class EffectTest
    {
        [Test]
        public void DefaultConstructor()
        {
            var effect = new Effect.Effect();
            Assert.That(effect.Force, Is.EqualTo(0));
            Assert.That(effect.Duration, Is.EqualTo(0));
            Assert.That(effect.Type, Is.EqualTo(TypeEffect.Slow_motion));
        }

        [TestCase]
        public void ConstructorWithInitialization()
        {
            var effect = new Effect.Effect(89, 5, 1);
            Assert.That(effect.Force, Is.EqualTo(89));
            Assert.That(effect.Duration, Is.EqualTo(5));
            Assert.That(effect.Type, Is.EqualTo(TypeEffect.Poisoning));
        }

        [TestCase(-9, 5, 1)]
        [TestCase(9, -5, 1)]
        [TestCase(9, 5, -1)]
        [TestCase(9, 5, 12)]
        public void ConstructorWithInitializationException(int f, int d, int te)
        {
            Effect.Effect effect;
            Assert.Throws<ArgumentOutOfRangeException>(() => effect = new Effect.Effect(f, d, te));
        }

        [Test]
        public void Setters()
        {
            var effect = new Effect.Effect();
            effect.Duration = 7;
            effect.Type = TypeEffect.Poisoning;
            effect.Force = 12;
            Assert.That(effect.Force, Is.EqualTo(12));
            Assert.That(effect.Duration, Is.EqualTo(7));
            Assert.That(effect.Type, Is.EqualTo(TypeEffect.Poisoning));
        }

        [Test]
        public void SettersException()
        {
            var effect = new Effect.Effect();
            Assert.Throws<ArgumentOutOfRangeException>(() => effect.Type=(Effect.TypeEffect)5);
            Assert.Throws<ArgumentOutOfRangeException>(() => effect.Type = (Effect.TypeEffect)(-5));
            Assert.Throws<ArgumentOutOfRangeException>(() => effect.Duration = -5);
            Assert.Throws<ArgumentOutOfRangeException>(() => effect.Force = -5);
        }
    }
}
