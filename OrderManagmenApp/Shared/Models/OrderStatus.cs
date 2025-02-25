﻿using System.ComponentModel.DataAnnotations;

namespace OrderManagmen.Shared.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Url { get; set; } = string.Empty;
    }
}
