using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class ShoppingService : IShoppingService
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public ShoppingService(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<ShoppingModel> GetShoppingItems(string username)
        {
            var basket = await _basketService.GetBasketItemWithProduct(username);
            foreach (var item in basket.Items)
            {
                var product = await _catalogService.GetCatalog(item.ProductId);
                if (product != null)
                {
                    item.Summary = product.Summary;
                    item.Description = product.Description;
                    item.ImageFile = product.ImageFile;
                    item.Category = product.Category;
                }
            }
            var orders = await _orderService.GetOrdersByUsername(username);
            var result = new ShoppingModel { Username = username, BasketWithProduct = basket, Orders = orders };
            return result;
        }
    }
}
