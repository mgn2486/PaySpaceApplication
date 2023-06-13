using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaySpaceDAL.Entities;
using System.Diagnostics;

namespace PaySpaceDAL
{
    public class PaySpaceDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public PaySpaceDbContext(IConfiguration configuration, DbContextOptions<PaySpaceDbContext> options) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("PaySpaceDbConnection"),
                sqlServerOptionsAction: sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure();
                });
        }

        public DbSet<PostalCode> PostalCodes { get; set; }

        public DbSet<TaxName> TaxNames { get; set; }

        public DbSet<TaxRange> TaxRanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxName>()
                        .HasMany(e => e.TaxRanges);

            modelBuilder.Entity<TaxName>()
                        .HasMany(e => e.PostalCodes);

            modelBuilder.Entity<PostalCode>()
                        .HasOne(e => e.TaxName);

            base.OnModelCreating(modelBuilder);
        }
    }
}
