using AutoMoq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Presentation.Controllers
{
    class LoginControllerTests
    {
        private LoginController _LoginController;
        private AutoMoqer _mocker;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMoqer();
            _LoginController = new LoginController();
        }
        [Test]
        public void TestGetViewType()
        {
            var result = _LoginController.Login();
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
