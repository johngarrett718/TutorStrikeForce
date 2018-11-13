using Microsoft.EntityFrameworkCore;
using System.Linq;
using TutorStrikeForce.Models;

namespace TutorStrikeForce.EF
{
    public class TutorStrikeForceContext : DbContext
    {
        public TutorStrikeForceContext(DbContextOptions<TutorStrikeForceContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesRep> SalesReps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal)))
            {
                property.Relational().ColumnType = "decimal(6, 2)";
            }

            modelBuilder.Entity<SalesRep>().ToTable("salesrep").HasData(SeedData.SeedSalesReps());
            modelBuilder.Entity<Client>().ToTable("client").HasData(SeedData.SeedClients());
            modelBuilder.Entity<Sale>().ToTable("sale").HasData(SeedData.SeedSales());
        }
    }
}