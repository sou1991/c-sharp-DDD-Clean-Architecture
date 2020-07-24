using Common.Books;
using Entities;
using Infrastructure;
using stackDreamPig.Models.Book;
using stackDreamPig.Models.Book.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valueobject.Books;

namespace Application.Books.Query
{
    public class SearchBooksQuery : ISearchBooksQuery
    {
        private IDataBaseService _dataBaseService;

        public SearchBooksQuery(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }


        public List<BooksModel> Execute(BooksModel booksModel)
        {
            var books = _dataBaseService.Books
                .OrderBy(p => p.registDate._registDate)
                .Where(p => p.m_no == booksModel.m_no 
                && p.registDate._registDate.Year == booksModel.registrationDateSearch.Year 
                && p.registDate._registDate.Month == booksModel.registrationDateSearch.Month)
                .Select(p => new BooksModel()
                {
                    amountUsed = p.amountUsed,
                    DispRegistDate = p.registDate._registDate
                });

            return books.ToList();

        }
    }
}
