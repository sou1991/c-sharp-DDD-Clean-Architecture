using Application.Member.Commands;
using Entities;
using NUnit.Framework;
using Application.Member.Model;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System;
using Infrastructure;
using Factory;
using Infrastructure.Member;

namespace Tests.Application.Member
{
    [TestFixture]
    internal class CreateMemberCommandTests
    {
        private MemberModel _memberModel;
        private CreateMemberCommand _command;
        private Mock<IMemberRepository> _mockDataSource;

        private readonly string _m_no = "1";
        private readonly string _userName = "stgDummyUser";
        private readonly string _password = "BGAy4ewuMhZ8vjJz5OtxfYPLiumP/kbGRkudsuTObaE=";
        private readonly string _salt = "\"HStsgbnfIH5TmnK0Awr/lQ==\"";
        private readonly string _monthlyIncome = "300000";
        private readonly string _savings = "100000";
        private readonly string _fixedCost = "50000";
        private readonly int _amountLimit = 150000;

        [SetUp]
        public void SetUp()
        {
            var memberValueObject = SdpFactory.ValueObjectFactory().CreateMemberValueObject(_userName, _password, _salt);
            var amountValueObject = SdpFactory.ValueObjectFactory().CreateAmountValueObject(_monthlyIncome, _savings, _fixedCost);
            var amountLimitValueObject = SdpFactory.ValueObjectFactory().CreateAmountLimitValueObject(_amountLimit);

            var memberEntity = new MemberEntity(memberValueObject, amountValueObject, amountLimitValueObject, DateTime.Now);

            _memberModel = new MemberModel
            {
                m_no = this._m_no,
                userName = "DummyUser",
                password = "DummyUser",
                monthlyIncome = this._monthlyIncome,
                savings = this._savings,
                fixedCost = this._fixedCost
            };

            _mockDataSource = new Mock<IMemberRepository>();

            _command = new CreateMemberCommand(_mockDataSource.Object);
        }

        [Test]
        public void TestShouldAddToMemberTheDatabase()
        {
            _command.Execute(_memberModel);

            _mockDataSource
            .Verify(p => p.Create(It.IsAny<MemberEntity>()), Times.Once);
            //.Verify(m => m.Add(It.Is<MemberEntity>(t => t.userName.Equals("testuser"))))
        }
    }

}
