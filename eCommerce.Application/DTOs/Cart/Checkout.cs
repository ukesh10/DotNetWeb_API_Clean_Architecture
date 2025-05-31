using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Cart
{
    public class Checkout
    {
        [Required]
        public required Guid PaymentMethodId { get; set; }
        [Required]
        public required IEnumerable<ProcessCart> Carts { get; set; }
    }
}
