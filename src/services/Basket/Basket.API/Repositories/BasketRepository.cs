using Basket.API.Entity;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCash;

        public BasketRepository(IDistributedCache redisCash)
        {
            _redisCash = redisCash;
        }

        public async Task DeketeBasketAsync(string username)
        {
            await _redisCash.RemoveAsync(username);
        }

        public async Task<ShoppingCart> GetBasketAsync(string username)
        {
            var result = await _redisCash.GetStringAsync(username);
            if (string.IsNullOrEmpty(result))
                return null;
            return JsonConvert.DeserializeObject<ShoppingCart>(result);
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart model)
        {
            await _redisCash.SetStringAsync(model.UserName, JsonConvert.SerializeObject(model));
            return await GetBasketAsync(model.UserName);
        }
    }
}
