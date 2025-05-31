using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.Cart;

namespace eCommerce.Application.Services.Implementation.Cart
{
    public class CartService(ICart cartInterface, IMapper mapper, IGeneric<Product> productInterface) : ICartService
    {
        public Task<ServiceResponse> Checkout(Checkout checkout, string userId)
        {
             
        }

        public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves)
        {
            var mappedData = mapper.Map<IEnumerable<Achieve>>(achieves);
            var result = await cartInterface.SaveCheckoutHistory(mappedData);
            return result > 0 ? new ServiceResponse(true, "Checkout achieved successfully")
                              : new ServiceResponse(false, "Error occurred in saving");
        }

        private async Task<(IEnumerable<Product>,decimal)> GetTotalAmount(IEnumerable<ProcessCart> carts)
        {
            if(!carts.Any()) return ([], 0);

            var products = await productInterface.GetAllAsync();
            if(!products.Any()) return ([], 0);

            var cartProducts = carts
                .Select(cartItem => products.FirstOrDefault(p => p.Id == cartItem.ProductId))
                .Where(product => product != null)
                .ToList();

            var totalAmount = carts
                .Where(cartItem => cartProducts.Any(p => p.Id == cartItem.ProductId))
                .Sum(cartItem => cartItem.Quantity * cartProducts.First(p => p.Id == cartItem.ProductId)!.Price);

            return (cartProducts!, totalAmount);
        }
    }
}
