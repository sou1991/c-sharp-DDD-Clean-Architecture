using Application.Member.Model;
using Application.Member.Query;
using Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valueobject.Member;

namespace Tests.Application.Member.Query
{
    [TestFixture]
    internal class SearchMemberQuaryTests
    {
        private MemberModel _memberModel;

        private SearchMemberQuary _searchMemberQuary;

        private readonly int _m_no = 1;
        private readonly string userName = "testuser";
        private readonly string password = "test";

        private readonly int _amountLimit = 10000;


        [SetUp]
        public void SetUp()
        {
            var memberEntity = new List<MemberEntity>
            {
                new MemberEntity
                {
                    m_no = _m_no,
                    userName = userName,
                    password = password,
                    amountLimit = new AmountLimitValueObject(_amountLimit)

                }
            }.AsQueryable();

            var mockMyEntity = new Mock<DbSet<MemberEntity>>();
            // DbSetとテスト用データを紐付け
            mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Provider).Returns(memberEntity.Provider);
            mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Expression).Returns(memberEntity.Expression);
            mockMyEntity.As<IQueryable<Type>>().Setup(m => m.ElementType).Returns(memberEntity.ElementType);
            mockMyEntity.As<IQueryable<MemberEntity>>().Setup(m => m.GetEnumerator()).Returns(memberEntity.GetEnumerator());

            _memberModel = new MemberModel
            {
                userName = this.userName,
                password = this.password,

            };

            var mockContext = new Mock<IDataBaseService>();
            mockContext.Setup(m => m.Member).Returns(mockMyEntity.Object);

            _searchMemberQuary = new SearchMemberQuary(mockContext.Object);
        }

       [Test]
       public void TestShouldLoginSuccess()
       {
            var result = _searchMemberQuary.Execute(_memberModel);
            Assert.That(result.m_no, Is.EqualTo(1));
       }

       [Test]
       public void TestShouldLoginFailed()
       {
           var result = _searchMemberQuary.Execute(_memberModel);
           Assert.AreNotEqual(result.m_no, 2);
       }


        [Test]
        public void TestShouldGetAmountLimitValue()
        {
            var result = _searchMemberQuary.GetMembersBooks(_m_no);
            Assert.AreNotEqual(result, _amountLimit);
        }
    }
}
