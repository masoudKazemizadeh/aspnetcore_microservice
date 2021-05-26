
using AutoMapper;
using Discount.Grpc.Entity;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var result = await _discountRepository.GetDiscountAsync(request.ProductName);
            if (result == null)
                throw new RpcException(new Status(StatusCode.NotFound, "This product i'nt exsist"));

            return _mapper.Map<CouponModel>(result);
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            if (request == null)
                throw new RpcException(new Status(StatusCode.Unimplemented, "request body isn't acceptable"));
            var result = await _discountRepository.CreateDiscountAsync(_mapper.Map<Coupon>(request.Coupon));
            if (result)
                return request.Coupon;
            throw new RpcException(new Status(StatusCode.Aborted, "it didn't create"));
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var tempResult = await _discountRepository.DeleteDiscountAsync(request.ProductName);
            var result = new DeleteDiscountResponse { Success = tempResult};
            return result;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            if (request == null)
                throw new RpcException(new Status(StatusCode.Unimplemented, "request body isn't acceptable"));
            var result = await _discountRepository.UpdateDiscountAsync(_mapper.Map<Coupon>(request.Coupon));
            if (result)
                return request.Coupon;
            throw new RpcException(new Status(StatusCode.Aborted, "it didn't update"));
        }

    }
}
