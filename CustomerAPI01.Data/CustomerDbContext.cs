using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CustomerAPI01.Data
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.EmailAddress)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerId)
                .HasValueGenerator<GuidValueGenerator>();

            modelBuilder.Entity<PhoneNumber>()
                .HasIndex(p => p.PhoneNumberId);

            modelBuilder.Entity<PhoneNumber>()
                .HasIndex(p => new { p.CustomerId, p.Number })
                .IsUnique();

            modelBuilder.Entity<PhoneNumber>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.PhoneNumbers)
                .HasForeignKey(p => p.CustomerId);
        }
    }
}
