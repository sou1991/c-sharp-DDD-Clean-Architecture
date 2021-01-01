using Application.Books.Commands;
using Common.Books;
using Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using stackDreamPig.Models.Book.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valueobject.Books;

namespace Tests.Application.Books.Commands
{
    [TestFixture]
    internal class BooksRegistTests
    {
        private IBooksRegistCommand _booksRegistCommand;
        private BooksModel _booksModel;
        private Mock<DbSet<BooksEntity>> _mockMyEntity;
        private Mock<IDataBaseService> _mockContext;

        private readonly int _m_no = 1;
        private readonly int _amountUsed = 10000;
        private readonly string _year = "2020";
        private readonly string _month = "7";
        private readonly string _day = "1";
        [SetUp]
        public void SetUp()
        {
            var registDate = _year + "/" + _month + "/" + _day;

            var booksEntity = new List<BooksEntity>
            {
                new BooksEntity(_m_no, _amountUsed, DateTime.Now, new RegistDateValueObject(DateTime.Parse(registDate)))

            }.AsQueryable();

            _booksModel = new BooksModel()
            {
                amountUsed = _amountUsed,
                m_no = _m_no,
                year = _year,
                month = _month,
                day = _day
            };

            _mockMyEntity = new Mock<DbSet<BooksEntity>>();

            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Provider).Returns(booksEntity.Provider);
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Expression).Returns(booksEntity.Expression);
            _mockMyEntity.As<IQueryable<Type>>().Setup(m => m.ElementType).Returns(booksEntity.ElementType);
            _mockMyEntity.As<IQueryable<BooksEntity>>().Setup(m => m.GetEnumerator()).Returns(booksEntity.GetEnumerator());

            _mockContext = new Mock<IDataBaseService>();
            _mockContext.Setup(p => p.Books).Returns(_mockMyEntity.Object);

            _booksRegistCommand = new BooksRegistCommand(_mockContext.Object);
        }

        [Test]
        public void TestShouldAddToBooksTheDatabase()
        {
            _booksModel.m_no = 1000;
            _booksRegistCommand.Execute(_booksModel);

            _mockMyEntity
            .Verify(p => p.Add(It.IsAny<BooksEntity>()), Times.Once);
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

