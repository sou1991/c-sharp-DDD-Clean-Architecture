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
        private readonly string _userName = "testuser";
        private readonly string _password = "test";
        private readonly string _monthlyIncome = "300000"; 
        private readonly string _savings = "50000";
        private readonly string _fixedCost = "100000";


        private readonly int _amountLimit = 4838;


        [SetUp]
        public void SetUp()
        {
            var memberEntity = new List<MemberEntity>
            {
                new MemberEntity
                {
                    m_no = _m_no,
                    userName = _userName,
                    password = _password,
                    monthlyIncome = _monthlyIncome,
                    savings = _savings,
                    fixedCost = _fixedCost,
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
                m_no = _m_no,
                userName = _userName,
                password = _password,

            };

            var mockContext = new Mock<IDataBaseService>();
            mockContext.Setup(m => m.Member).Returns(mockMyEntity.Object);

            _searchMemberQuary = new SearchMemberQuary(mockContext.Object);
        }

       [Test]
       public void TestShouldLoginSuccess()
       {
            var result = _searchMemberQuary.Execute(_memberModel);
            Assert.That(result.m_no, Is.EqualTo(_m_no));
       }

       [Test]
       public void TestShouldLoginFailed()
       {
           var result = _searchMemberQuary.Execute(_memberModel);
           Assert.AreNotEqual(result.m_no, 2);
       }
        [Test]
        public void CheckLoginTests()
        {
            var result = _searchMemberQuary.CheckLogin(_memberModel);
            Assert.That(result.m_no, Is.EqualTo(_m_no));
        }

        [Test]
        public void GetOneMemberTests()
        {
            var result = _searchMemberQuary.GetOneMember(_memberModel);
            Assert.That(result.m_no, Is.EqualTo(_m_no));
            Assert.That(result.userName, Is.EqualTo(_userName));
            Assert.That(result.monthlyIncome, Is.EqualTo(_monthlyIncome));
            Assert.That(result.savings, Is.EqualTo(_savings));
            Assert.That(result.fixedCost, Is.EqualTo(_fixedCost));
            Assert.That(result.amountLimit, Is.EqualTo(_amountLimit));
        }
    }
}
