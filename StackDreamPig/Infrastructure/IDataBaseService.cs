using Entities;
using Entities.Books;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public interface IDataBaseService
    {
        DbSet<MemberEntity> Member { get; set; }

        DbSet<BooksData> Books { get; set; }
        void Save();
    }
}
