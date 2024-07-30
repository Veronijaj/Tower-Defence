using Effect;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class EffectTest
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var effect = new Effect.Effect();
            Assert.AreEqual(effect.Force, 0);
            Assert.AreEqual(effect.Duration, 0);
            Assert.AreEqual(effect.Type, TypeEffect.Slow_motion);
        }

        [TestMethod]
        public void ConstructorWithInitialization()
        {
            var effect = new Effect.Effect(89, 5, 1);
            Assert.AreEqual(effect.Force, 89);
            Assert.AreEqual(effect.Duration, 5);
            Assert.AreEqual(effect.Type, TypeEffect.Poisoning);
        }

        [TestMethod]
        [DataRow(-9, 5, 1)]
        [DataRow(9, -5, 1)]
        [DataRow(9, 5, -1)]
        [DataRow(9, 5, 12)]
        public void ConstructorWithInitializationException(int f, int d, int te)
        {
            Effect.Effect effect;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect = new Effect.Effect(f, d, te));
        }

        [TestMethod]
        public void Setters()
        {
            var effect = new Effect.Effect();
            effect.Duration = 7;
            effect.Type = TypeEffect.Poisoning;
            effect.Force = 12;
            Assert.AreEqual(effect.Force, 12);
            Assert.AreEqual(effect.Duration, 7);
            Assert.AreEqual(effect.Type, TypeEffect.Poisoning);
        }

        [TestMethod]
        public void SettersException()
        {
            var effect = new Effect.Effect();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect.Type = (Effect.TypeEffect)5);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect.Type = (Effect.TypeEffect)(-5));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect.Duration = -5);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect.Force = -5);
        }
    }
}
