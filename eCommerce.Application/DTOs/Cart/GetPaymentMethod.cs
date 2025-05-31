using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTOs.Cart
{
    public class GetPaymentMethod
    {
        public required Guid Id { get; set; }
        public string Name { get; set; }
    }
}
