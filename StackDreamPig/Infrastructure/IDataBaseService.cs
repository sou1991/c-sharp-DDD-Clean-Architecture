using Entities;
using Entities.Books;
using Entities.Member;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public interface IDataBaseService
    {
        DbSet<MemberData> Member { get; set; }

        DbSet<BooksData> Books { get; set; }
        void Save();
    }
}
