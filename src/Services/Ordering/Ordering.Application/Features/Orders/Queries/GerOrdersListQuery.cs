using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries
{
    public class GerOrdersListQuery : IRequest<List<OrderDto>>
    {
        public string UserName { get; set; }

        public GerOrdersListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
