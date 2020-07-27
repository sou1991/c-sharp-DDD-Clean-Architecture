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

namespace Tests.Application.Member
{
    [TestFixture]
    internal class CreateMemberCommandTests
    {
        private MemberModel _memberModel;
        private CreateMemberCommand _command;
        private IQueryable<MemberEntity> _memberEntity;
        private Mock<DbSet<MemberEntity>> _mockMyEntity;

        private readonly int m_no = 1;
        private readonly string userName = "testuser";
        private readonly string password = "test";
        private readonly string monthlyIncome = "300000";
        private readonly string savings = "100000";
        private readonly string fixedCost = "50000";
        private readonly int amontLimit = 150000;

        [SetUp]
        public void SetUp()
        {

            _memberEntity = new List<MemberEntity>
            {
                new MemberEntity
                {
                    m_no = this.m_no,
                    userName = userName,
                    password = this.password,
                    monthlyIncome = this.monthlyIncome,
                    savings = this.savings,
                    fixedCost = this.fixedCost,
                    amountLimit = new AmountLimitValueObject(amontLimit)
                }

            }.AsQueryable();

            _mockMyEntity = new Mock<DbSet<MemberEntity>>();
            // DbSetとテスト用データを紐付け
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Provider).Returns(_memberEntity.Provider);
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Expression).Returns(_memberEntity.Expression);
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.ElementType).Returns(_memberEntity.ElementType);
            _mockMyEntity.As<IQueryable<MemberEntity>>().Setup(m => m.GetEnumerator()).Returns(_memberEntity.GetEnumerator());


            _memberModel = new MemberModel
            {
                m_no = this.m_no,
                userName = "太郎",
                password = "山田",
                monthlyIncome = this.monthlyIncome,
                savings = this.savings,
                fixedCost = this.fixedCost
            };

            var mockContext = new Mock<IDataBaseService>();
            mockContext.Setup(m => m.Member).Returns(_mockMyEntity.Object);

            _command = new CreateMemberCommand(mockContext.Object);
        }
        /***
         * 
         * INSERTの成功テストはm_no = 0にする
         * UPDATEの成功テストはm_no = 1にする
         * 
         ***/
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

        [Test]
        public void TestShouldMemberUpdate()
        {
            _command.Execute(_memberModel);

            _mockMyEntity
            .Verify(p => p.Add(It.IsAny<MemberEntity>()), Times.Never);

        }

    }

}
