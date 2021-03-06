﻿using Application.Member.Model;
using Application.Member.Query;
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

namespace Tests.Application.Member.Query
{
    [TestFixture]
    internal class SearchMemberQuaryTests
    {
        private MemberModel _memberModel;

        private SearchMemberQuary _searchMemberQuary;

        private readonly string _m_no = "1";
        private readonly string _userName = "stgDummyUser";
        private readonly string _password = "BGAy4ewuMhZ8vjJz5OtxfYPLiumP/kbGRkudsuTObaE=";
        private readonly string _salt = "\"HStsgbnfIH5TmnK0Awr/lQ==\"";
        private readonly string _monthlyIncome = "300000"; 
        private readonly string _savings = "50000";
        private readonly string _fixedCost = "100000";
        private readonly int _amountLimit = 5357;


        [SetUp]
        public void SetUp()
        {
            var memberValueObject = SdpFactory.ValueObjectFactory().CreateMemberValueObject(_userName, _password, _salt);
            var amountValueObject = SdpFactory.ValueObjectFactory().CreateAmountValueObject(_monthlyIncome, _savings, _fixedCost);
            var amountLimitValueObject = SdpFactory.ValueObjectFactory().CreateAmountLimitValueObject(_amountLimit);

            var memberEntity = SdpFactory.EntityFactory().CreateMemberEntity(_m_no, memberValueObject, amountValueObject, amountLimitValueObject, DateTime.Now);

            
            _memberModel = new MemberModel
            {
                m_no = _m_no,
                userName = _userName,
                password = "stgDummyUser",
                monthlyIncome = _monthlyIncome,
                savings = _savings,
                fixedCost = _fixedCost
            };

            var mockContext = new Mock<IMemberRepository>();
            mockContext.Setup(m => m.GetUserWithSession(_m_no)).Returns(memberEntity);
            mockContext.Setup(m => m.GetUserWithUserName(_userName)).Returns(memberEntity);
            mockContext.Setup(m => m.GetUser(_userName,_password)).Returns(memberEntity);

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
           var result = _searchMemberQuary.AbleToLogin(_memberModel);
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
