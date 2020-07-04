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

namespace Tests.Application.Member.Query
{
    [TestFixture]
    internal class SearchMemberQuaryTests
    {
        private LoginModel _loginModel;
        private IQueryable<MemberEntity> _memberEntity;
        private Mock<DbSet<MemberEntity>> _mockMyEntity;

        private SearchMemberQuary _searchMemberQuary;

        private readonly string userName = "testuser";
        private readonly string password = "test";


        [SetUp]
        public void SetUp()
        {
            _memberEntity = new List<MemberEntity>
            {
                new MemberEntity
                {
                    m_no = 1,
                    userName = this.userName,
                    password = this.password
                }
            }.AsQueryable();

            _mockMyEntity = new Mock<DbSet<MemberEntity>>();
            // DbSetとテスト用データを紐付け
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Provider).Returns(_memberEntity.Provider);
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Expression).Returns(_memberEntity.Expression);
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.ElementType).Returns(_memberEntity.ElementType);
            _mockMyEntity.As<IQueryable<MemberEntity>>().Setup(m => m.GetEnumerator()).Returns(_memberEntity.GetEnumerator());

            _loginModel = new LoginModel
            {
                userName = this.userName,
                password = this.password,

            };

            var mockContext = new Mock<IDataBaseService>();
            mockContext.Setup(m => m.Member).Returns(_mockMyEntity.Object);

            _searchMemberQuary = new SearchMemberQuary(mockContext.Object);
        }

       [Test]
       public void TestShouldLoginSuccess()
       {
            var result = _searchMemberQuary.Execute(_loginModel);
            Assert.That(result, Is.EqualTo(1));
       }

       [Test]
       public void TestShouldLoginFailed()
       {
           var result = _searchMemberQuary.Execute(_loginModel);
           Assert.That(result, Is.EqualTo(null));
       }
    }
}
