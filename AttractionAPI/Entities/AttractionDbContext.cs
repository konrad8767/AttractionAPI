using Microsoft.EntityFrameworkCore;

namespace AttractionAPI.Entities
{
    public class AttractionDbContext : DbContext
    {
        private string _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=AttractionDb;Trusted_Connection=True;";
        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attraction>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Address>()
                .Property(x => x.City)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(x => x.Street)
                .IsRequired()
                .HasMaxLength(50);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
