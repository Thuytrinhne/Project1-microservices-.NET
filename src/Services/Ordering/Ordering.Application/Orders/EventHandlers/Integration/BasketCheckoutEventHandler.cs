using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public  class BasketCheckoutEventHandler
        (ISender sender, ILogger<BasketCheckoutEvent> logger)
        : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            // Todo: Create new order and start order fullfillment process
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
            
            // context.Message means Basket checkout event object
            var command = MapToCreateOrderCommand(context.Message);
           await  sender.Send(command);
        }
        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            // Create full order with incoming event data
            var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
            var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
            var orderId = Guid.NewGuid();

            var orderDto = new OrderDto(
                Id: orderId,
                CustomerId: message.CustomerId,
                OrderName: "ORDER12",
                ShippingAddress: addressDto,
                BillingAddress: addressDto,
                Payment: paymentDto,
                Status: Ordering.Domain.Enums.OrderStatus.Pending,
                OrderItems:
                [
                    new OrderItemDto(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500),
                    new OrderItemDto(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400)
                ]);
            // here should get this product information from the incoming request message from RabbitMQ 
            return new CreateOrderCommand(orderDto);
        }
    }
}
