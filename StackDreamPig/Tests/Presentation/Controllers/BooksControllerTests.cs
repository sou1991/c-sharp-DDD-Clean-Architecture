using Application.Member.Model;
using AutoMoq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Presentation.Controllers;
using stackDreamPig.Models.Book;
using stackDreamPig.Models.Book.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Presentation.Controllers
{
    [TestFixture]
    internal class BooksControllerTests
    {

        private BooksController _booksController;
        private AutoMoqer _mocker;
        private BooksModel _booksModel;
        private int _m_no = 1;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMoqer();
            _booksController = _mocker.Create<BooksController>();
            _booksModel = new BooksModel()
            {
                m_no = _m_no
            };
        }
        [Test]
        public void TestGetViewTypeBooks()
        {
            var result = _booksController.Books(_booksModel);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
        [Test]
        public void TestGetViewTypeBooksRegisted()
        {
            var result = _booksController.BooksRegisted(_booksModel);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void TestGetViewTypeBooksList()
        {
            var result = _booksController.BooksList(_booksModel);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
