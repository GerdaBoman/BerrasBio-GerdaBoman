#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BerrasBio.Models;

namespace BerrasBio.Data
{
    public class BerrasBioContext : DbContext
    {
        public BerrasBioContext()
        {

        }
        public BerrasBioContext (DbContextOptions<BerrasBioContext> options)
            : base(options)
        {
            

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile("appSettings.json");
                IConfiguration configuration = configurationBuilder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("BerrasBioContext"));
            }
        }

        public DbSet<BerrasBio.Models.Movie> Movie { get; set; }
        public DbSet<BerrasBio.Models.Salon> Salons { get; set; }
        public DbSet<BerrasBio.Models.Showing> Showings { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("Movies");
            modelBuilder.Entity<Salon>().ToTable("Salons");
            modelBuilder.Entity<Showing>().ToTable("Showings");

            base.OnModelCreating(modelBuilder);
        }
    }
}
