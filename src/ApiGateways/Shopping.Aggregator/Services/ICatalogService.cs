using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public interface ICatalogService
    {
        Task<CatalogModel> GetCatalog(string id);
        Task<CatalogModel> GetProductByCategory(string categoryName);
        Task<IEnumerable<CatalogModel>> GetCatalog();
    }
}
