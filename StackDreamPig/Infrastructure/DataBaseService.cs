using Entities;
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

        public DbSet<BooksEntity> Books { get; set; }

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

                entity.Property(e => e.password)
                .HasColumnName("password");
        
                entity.Property(e => e.monthlyIncome)
                .HasColumnName("monthlyIncome");

                entity.Property(e => e.fixedCost)
                .HasColumnName("fixedCost");

                entity.OwnsOne(e => e.amountLimit, a => a.WithOwner());

                entity.Property(e => e.saltPassword)
                .HasColumnName("saltPassword");

            });
            modelBuilder.Entity<BooksEntity>(entity =>
            {
                entity.ToTable("books");

                entity.Property(e => e.m_no)
                .HasColumnName("m_no");

                entity.Property(e => e.amountUsed)
                .HasColumnName("amountUsed");

                entity.Property(e => e.intime)
                .HasColumnName("intime");

                entity.Property(e => e.utime)
                .HasColumnName("utime");

                entity.OwnsOne(e => e.registDate, a => a.WithOwner());

            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
