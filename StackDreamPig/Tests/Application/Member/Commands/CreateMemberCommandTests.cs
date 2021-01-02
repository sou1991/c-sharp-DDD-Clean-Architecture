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
using System.Security.Permissions;
using AutoMoq;
using Valueobject.Member;
using Factory;

namespace Tests.Application.Member
{
    [TestFixture]
    internal class CreateMemberCommandTests
    {
        private MemberModel _memberModel;
        private CreateMemberCommand _command;
        private IQueryable<MemberEntity> _memberEntity;
        private Mock<DbSet<MemberEntity>> _mockMyEntity;

        private readonly int _m_no = 1;
        private readonly string _userName = "stgDummyUser";
        private readonly string _monthlyIncome = "300000";
        private readonly string _savings = "100000";
        private readonly string _fixedCost = "50000";
        private readonly int _amountLimit = 150000;

        [SetUp]
        public void SetUp()
        {
            var memberValueObject = SdpFactory.ValueObjectFactory().CreateMemberValueObject(_userName, null, null);
            var amountValueObject = SdpFactory.ValueObjectFactory().CreateAmountValueObject(_monthlyIncome, _savings, _fixedCost);
            var amountLimitValueObject = SdpFactory.ValueObjectFactory().CreateAmountLimitValueObject(_amountLimit);

            _memberEntity = new List<MemberEntity>
            {
               new MemberEntity(memberValueObject, amountValueObject, amountLimitValueObject, DateTime.Now)

            }.AsQueryable();

            _mockMyEntity = new Mock<DbSet<MemberEntity>>();
            // DbSetとテスト用データを紐付け
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Provider).Returns(_memberEntity.Provider);
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Expression).Returns(_memberEntity.Expression);
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.ElementType).Returns(_memberEntity.ElementType);
            _mockMyEntity.As<IQueryable<MemberEntity>>().Setup(m => m.GetEnumerator()).Returns(_memberEntity.GetEnumerator());


            _memberModel = new MemberModel
            {
                m_no = this._m_no,
                userName = "DummyUser",
                password = "DummyUser",
                monthlyIncome = this._monthlyIncome,
                savings = this._savings,
                fixedCost = this._fixedCost
            };

            var mockContext = new Mock<IDataBaseService>();
            mockContext.Setup(m => m.Member).Returns(_mockMyEntity.Object);

            _command = new CreateMemberCommand(mockContext.Object);
        }

        [Test]
        public void TestShouldAddToMemberTheDatabase()
        {
            _command.Execute(_memberModel);

            _mockMyEntity
            .Verify(p => p.Add(It.IsAny<MemberEntity>()), Times.Once);
            //.Verify(m => m.Add(It.Is<MemberEntity>(t => t.userName.Equals("testuser"))))

        }
        [Test]
        public void TestMemberRegisted()
        {
            var result = _command.HasRegistMember(_memberModel);
            Assert.That(result, Is.TypeOf<bool>()); ;
        }
    }

}
