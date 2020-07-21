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
            var DataTimeChangeToDataBaseFormat = booksModel.registDate.ToString().Replace("/", "-").Remove((int)EnumBooks.TIME_AREA_INDEX_NUMBER);

            var alredyRegistedBooks = _dataBaseService.Books
            .Where(p => p.m_no == booksModel.m_no && p.registDate._registDate.ToString().Contains(DataTimeChangeToDataBaseFormat));

            if(alredyRegistedBooks.Count() == (int)EnumBooks.NON_BOOKS)
            {
                var booksEntity = new BooksEntity()
                {
                    m_no = booksModel.m_no,
                    amountUsed = booksModel.amountUsed,
                    intime = DateTime.Now,
                    utime = DateTime.Now,
                    registDate = new RegistDateValueObject(booksModel.registDate)
                };
                _dataBaseService.Books.Add(booksEntity);
            }
            else
            {
                var books = _dataBaseService.Books
                .Where(p => p.m_no == booksModel.m_no && p.registDate._registDate.ToString().Contains(DataTimeChangeToDataBaseFormat)).First();

                books.amountUsed = booksModel.amountUsed;
                books.registDate = new RegistDateValueObject(booksModel.registDate);
                books.utime = DateTime.Now;
            }

            _dataBaseService.Save();
        }
    }
}
