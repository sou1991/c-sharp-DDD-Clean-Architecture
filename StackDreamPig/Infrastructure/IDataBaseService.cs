using Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public interface IDataBaseService
    {
        DbSet<MemberEntity> Member { get; set; }

        DbSet<BooksEntity> Books { get; set; }
        void Save();
    }
}
