using CloudComputingPT.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudComputingPT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DriverService> DriverServices { get; set; }
        public DbSet<BookingDetails> BookingDetails { get; set; }
        public DbSet<PricesDictionary> PricesDictionary { get; set; }
        public DbSet<Categories> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DriverService>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<BookingDetails>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Categories>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
