using Application.Member.DomainService;
using Application.Member.Model;
using Application.Member.Query;
using Entities;
using Entities.Member;
using Factory;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Application.Member.DomainService
{
    [TestFixture]
    internal class MemberDomainServiceTests
    {
        private MemberModel _memberModel;

        private MemberDomainService _memberDomainService;

        private readonly string _m_no = "1";
        private readonly string _userName = "stgDummyUser";
        private readonly string _password = "BGAy4ewuMhZ8vjJz5OtxfYPLiumP/kbGRkudsuTObaE=";
        private readonly string _salt = "\"HStsgbnfIH5TmnK0Awr/lQ==\"";
        private readonly string _monthlyIncome = "300000";
        private readonly string _savings = "50000";
        private readonly string _fixedCost = "100000";
        private readonly int _amountLimit = 4838;

        
        [SetUp]
        public void SetUp()
        {
            var memberValueObject = SdpFactory.ValueObjectFactory().CreateMemberValueObject(_userName, _password, _salt);
            var amountValueObject = SdpFactory.ValueObjectFactory().CreateAmountValueObject(_monthlyIncome, _savings, _fixedCost);
            var amountLimitValueObject = SdpFactory.ValueObjectFactory().CreateAmountLimitValueObject(_amountLimit);

            var memberEntity = new List<MemberData>
            {
               new MemberData(memberValueObject, amountValueObject, amountLimitValueObject, DateTime.Now)

            }.AsQueryable();

            var mockMemberEntity = new Mock<DbSet<MemberData>>();
            // DbSetとテスト用データを紐付け
            mockMemberEntity.As<IQueryable<Type>>().Setup(m => m.Provider).Returns(memberEntity.Provider);
            mockMemberEntity.As<IQueryable<Type>>().Setup(m => m.Expression).Returns(memberEntity.Expression);
            mockMemberEntity.As<IQueryable<Type>>().Setup(m => m.ElementType).Returns(memberEntity.ElementType);
            mockMemberEntity.As<IQueryable<MemberData>>().Setup(m => m.GetEnumerator()).Returns(memberEntity.GetEnumerator());


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
            mockContext.Setup(m => m.Member).Returns(mockMemberEntity.Object);

            _memberDomainService = new MemberDomainService(mockContext.Object);
        }

        [Test]
        public void TestMemberRegisted()
        {
            var result = _memberDomainService.HasRegistMember(_memberModel);
            Assert.That(result, Is.TypeOf<bool>()); ;
        }
    }
}
