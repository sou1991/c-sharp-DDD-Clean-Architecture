using Entities;
using System;
using System.Linq;

namespace Infrastructure.Books
{
    public interface IBooksRepositorySS
    {
        BooksEntity Find(string targetID, DateTime targetDate, bool isRegist = false);

        IQueryable<BooksEntity> FindSingle();

        void Create(BooksEntity memberEntity);

        void Save();
    }
}
