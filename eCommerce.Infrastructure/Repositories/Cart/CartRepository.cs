using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Infrastructure.Data;

namespace eCommerce.Infrastructure.Repositories.Cart
{
    public class CartRepository(AppDbContext context) : ICart
    {
        public async Task<int> SaveCheckoutHistory(IEnumerable<Achieve> checkouts)
        {
            context.CheckoutAchieves.AddRange(checkouts);
            return await context.SaveChangesAsync();
        }
    }
}
