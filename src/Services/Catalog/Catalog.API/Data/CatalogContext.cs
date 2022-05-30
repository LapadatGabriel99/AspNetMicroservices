using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
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

        public CatalogContext(IConfiguration configuration)
        {
            _mongoClient = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = _mongoClient.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeeder.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; set; }
    }
}
