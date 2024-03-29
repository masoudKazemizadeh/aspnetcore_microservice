﻿using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;

        public OrderService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByUsername(string username)
        {
            var response = await _client.GetAsync($"/api/order/{username}");
            return await response.ReadContentAs<IEnumerable<OrderModel>>();
        }
    }
}
