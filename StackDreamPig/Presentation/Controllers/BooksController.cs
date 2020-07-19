using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Books.Commands;
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

        public BooksController(ISearchMemberQuary searchMemberQuary, IBooksRegistCommand booksRegistCommand)
        {
            _searchMemberQuary = searchMemberQuary;
            _booksRegistCommand = booksRegistCommand;
        }
        public IActionResult Books(BooksModel booksModel)
        {
            try
            { 
                var session_m_no = int.Parse(HttpContext.Session.GetString("m_no"));
                booksModel.amountLimit = _searchMemberQuary.GetMembersBooks(session_m_no);
                return View(booksModel);
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(booksModel, ex);
                
                return View ("_SessionErrorPage", booksModel);
            }

        }

        public IActionResult BooksRegisted(BooksModel booksModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Books",booksModel);
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
    }
}