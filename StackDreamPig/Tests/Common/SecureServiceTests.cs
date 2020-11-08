using Common.Member;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Common
{
    [TestFixture]
    internal class SecureServiceTests
    {
        private readonly string userName = "dummyUser";
        private readonly string password = "dummyPass";

        [Test]
        public void TestShouldSecureToUserName()
        {
            var secureService = new SecureService();
            var (secureValue, salt) = secureService.HashToValue(userName);

            Assert.True(secureService.VerifyPassword(secureValue, userName, salt));
        }

        [Test]
        public void TestShouldSecureTopassword()
        {
            var secureService = new SecureService();
            var (secureValue, salt) = secureService.HashToValue(password);

            Assert.True(secureService.VerifyPassword(secureValue, password, salt));
        }
    }
}
