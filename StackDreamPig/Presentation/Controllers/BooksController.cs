using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Books.Commands;
using Application.Books.Query;
using Application.Member.Model;
using Application.Member.Query;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stackDreamPig.Models.Book;
using stackDreamPig.Models.Book.Query;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
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
        [HttpPost]
        public IActionResult Books(MemberModel memberModel)
        {
            memberModel.m_no = HttpContext.Session.GetString("m_no");
            
            try
            {
                var member = _searchMemberQuary.Execute(memberModel);
                return View(member);
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(memberModel, ex);

                return View("_ErrorPage", memberModel);
            }

        }
        [HttpPost]
        public IActionResult BooksRegisted(BooksModel booksModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Books", booksModel);
            }

            booksModel.m_no = HttpContext.Session.GetString("m_no");
            
            try
            {
                _booksRegistCommand.Execute(booksModel);
                return View(booksModel);
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(booksModel, ex);

                return View("_ErrorPage", booksModel);
            }

        }
        [HttpPost]
        public IActionResult BooksList(BooksModel booksModel)
        {
            booksModel.m_no = HttpContext.Session.GetString("m_no");

            try
            {
                booksModel.booksList = _searchBooksQuery.Execute(booksModel);

                var member = _searchMemberQuary.Execute(new MemberModel { m_no = booksModel.m_no });

                booksModel.currencyTypeAmountLimit = CurrencyType.CastIntegerToCurrencyType(member.amountLimit);
                return View(booksModel);
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(booksModel, ex);

                return View("_ErrorPage", booksModel);
            }
        }
    }
}