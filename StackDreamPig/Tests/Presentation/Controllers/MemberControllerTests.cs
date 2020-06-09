using AutoMoq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Presentation.Controllers;
using Application.Member.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Presentation.Controllers
{
    [TestFixture]
    internal class MemberControllerTests
    {
        private MemberController _memberController;
        private AutoMoqer _mocker;
        private MemberModel _meberModel;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMoqer();
            _memberController = _mocker.Create<MemberController>();
            _meberModel = new MemberModel();
        }
        [Test]
        public void TestGetViewType()
        {
            var result = _memberController.Entry(_meberModel);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
