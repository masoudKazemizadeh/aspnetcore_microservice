using Basket.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public  interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string username);
        Task<ShoppingCart> UpdateBasketAsync(ShoppingCart model);
        Task DeketeBasketAsync(string username);
    }
}
