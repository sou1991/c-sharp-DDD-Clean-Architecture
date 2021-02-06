using Application.Books.Model;
using Common;
using Common.Books;
using Entities;
using Infrastructure;
using Npgsql;
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

        public IEnumerable<IBooksDTO> Execute(IBooksDTO booksModel)
        {
            if (string.IsNullOrEmpty(booksModel.m_no)) throw new ArgumentNullException(null, "セッションが切れました。再度ログインしてください。");

            try
            {
                var resultBooks = _dataBaseService.Books
                .OrderByDescending(p => p.registDate._registDate)
                .Where(p => p.m_no == booksModel.m_no
                && p.registDate._registDate.Year == booksModel.registrationDateSearch.Year
                && p.registDate._registDate.Month == booksModel.registrationDateSearch.Month);

                var books = resultBooks
                    .Select(p => new BooksModel()
                    {
                        currencyTypeAmountUsed = CurrencyType.CastIntegerToCurrencyType(p.amountUsed),
                        DispRegistDate = p.registDate._registDate,
                        monthlyTotalAmountUsed = CurrencyType.CastIntegerToCurrencyType(resultBooks.Sum(p => p.amountUsed))
                    });
                return books;
            }
            catch (NpgsqlException)
            {
                throw new Exception("データベース接続に失敗しました。");
            }
        }
    }
}
