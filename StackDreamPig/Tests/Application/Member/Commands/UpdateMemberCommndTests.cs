using Application.Member.Commands;
using Application.Member.Model;
using Entities;
using Factory;
using Infrastructure;
using Infrastructure.Member;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Application.Member.Commands
{
    [TestFixture]
    internal class UpdateMemberCommndTests
    {
        private MemberModel _memberModel;
        private UpdateMemberCommnd _command;
        private MemberEntity _memberEntity;
        private Mock<DbSet<MemberEntity>> _mockMyEntity;
        private Mock<IMemberRepository> _mockDataSource;

        private readonly string _m_no = "2";
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

            _memberEntity = SdpFactory.EntityFactory().CreateMemberEntity(_m_no, memberValueObject, amountValueObject, amountLimitValueObject, DateTime.Now);

            _mockDataSource = new Mock<IMemberRepository>();

            _mockDataSource.Setup(m => m.GetUserWithSession(_m_no)).Returns(_memberEntity);
            _mockDataSource.Setup(m => m.GetUserWithUserName(_userName)).Returns(_memberEntity);

            _command = new UpdateMemberCommnd(_mockDataSource.Object);
        }

        [Test]
        public void TestShouldUpdateMemberTheDatabase()
        {
            _memberModel = new MemberModel
            {
                m_no = "2",
                userName = _userName,
                password = "stgDummyUser",
                monthlyIncome = _monthlyIncome,
                savings = _savings,
                fixedCost = _fixedCost
            };

            _command.Execute(_memberModel);

            _mockDataSource.Verify(p => p.Save(), Times.Once);
            //.Verify(m => m.Add(It.Is<MemberEntity>(t => t.userName.Equals("testuser"))))
        }
    }
}
