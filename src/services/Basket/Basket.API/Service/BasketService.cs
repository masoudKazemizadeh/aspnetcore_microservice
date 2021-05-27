using Basket.API.Entity;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Service
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<BasketService> _logger;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketService(IBasketRepository basketRepository, ILogger<BasketService> logger, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _logger = logger;
            _discountGrpcService = discountGrpcService;
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await _basketRepository.DeketeBasketAsync(userName);
            return;
        }

        public async Task<ShoppingCart> GetBasketAsync(string username)
        {
            var result = await _basketRepository.GetBasketAsync(username);
            return result ?? new ShoppingCart(username);
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart dto)
        {
            foreach(var item in dto.Items)
            {
                var coupon = await _discountGrpcService.GetDiscountAsync(item.ProductName);
                item.Price -= coupon.Amount;
            }
            return await _basketRepository.UpdateBasketAsync(dto);
        }
    }
}
