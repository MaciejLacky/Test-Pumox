using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Test_Pumox.Entities
{
    public class Test_PumoxDbContext :DbContext
    {
        //nazwa serwera domyslnie (localdb)\\mssqllocaldb
        private string _connectionString = "Server=DESKTOP-F3NJMD4\\SQLEXPRESS;Database=Test_Pumox;Trusted_Connection=True;";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(p => p.Name)
                .IsRequired();
            modelBuilder.Entity<Company>()
                .Property(p => p.EstablishmentYear)
                .IsRequired();           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);          
        }
        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
