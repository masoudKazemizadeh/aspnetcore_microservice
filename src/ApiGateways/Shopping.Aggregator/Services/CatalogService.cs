using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client;
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var response = await _client.GetAsync($"/api/catalog/{id}");
            return await response.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var response = await _client.GetAsync($"/api/catalog");
            return await response.ReadContentAs<IEnumerable<CatalogModel>>();
        }

        public async Task<CatalogModel> GetProductByCategory(string categoryName)
        {
            var response = await _client.GetAsync($"/api/catalog/GetProductByCategoryName/{categoryName}");
            return await response.ReadContentAs<CatalogModel>();
        }
    }
}
