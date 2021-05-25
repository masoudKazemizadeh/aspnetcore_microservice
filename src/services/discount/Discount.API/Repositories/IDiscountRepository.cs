using Discount.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscountAsync(string productName);
        Task<bool> CreateDiscountAsync(Coupon model);
        Task<bool> UpdateDiscountAsync(Coupon model);
        Task<bool> DeleteDiscountAsync(string productName);
    }
}
