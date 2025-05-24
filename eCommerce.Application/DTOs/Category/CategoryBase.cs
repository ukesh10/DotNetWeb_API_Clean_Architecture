﻿using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Category
{
    public class CategoryBase
    {
        [Required]
        public string? Name { get; set; }
    }
}
