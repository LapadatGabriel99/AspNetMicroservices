using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries
{
    public class GetOrdersListQueryHandler : IRequestHandler<GerOrdersListQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> Handle(GerOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrderByUserName(request.UserName);
            var ordersDto = _mapper.Map<List<OrderDto>>(orders);

            return ordersDto;
        }
    }
}
