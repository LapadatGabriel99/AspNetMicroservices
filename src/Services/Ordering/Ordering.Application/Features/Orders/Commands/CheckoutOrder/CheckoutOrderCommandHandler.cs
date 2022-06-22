using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Internal;
using Ordering.Application.Internal.Contracts;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;

        private readonly IEmailService _emailService;

        private readonly ILogger<CheckoutOrderCommandHandler> _logger;
        private readonly IEmailBuilder _emailBuilder;

        public CheckoutOrderCommandHandler(
            IOrderRepository orderRepository,
            IMapper mapper, 
            IEmailService emailService, 
            ILogger<CheckoutOrderCommandHandler> logger,
            IEmailBuilder emailBuilder)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
            _emailBuilder = emailBuilder;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var result = await AddOrderAsync(request);

            _logger.LogInformation($"Order {result.Id} is successfully created");

            await SendEmail();

            return result.Id;
        }

        private async Task<Order> AddOrderAsync(CheckoutOrderCommand request)
        {
            var orderEntity = _mapper.Map<Order>(request);

            return await _orderRepository.AddAsync(orderEntity);
        }

        private async Task SendEmail()
        {
            var result = await _emailService.SendEmail(
                _emailBuilder
                .To("lapadatrobert123@gmail.com")
                .Subject("Order Api")
                .Body("Order was created").Create());

            if (!result)
            {
                _logger.LogError("Failed to send order confirmation mail");

                return;
            }

            _logger.LogInformation("Succeeded in sending order confirmation mail");
        }
    }
}
