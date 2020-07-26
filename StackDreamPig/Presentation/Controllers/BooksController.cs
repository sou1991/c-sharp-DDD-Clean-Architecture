﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Books.Commands;
using Application.Books.Query;
using Application.Member.Model;
using Application.Member.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stackDreamPig.Models.Book;
using stackDreamPig.Models.Book.Query;

namespace Presentation.Controllers
{
    public class BooksController : Controller
    {
        private ISearchMemberQuary _searchMemberQuary;
        private IBooksRegistCommand _booksRegistCommand;
        private ISearchBooksQuery _searchBooksQuery;

        public BooksController(ISearchMemberQuary searchMemberQuary, IBooksRegistCommand booksRegistCommand, ISearchBooksQuery searchBooksQuery)
        {
            _searchMemberQuary = searchMemberQuary;
            _booksRegistCommand = booksRegistCommand;
            _searchBooksQuery = searchBooksQuery;
        }

        public IActionResult Books(MemberModel memberModel)
        {
            try
            {
                memberModel.m_no = int.Parse(HttpContext.Session.GetString("m_no"));
                var member = _searchMemberQuary.Execute(memberModel);
                return View(member);
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(memberModel, ex);

                return View("_SessionErrorPage", memberModel);
            }

        }

        public IActionResult BooksRegisted(BooksModel booksModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Books", booksModel);
            }

            try
            {
                booksModel.m_no = int.Parse(HttpContext.Session.GetString("m_no"));
                _booksRegistCommand.Execute(booksModel);
                return View(booksModel);
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(booksModel, ex);

                return View("_SessionErrorPage", booksModel);
            }

        }
        public IActionResult BooksList(BooksModel booksModel)
        {
            if (!ModelState.IsValid)
            {
                return View(booksModel);
            }

            try
            {
                booksModel.m_no = int.Parse(HttpContext.Session.GetString("m_no"));
                booksModel.booksList = _searchBooksQuery.Execute(booksModel);
                return View(booksModel);
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(booksModel, ex);

                return View("_SessionErrorPage", booksModel);
            }
        }
    }
}