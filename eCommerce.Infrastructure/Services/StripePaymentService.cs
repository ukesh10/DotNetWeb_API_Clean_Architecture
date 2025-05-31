using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities;
using Stripe.Checkout;

namespace eCommerce.Infrastructure.Services
{
    public class StripePaymentService : IPaymentService
    {
        public async Task<ServiceResponse> 
            Pay(decimal totalAmount, IEnumerable<Product> cartProducts, IEnumerable<ProcessCart> carts)
        {
            try
            {
                var lineItems = new List<SessionLineItemOptions>();
                foreach (var item in cartProducts)
                {
                    var pQuantity = carts.FirstOrDefault(c => c.ProductId == item.Id);
                    lineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Name,
                                Description = item.Description
                            },
                            UnitAmount = (long)(item.Price * 100), // Stripe expects amount in cents
                        },
                        Quantity = pQuantity!.Quantity
                    });
                }

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = ["usd"],
                    LineItems = lineItems,
                    Mode = "payment",
                    SuccessUrl = "https://localhost:7025/payment-success",
                    CancelUrl = "https://localhost:7025/payment-cancel"
                };

                var service = new SessionService();
                Session session = await service.CreateAsync(options);
                return new ServiceResponse(true, session.Url);
            }catch(Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
            
        }
    }
}
