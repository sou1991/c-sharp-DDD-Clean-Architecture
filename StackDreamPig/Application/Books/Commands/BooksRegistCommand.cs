using Entities;
using Infrastructure;
using stackDreamPig.Models.Book.Query;
using System;
using System.Collections.Generic;
using System.Text;
using Valueobject.Books;
using System.Linq;

namespace Application.Books.Commands
{
    public class BooksRegistCommand : IBooksRegistCommand
    {
        private IDataBaseService _dataBaseService;
        private readonly int deleteSecondIndexNumber = 10;

        public BooksRegistCommand(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }
        public void Execute(BooksModel booksModel)
        {
            var DataTimeChangeToDataBaseFormat = booksModel.registDate.ToString().Replace("/", "-").Remove(deleteSecondIndexNumber);

            var alredyRegistedBooks = _dataBaseService.Books
            .Where(p => p.m_no == booksModel.m_no && p.registDate._registDate.ToString().Contains(DataTimeChangeToDataBaseFormat));

            if(alredyRegistedBooks.Count() == 0)
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
                var hasBooks = _dataBaseService.Books
                .Where(p => p.m_no == booksModel.m_no && p.registDate._registDate.ToString().Contains(DataTimeChangeToDataBaseFormat)).First();

                hasBooks.amountUsed = booksModel.amountUsed;
                hasBooks.registDate = new RegistDateValueObject(booksModel.registDate);
                hasBooks.utime = DateTime.Now;
            }

            _dataBaseService.Save();
        }
    }
}
