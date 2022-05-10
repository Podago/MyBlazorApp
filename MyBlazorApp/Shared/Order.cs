using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlazorApp.Shared
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Number { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Note { get; set; }
        [Required]
        [MaxLength(100)]
        public string? ImgUrl { get; set; }
        public OrderStatus? Status { get; set; }
        public int StatusId { get; set; }
    }
}
