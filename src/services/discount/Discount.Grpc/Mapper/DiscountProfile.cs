﻿using AutoMapper;
using Discount.Grpc.Entity;
using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Mapper
{
    public class DiscountProfile:Profile
    {
        public DiscountProfile()
        {
            CreateMap<CouponModel, Coupon>().ReverseMap();

        }
    }
}
