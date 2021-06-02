using Entities;
using Entities.Books;
using System;
using System.Linq;

namespace Infrastructure.Books
{
    public interface IBooksRepository
    {
        BooksEntity FindSingle(string targetID, DateTime targetDate);

        IQueryable<BooksEntity> Find(string targetID,int year,int month);

        void Create(BooksEntity memberEntity);

        void Update(BooksEntity memberEntity, BooksDataModelBuilder DataModel);

        void Save();
    }
}
