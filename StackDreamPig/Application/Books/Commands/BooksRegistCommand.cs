using Entities;
using Infrastructure;
using stackDreamPig.Models.Book.Query;
using System;
using System.Collections.Generic;
using System.Text;
using Valueobject.Books;
using System.Linq;
using Common.Books;

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

            var alredyRegistedBooks = _dataBaseService.Books
            .Where(p => p.m_no == booksModel.m_no && p.registDate._registDate == DataTimeChangeToDataBaseFormat);

            //既に登録されている日は更新する。未登録の日は新規登録する。
            if(alredyRegistedBooks.Any())
            {
                var books = alredyRegistedBooks.First();

                books.amountUsed = booksModel.amountUsed;
                books.registDate = new RegistDateValueObject(booksModel.registDate);
                books.utime = DateTime.Now;
            }
            else
            {
                var booksEntity = new BooksEntity()
                {
                    m_no = booksModel.m_no,
                    amountUsed = booksModel.amountUsed,
                    intime = DateTime.Now,
                    registDate = new RegistDateValueObject(booksModel.registDate)
                };
                _dataBaseService.Books.Add(booksEntity);
            }

            _dataBaseService.Save();
        }
    }
}
