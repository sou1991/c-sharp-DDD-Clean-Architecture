using Entities;
using Entities.Books;
using Entities.Member;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public class DataBaseService : DbContext, IDataBaseService
    {
        public DataBaseService(DbContextOptions<DataBaseService> options)
        : base(options)
        {
        }

        public DbSet<MemberData> Member { get; set; }

        public DbSet<BooksData> Books { get; set; }

        public void Save()
        {
            this.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberData>(entity =>
            {
                entity.ToTable("member");

                entity.HasKey(e => e.m_no);

                entity.Property(e => e.userName)
                .HasColumnName("user_name");

                entity.Property(e => e.password)
                .HasColumnName("password");

                entity.Property(e => e.saltPassword)
                .HasColumnName("salt_password");

                entity.Property(e => e.monthlyIncome)
                .HasColumnName("monthly_income");

                entity.Property(e => e.savings)
                .HasColumnName("savings");

                entity.Property(e => e.fixedCost)
                .HasColumnName("fixed_cost");

                entity.Property(e => e.amountLimit)
                .HasColumnName("amount_limit");

                entity.Property(e => e.intime)
                .HasColumnName("intime");

                entity.Property(e => e.utime)
                .HasColumnName("utime");

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
