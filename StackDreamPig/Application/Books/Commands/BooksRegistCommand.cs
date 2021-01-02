using Entities;
using Infrastructure;
using stackDreamPig.Models.Book.Query;
using System;
using System.Collections.Generic;
using System.Text;
using Valueobject.Books;
using System.Linq;
using Common.Books;
using Npgsql;
using Factory;

namespace Application.Books.Commands
{
    public class BooksRegistCommand : IBooksRegistCommand
    {
        private IDataBaseService _dataBaseService;

        public BooksRegistCommand(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }
        public void Execute(BooksModel booksModel)
        {
            var DataTimeChangeToDataBaseFormat = booksModel.registDate;

            try
            {
                var alredyRegistedBooks = _dataBaseService.Books
                .Where(p => p.m_no == booksModel.m_no && p.registDate._registDate == DataTimeChangeToDataBaseFormat);
                //既に登録されている日は更新する。未登録の日は新規登録する。
                if (alredyRegistedBooks.Any())
                {
                    var books = alredyRegistedBooks.First();

                    books.amountUsed = booksModel.amountUsed;
                    books.registDate = SdpFactory.ValueObjectFactory().CreateRegistDateValueObject(booksModel.registDate);
                    books.utime = DateTime.Now;
                }
                else
                {
                    var booksEntity = SdpFactory.EntityFactory().CreateBooksEntity(booksModel.m_no, booksModel.amountUsed, DateTime.Now, new RegistDateValueObject(booksModel.registDate));
                    _dataBaseService.Books.Add(booksEntity);
                }

                _dataBaseService.Save();
            }
            catch (NpgsqlException)
            {
                throw new Exception("データベース接続に失敗しました。");
            }
        }
    }
}
