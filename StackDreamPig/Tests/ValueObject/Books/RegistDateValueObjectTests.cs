using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Valueobject.Books;

namespace Tests.ValueObject.Books
{
    [TestFixture]
    internal class RegistDateValueObjectTests
    {
        private RegistDateValueObject _registDateValueObject;
        private string _registDate;

        [SetUp]
        public void SetUp()
        {
            var dt = DateTime.Now;
            _registDate = dt.Year + "/" + dt.Month + "/" + dt.Day;

            _registDateValueObject = new RegistDateValueObject(DateTime.Parse(_registDate));
        }

        [Test]
        public void EqualValueObjectTest()
        {
            var result = _registDateValueObject.Equals(new RegistDateValueObject(DateTime.Parse(_registDate)));
            Assert.That(result);


        }

        [Test]
        public void NotEqualValueObjectTest()
        {
            _registDate = "1999/01/01";
            var vo = new RegistDateValueObject(DateTime.Parse(_registDate));

            Assert.AreEqual(true, _registDateValueObject != vo);
        }
    }
}
