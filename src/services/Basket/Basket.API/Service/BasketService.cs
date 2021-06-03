using AutoMapper;
using Basket.API.Entity;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, ILogger<BasketService> logger, DiscountGrpcService discountGrpcService, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _logger = logger;
            _discountGrpcService = discountGrpcService;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        public async Task<bool> CheckoutAsync(BasketCheckout dto)
        {
            //check basket
            var basket = await _basketRepository.GetBasketAsync(dto.UserName);
            if (basket == null)
                return false;
            //convert dto to event model
            var eventmessage = _mapper.Map<BasketCheckoutEvent>(dto);
            eventmessage.TotalPrice = basket.TotalPrice;
            //publish event
            await _publishEndpoint.Publish(eventmessage);

            //delete basket
            await _basketRepository.DeketeBasketAsync(dto.UserName);
            return true;
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
            foreach (var item in dto.Items)
            {
                var coupon = await _discountGrpcService.GetDiscountAsync(item.ProductName);
                item.Price -= coupon.Amount;
            }
            return await _basketRepository.UpdateBasketAsync(dto);
        }
    }
}
