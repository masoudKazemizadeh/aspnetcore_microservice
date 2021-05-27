using Discount.Grpc.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;
        private readonly ILogger<DiscountGrpcService> _logger;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService, ILogger<DiscountGrpcService> logger)
        {
            _discountProtoService = discountProtoService;
            _logger = logger;
        }

        public async Task<CouponModel> GetDiscountAsync(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
        
    }
}
