using Application.Books.Commands;
using Entities;
using Factory;
using Infrastructure;
using Infrastructure.Books;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using stackDreamPig.Models.Book.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Valueobject.Books;

namespace Tests.Application.Books.Commands
{
    [TestFixture]
    internal class BooksRegistTests
    {
        private IBooksRegistCommand _booksRegistCommand;
        private BooksModel _booksModel;
        private Mock<IBooksRepository> _mockContext;

        private readonly int _id = 1;
        private readonly string _m_no = "1";
        private readonly int _amountUsed = 10000;
        private readonly string _year = "2020";
        private readonly string _month = "7";
        private readonly string _day = "1";

        [SetUp]
        public void SetUp()
        {
            var registDate = _year + "/" + _month + "/" + _day;

            var booksEntity = SdpFactory.EntityFactory().CreateBooksEntity(_id, _m_no, _amountUsed, DateTime.Now, new RegistDateValueObject(DateTime.Parse(registDate)));

            _booksModel = new BooksModel()
            {
                amountUsed = _amountUsed,
                m_no = _m_no,
                year = _year,
                month = _month,
                day = _day
            };

            _mockContext = new Mock<IBooksRepository>();

            _mockContext.Setup(p => p.FindSingle(_m_no, _booksModel.registDate)).Returns(booksEntity);
            _booksRegistCommand = new BooksRegistCommand(_mockContext.Object);
        }

        [Test]
        public void TestShouldAddToBooksTheDatabase()
        {
            _booksModel.m_no = "1000";
            _booksRegistCommand.Execute(_booksModel);

            _mockContext
            .Verify(p => p.Create(It.IsAny<BooksEntity>()), Times.Once);
        }

        [Test]
        public void TestSuccessBooksRegist()
        {
            _booksRegistCommand.Execute(_booksModel);

            _mockContext
            .Verify(p => p.Save());
        }
    }
}

