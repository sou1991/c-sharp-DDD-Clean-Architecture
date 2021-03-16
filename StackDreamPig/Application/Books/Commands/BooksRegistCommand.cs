using Infrastructure;
using stackDreamPig.Models.Book.Query;
using System;
using Valueobject.Books;
using System.Linq;
using Npgsql;
using Factory;
using Infrastructure.Books;
using Application.Books.Model;

namespace Application.Books.Commands
{
    public class BooksRegistCommand : IBooksRegistCommand
    {
        private IBooksRepository _booksRepository;

        public BooksRegistCommand(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }
        public void Execute(IBooksDTO booksModel)
        {
            if (string.IsNullOrEmpty(booksModel.m_no)) throw new ArgumentNullException(null, "セッションが切れました。再度ログインしてください。");

            var DataTimeChangeToDataBaseFormat = booksModel.registDate;

            try
            {
                var alredyRegistedBooks = _booksRepository.FindSingle(booksModel.m_no, DataTimeChangeToDataBaseFormat);
                //既に登録されている日は更新する。未登録の日は新規登録する。
                if (alredyRegistedBooks != null)
                {
                    alredyRegistedBooks.amountUsed = booksModel.amountUsed;
                    alredyRegistedBooks.registDate = SdpFactory.ValueObjectFactory().CreateRegistDateValueObject(booksModel.registDate);
                    alredyRegistedBooks.utime = DateTime.Now;
                }
                else
                {
                    var booksEntity = SdpFactory.EntityFactory().CreateBooksEntity(booksModel.m_no, booksModel.amountUsed, DateTime.Now, new RegistDateValueObject(booksModel.registDate));
                    _booksRepository.Create(booksEntity);
                }

                _booksRepository.Save();
            }
            catch (NpgsqlException)
            {
                throw new Exception("データベース接続に失敗しました。");
            }
        }
    }
}
