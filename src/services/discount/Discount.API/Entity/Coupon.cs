using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Entity
{
    public class Coupon
    {
        public int Id { get; set; } = 0;
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; } = 0;
    }
}
