using Entities;
using Entities.Books;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public class DataBaseService : DbContext, IDataBaseService
    {
        public DataBaseService(DbContextOptions<DataBaseService> options)
        : base(options)
        {
        }

        public DbSet<MemberEntity> Member { get; set; }

        public DbSet<BooksData> Books { get; set; }

        public void Save()
        {
            this.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberEntity>(entity =>
            {
                entity.ToTable("member");

                entity.HasKey(e => e.m_no);

                entity.Property(e => e.m_no)
                .HasColumnName("m_no");

                entity.OwnsOne(e => e.memberValueObject, a => a.WithOwner());

                entity.OwnsOne(e => e.amountValueObject, a => a.WithOwner());

                entity.OwnsOne(e => e.amountLimitValueObject, a => a.WithOwner());

            });
            modelBuilder.Entity<BooksData>(entity =>
            {
                entity.ToTable("books");

                entity.HasKey(e => e.id);

                entity.Property(e => e.m_no)
                .HasColumnName("id");

                entity.Property(e => e.m_no)
                .HasColumnName("m_no");

                entity.Property(e => e.amountUsed)
                .HasColumnName("amount_used");

                entity.Property(e => e.intime)
                .HasColumnName("intime");

                entity.Property(e => e.utime)
                .HasColumnName("utime");

                entity.Property(e => e.registDate)
                .HasColumnName("regist_date");

            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
