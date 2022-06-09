using Discount.GRPC.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.GRPC.Helpers.Contracts
{
    public interface IWebHostEnvironmentHelper
    {
        WebHostEnvironmentEnum GetWebHostEnvironmentType();
    }
}
