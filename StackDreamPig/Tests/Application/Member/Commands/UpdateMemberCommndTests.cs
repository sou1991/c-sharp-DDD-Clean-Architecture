﻿using Application.Member.Commands;
using Application.Member.Model;
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

namespace Tests.Application.Member.Commands
{
    [TestFixture]
    internal class UpdateMemberCommndTests
    {
        private MemberModel _memberModel;
        private UpdateMemberCommnd _command;
        private IQueryable<MemberEntity> _memberEntity;
        private Mock<DbSet<MemberEntity>> _mockMyEntity;
        private Mock<IDataBaseService> _mockContext;

        private readonly int _m_no = 1;
        private readonly string _userName = "testuser";
        private readonly string _password = "test";
        private readonly string _monthlyIncome = "300000";
        private readonly string _savings = "100000";
        private readonly string _fixedCost = "50000";
        private readonly int _amontLimit = 150000;

        [SetUp]
        public void SetUp()
        {
            var memberValueObject = new MemberValueObject(_userName, null, null);
            var amountValueObject = new AmountValueObject(_monthlyIncome, _savings, _fixedCost);
            var amountLimitValueObject = new AmountLimitValueObject(_amontLimit);

            _memberEntity = new List<MemberEntity>
            {
                //new MemberEntity
                //{
                //    m_no = this.m_no,
                //    userName = userName,
                //    password = this.password,
                //    monthlyIncome = this.monthlyIncome,
                //    savings = this.savings,
                //    fixedCost = this.fixedCost,
                //    amountLimit = new AmountLimitValueObject(amontLimit)
                //}

            }.AsQueryable();

            _mockMyEntity = new Mock<DbSet<MemberEntity>>();
            // DbSetとテスト用データを紐付け
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Provider).Returns(_memberEntity.Provider);
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Expression).Returns(_memberEntity.Expression);
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.ElementType).Returns(_memberEntity.ElementType);
            _mockMyEntity.As<IQueryable<MemberEntity>>().Setup(m => m.GetEnumerator()).Returns(_memberEntity.GetEnumerator());

            _mockContext = new Mock<IDataBaseService>();
            _mockContext.Setup(m => m.Member).Returns(_mockMyEntity.Object);

            _command = new UpdateMemberCommnd(_mockContext.Object);
        }

        [Test]
        public void TestShouldUpdateMemberTheDatabase()
        {
            _memberModel = new MemberModel
            {
                m_no = this._m_no,
                userName = "太郎",
                monthlyIncome = this._monthlyIncome,
                savings = this._savings,
                fixedCost = this._fixedCost
            };

            _command.Execute(_memberModel);

            _mockContext.Verify(p => p.Save(), Times.Once);
            //.Verify(m => m.Add(It.Is<MemberEntity>(t => t.userName.Equals("testuser"))))
        }

        [Test]
        public void TestShouldCantUpdateMemberTheDatabase()
        {
            _memberModel = new MemberModel
            {
                m_no = 2,
                userName = _userName,
                monthlyIncome = this._monthlyIncome,
                savings = this._savings,
                fixedCost = this._fixedCost
            };

            _command.Execute(_memberModel);

            _mockContext.Verify(p => p.Save(), Times.Never);
            //.Verify(m => m.Add(It.Is<MemberEntity>(t => t.userName.Equals("testuser"))))
        }
    }
}
