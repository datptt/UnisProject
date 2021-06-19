using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql;
using Unis.Domain;

namespace Unis.Repository
{

    public class RepositoryContext : DbContext
    {

        private readonly IConfiguration configuration;

        public DbSet<User> Users { get; set; }


        public RepositoryContext(DbContextOptions<RepositoryContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = configuration.GetSection("ConnectionString:DBUnis").Value;
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}
