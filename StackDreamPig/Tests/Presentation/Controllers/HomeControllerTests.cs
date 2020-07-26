using Application.Member.Model;
using AutoMoq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Presentation.Controllers
{
    [TestFixture]
    internal class HomeControllerTests
    {
        private HomeController _homeController;
        private AutoMoqer _mocker;
        private MemberModel _memberModel;
        private int _m_no = 1;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMoqer();
            _homeController = _mocker.Create<HomeController>();
            _memberModel = new MemberModel()
            {
                m_no = _m_no
            };
        }
        [Test]
        public void TestGetViewType()
        {
            var result = _homeController.Index(_memberModel);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void TestGetViewTypeLogin()
        {
            var result = _homeController.Login(_memberModel);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void TestGetViewTypeIndexOrLogin()
        {
            var result = _homeController.IndexOrLogin();
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
