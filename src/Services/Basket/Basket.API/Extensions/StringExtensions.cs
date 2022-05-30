using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string content)
        {
            return string.IsNullOrEmpty(content) || string.IsNullOrEmpty(content);
        }
    }
}
