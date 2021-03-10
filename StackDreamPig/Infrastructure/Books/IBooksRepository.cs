using Entities;
using System;
using System.Linq;

namespace Infrastructure.Books
{
    public interface IBooksRepository
    {
        BooksEntity FindSingle(string targetID, DateTime targetDate);

        IQueryable<BooksEntity> Find(string targetID,int year,int month);

        void Create(BooksEntity memberEntity);

        void Save();
    }
}
