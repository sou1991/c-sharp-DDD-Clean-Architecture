using NUnit.Framework;
using Valueobject.Member;

namespace Tests.ValueObject
{
    [TestFixture]
    internal class ValueObjectTests
    {
        private AmountLimitValueObject _amountLimitValueObject;

        [SetUp]
        public void SetUp()
        {
            _amountLimitValueObject = new AmountLimitValueObject(777);
        }

        [Test]
        public void EqualValueObjectTest()
        {
            var result = _amountLimitValueObject.Equals(new AmountLimitValueObject(777));
            Assert.That(result);


        }

        [Test]
        public void NotEqualValueObjectTest()
        {
            var vo = new AmountLimitValueObject(75577);

            Assert.AreEqual(true, _amountLimitValueObject != vo);
        }
    }
}
