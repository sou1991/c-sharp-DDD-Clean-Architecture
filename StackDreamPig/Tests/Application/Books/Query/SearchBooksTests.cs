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
using Valueobject.Books;
using Valueobject.Member;

namespace Tests.Application.Books
{
    [TestFixture]
    internal class SearchBooksTests
    {
        private ISearchBooksQuery _searchBooksQuery;
        private BooksModel _booksModel;
        private readonly int _m_no = 1;
        private readonly int _amountUsed = 10000;
        private readonly string _year = "2020";
        private readonly string _month = "7";


        [SetUp]
        public void SetUp()
        {
            var registdate = _year + "-" + _month;

            var booksEntity = new List<BooksEntity>()
            {
                 new BooksEntity()
                {
                    m_no = _m_no,
                    amountUsed = _amountUsed,
                    registDate = new RegistDateValueObject(DateTime.Parse(registdate))
                }
            }.AsQueryable();

            _booksModel = new BooksModel()
            {
                amountUsed = _amountUsed,
                m_no = _m_no,
                year = _year,
                month = _month
            };

            var mockMyEntity = new Mock<DbSet<BooksEntity>>();
            // DbSetとテスト用データを紐付け
            mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Provider).Returns(booksEntity.Provider);
            mockMyEntity.As<IQueryable<Type>>().Setup(m => m.Expression).Returns(booksEntity.Expression);
            mockMyEntity.As<IQueryable<Type>>().Setup(m => m.ElementType).Returns(booksEntity.ElementType);
            mockMyEntity.As<IQueryable<BooksEntity>>().Setup(m => m.GetEnumerator()).Returns(booksEntity.GetEnumerator());

            var mockContext = new Mock<IDataBaseService>();
            mockContext.Setup(m => m.Books).Returns(mockMyEntity.Object);

            _searchBooksQuery = new SearchBooksQuery(mockContext.Object);
        }

        [Test]
        public void TestShouldGetBooksList()
        {
            var results = _searchBooksQuery.Execute(_booksModel);

            var result = results.First();
            var registedDate = DateTime.Parse(_year + "/" + _month);

            Assert.That(result.amountUsed, Is.EqualTo(_amountUsed));
            Assert.That(result.DispRegistDate, Is.EqualTo(registedDate.Date));
        }
    }
}
