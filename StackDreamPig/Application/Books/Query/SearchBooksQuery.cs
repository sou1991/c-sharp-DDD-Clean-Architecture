using Application.Books.Model;
using Common;
using Common.Books;
using Entities;
using Entities.Books;
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

                var dataModel = new BooksDataModelBuilder();

                var viewModels = CreateViewModelList(books);

                return viewModels;
            }
            catch (NpgsqlException)
            {
                throw new Exception("データベース接続に失敗しました。");
            }
        }

        private IEnumerable<IBooksDTO> CreateViewModelList(IQueryable<BooksEntity> books)
        {
            foreach(var book in books)
            {
                var dataModel = new BooksDataModelBuilder();
                var viewModel = new BooksModel();

                book.Notice(dataModel);
                var dtoModel = dataModel.Build();

                viewModel.currencyTypeAmountUsed = dtoModel.amountUsed.ToString();
                viewModel.DispRegistDate = dtoModel.registDate;

                //To do リファクタリング同じ計算
                viewModel.monthlyTotalAmountUsed = books.Sum(p => p.amountUsed).ToString();

                yield return viewModel;
            }
        }
    }
}
