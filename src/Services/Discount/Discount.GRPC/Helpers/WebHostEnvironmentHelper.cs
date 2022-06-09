using Discount.GRPC.Helpers.Contracts;
using Discount.GRPC.Helpers.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.GRPC.Helpers
{
    internal sealed class WebHostEnvironmentHelper : IWebHostEnvironmentHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WebHostEnvironmentHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public WebHostEnvironmentEnum GetWebHostEnvironmentType()
        {
            if (_webHostEnvironment.IsProduction())
            {
                return WebHostEnvironmentEnum.Production;
            }

            return WebHostEnvironmentEnum.Development;
        }
    }
}
