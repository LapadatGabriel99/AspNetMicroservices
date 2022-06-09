using AutoMapper;
using Discount.GRPC.Entities;
using Discount.GRPC.Protos;
using Discount.GRPC.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Discount.GRPC.Protos.DiscountProtoService;

namespace Discount.GRPC.Services
{
    public class DiscountService : DiscountProtoServiceBase
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

        public override async Task<GetDiscountResponse> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDisount(request.ProductName);

            if (coupon == null)
            {
                _logger.LogError($"Discount with ProductName = {request.ProductName} not found");

                return new GetDiscountResponse { Success = false, Coupon = null };
            }

            var couponModel = _mapper.Map<CouponModel>(coupon);

            return new GetDiscountResponse { Success = true, Coupon = couponModel };
        }

        public override async Task<CreateDiscountResponse> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            var result = await _discountRepository.CreateDiscount(coupon);

            if (result)
            {
                var createdCoupon = _discountRepository.GetDisount(coupon.ProductName);

                var couponModel = _mapper.Map<CouponModel>(createdCoupon);

                return new CreateDiscountResponse { Success = result, Coupon = couponModel };
            }

            return new CreateDiscountResponse { Success = result };
        }

        public override async Task<UpdateDiscountResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            var result = await _discountRepository.UpdateDiscount(coupon);

            if (result)
            {
                var updatedCoupon = _discountRepository.GetDisount(coupon.ProductName);

                var couponModel = _mapper.Map<CouponModel>(updatedCoupon);

                return new UpdateDiscountResponse { Success = result, Coupon = couponModel };
            }

            return new UpdateDiscountResponse { Success = result, Coupon = null };
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var result = await _discountRepository.DeleteDiscount(request.ProductName);

            return new DeleteDiscountResponse { Success = result };
        }
    }
}
