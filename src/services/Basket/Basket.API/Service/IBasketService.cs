using Basket.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Service
{
    public interface IBasketService
    {
        Task<ShoppingCart> GetBasketAsync(string username);
        Task<ShoppingCart> UpdateBasketAsync(ShoppingCart dto);
        Task DeleteBasketAsync(string userName);
    }
}
