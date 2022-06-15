using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly MongoClient _mongoClient;

        private readonly ILogger<CatalogContext> _logger;

        public CatalogContext(IConfiguration configuration, ILogger<CatalogContext> logger)
        {
            _logger = logger;
            _logger.LogInformation("Connection string: " + configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            _mongoClient = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = _mongoClient.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeeder.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; set; }
    }
}
