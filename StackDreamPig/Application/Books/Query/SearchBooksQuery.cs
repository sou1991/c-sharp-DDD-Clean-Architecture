using Application.Books.Model;
using Common;
using Common.Books;
using Entities;
using Infrastructure;
using Infrastructure.Books;
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
        private IBooksRepository _booksRepository;

        public SearchBooksQuery(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public IEnumerable<IBooksDTO> Execute(IBooksDTO booksModel)
        {
            if (string.IsNullOrEmpty(booksModel.m_no)) throw new ArgumentNullException(null, "セッションが切れました。再度ログインしてください。");

            try
            {
                var books = _booksRepository
                            .Find(booksModel.m_no, booksModel.registrationDateSearch.Year, booksModel.registrationDateSearch.Month);

                var booksEntities = books
                    .Select(p => new BooksModel()
                    {
                        currencyTypeAmountUsed = CurrencyType.CastIntegerToCurrencyType(p.amountUsed),
                        DispRegistDate = p.registDate._registDate,
                        monthlyTotalAmountUsed = CurrencyType.CastIntegerToCurrencyType(books.Sum(p => p.amountUsed))
                    });

                return booksEntities;
            }
            catch (NpgsqlException)
            {
                throw new Exception("データベース接続に失敗しました。");
            }
        }
    }
}
