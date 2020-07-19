using Application.Books.Query;
using Application.Member.Model;
using Application.Member.Query;
using Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using stackDreamPig.Models.Book;
using stackDreamPig.Models.Book.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valueobject.Member;

namespace Tests.Application.Books
{
    [TestFixture]
    internal class SearchBooksTests
    {
        private ISearchMemberQuary _searchMemberQuary;
        private readonly int _m_no = 1;
        private readonly int _amountLimit = 10000;
    

        [SetUp]
        public void SetUp()
        {
            var memberEntity = new List<MemberEntity>()
            {
                 new MemberEntity()
                {
                    m_no = _m_no,
                    amountLimit = new AmountLimitValueObject(_amountLimit)
                }
            }.AsQueryable();

            var mockMyEntity = new Mock<DbSet<MemberEntity>>();
            // DbSetとテスト用データを紐付け
            mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Provider).Returns(memberEntity.Provider);
            mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Expression).Returns(memberEntity.Expression);
            mockMyEntity.As<IQueryable<Type>>().Setup(m => m.ElementType).Returns(memberEntity.ElementType);
            mockMyEntity.As<IQueryable<MemberEntity>>().Setup(m => m.GetEnumerator()).Returns(memberEntity.GetEnumerator());

            var mockContext = new Mock<IDataBaseService>();
            mockContext.Setup(m => m.Member).Returns(mockMyEntity.Object);

            _searchMemberQuary = new SearchMemberQuary(mockContext.Object);
        }

        [Test]
        public void TestShouldGetAmountLimitValue()
        {
            var result = _searchMemberQuary.GetMembersBooks(_m_no);
            Assert.AreNotEqual(result, _amountLimit);
        }
    }
}
