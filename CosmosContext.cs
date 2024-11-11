using WidgetCoUser.Models;
using Microsoft.EntityFrameworkCore;

namespace WidgetCoUser
{
    public class CosmosContext : DbContext
    {
        private readonly FunctionConfiguration _config;

        public DbSet<User> Users { get; set; }

        public CosmosContext(FunctionConfiguration config)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<User>()
            .ToContainer("User") //so that it maps to the Users container in Cosmos DB and not to the same one by default if we wanted other dbs
            .HasNoDiscriminator() //no metadata
            .HasPartitionKey(u => u.Id); //partition key is the Id property)
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                accountEndpoint: _config.CosmosAccountEndpoint,
                accountKey: _config.CosmosAccountKey,
                databaseName: _config.CosmosDatabaseName);
        }
    }
}