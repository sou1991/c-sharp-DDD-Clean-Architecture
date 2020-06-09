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

        public virtual DbSet<MemberEntity> Member { get; set; }

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

            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
