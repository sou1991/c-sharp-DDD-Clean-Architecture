using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Books
{
    public class BooksDBServiceRepository : IBooksRepository
    {
        private IDataBaseService _dataBaseService;

        public BooksDBServiceRepository(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public void Create(BooksEntity memberEntity)
        {
            _dataBaseService.Books.Add(memberEntity);
        }



        public void Save()
        {
            _dataBaseService.Save();
        }

        public BooksEntity FindSingle(string targetID, DateTime targetDate)
        {
            var booksEntity = _dataBaseService.Books
                              .Where(p => p.m_no == targetID && p.registDate._registDate == targetDate)
                              .FirstOrDefault();

            return booksEntity;
        }

        public IQueryable<BooksEntity> Find(string targetID, int year, int month)
        {
            var booksEntities = _dataBaseService.Books
                .OrderByDescending(p => p.registDate._registDate)
                .Where(p => p.m_no == targetID
                && p.registDate._registDate.Year == year
                && p.registDate._registDate.Month == month);

            return booksEntities;
        }
    }
}
