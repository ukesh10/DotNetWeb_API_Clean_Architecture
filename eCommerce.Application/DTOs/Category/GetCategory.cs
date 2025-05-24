using eCommerce.Application.DTOs.Product;

namespace eCommerce.Application.DTOs.Category
{
    public class GetCategory : CategoryBase
    {
        public Guid Id { get; set; }
        public ICollection<GetProduct>? Products { get; set; }
    }
}
