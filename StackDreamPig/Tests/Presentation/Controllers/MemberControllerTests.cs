using AutoMoq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Presentation.Controllers;
using Application.Member.Model;

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
        public void TestGetViewTypeEntry()
        {
            var result = _memberController.Entry(_meberModel);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void TestGetViewTypeEntryConfirm()
        {
            var result = _memberController.EntryConfirm(_meberModel);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void TestGetViewTypeEntryComplete()
        {
            var result = _memberController.EntryComplete(_meberModel);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
