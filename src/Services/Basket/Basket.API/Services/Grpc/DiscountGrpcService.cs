using Discount.GRPC.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Discount.GRPC.Protos.DiscountProtoService;

namespace Basket.API.Services.Grpc
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var getDiscountRequest = new GetDiscountRequest { ProductName = productName };

            return await _discountProtoServiceClient.GetDiscountAsync(getDiscountRequest);
        }
    }
}
