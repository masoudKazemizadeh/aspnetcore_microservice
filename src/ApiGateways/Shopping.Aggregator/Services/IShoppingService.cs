﻿using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public interface IShoppingService
    {
        Task<ShoppingModel> GetShoppingItems(string username);
    }
}
