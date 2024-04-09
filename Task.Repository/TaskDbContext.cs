using Microsoft.EntityFrameworkCore;
using Task.DTO;

namespace Task.Repository
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext()
        {

        }
        public TaskDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().Property(c => c.Name).HasColumnType("nvarchar(50)").IsRequired(true);
            modelBuilder.Entity<City>().HasIndex(c => c.Name).IsUnique(true);
            modelBuilder.Entity<City>().Property(c => c.CreateDate).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<City>().Property(c => c.IsDeleted).HasColumnType("bit");
            modelBuilder.Entity<City>().HasMany(u => u.Users).WithOne(c => c.City);

            modelBuilder.Entity<User>().Property(u => u.FirstName).HasColumnType("nvarchar").HasMaxLength(50).IsRequired(true).HasAnnotation("MinLength", 2);
            modelBuilder.Entity<User>().Property(u => u.LastName).HasColumnType("nvarchar").IsRequired(true).HasMaxLength(50).HasAnnotation("MinLength", 2);
            modelBuilder.Entity<User>().Property(u => u.IdNumber).HasColumnType("nvarchar(11)").IsRequired(true);
            modelBuilder.Entity<User>().Property(u => u.BirthDate).IsRequired(true).HasColumnType("date").HasAnnotation("CheckConstraint", "DateOfBirth <= DateAdd(Year, -18, GetDate())");
            modelBuilder.Entity<User>().Property(u => u.Photo).HasColumnType("nvarchar");
            modelBuilder.Entity<User>().HasMany(p => p.PhoneNumbers).WithOne(u => u.User);
            modelBuilder.Entity<User>().Property(u => u.CreateDate).IsRequired(true).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<User>().Property(u => u.IsDeleted).IsRequired(true).HasColumnType("Bit");

            modelBuilder.Entity<Relation>().Property(r => r.RelationType).IsRequired(true);
            modelBuilder.Entity<Relation>().Property(p => p.CreateDate).IsRequired(true).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Relation>().Property(p => p.IsDeleted).IsRequired(true).HasColumnType("Bit");
            modelBuilder.Entity<Relation>().HasOne(r => r.FromUser).WithMany(p => p.RelationsFrom).HasForeignKey(r => r.FromId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Relation>().HasOne(r => r.ToUser).WithMany(p => p.RelationsTo).HasForeignKey(r => r.ToId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PhoneNumber>().Property(p => p.Number).HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50).HasAnnotation("MinLength", 4);
            modelBuilder.Entity<PhoneNumber>().Property(p => p.IsDeleted).HasColumnType("bit");
            modelBuilder.Entity<PhoneNumber>().Property(p => p.CreateDate).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<PhoneNumber>().HasOne(p => p.User).WithMany(p => p.PhoneNumbers);
        }
    }
}