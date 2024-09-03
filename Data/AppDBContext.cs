using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TelerikUI.Models;

namespace TelerikUI.Data
{
    public class AppDBContext : DbContext
    {
        // Represents the table in the database
        public DbSet<ModtblGenerator> Generators { get; set; }
        public DbSet<ModvGeneratorWasteClass> GeneratorWasteClasses { get; set; }

        protected readonly IConfiguration Configuration;

        // Constructor
        public AppDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DBConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map the ModGenerator class to the tblGenerators table
            modelBuilder.Entity<ModtblGenerator>().ToTable("tblGenerators");
            modelBuilder.Entity<ModvGeneratorWasteClass>().ToView("vGeneratorWasteClasses").HasNoKey(); 
        }
    }
}
