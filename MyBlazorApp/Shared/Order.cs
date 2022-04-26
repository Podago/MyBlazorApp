using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazorApp.Shared
{
    public class Order
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string? Note { get; set; }
        public string? ImgUrl { get; set; }
        public OrderStatus? Status { get; set; }
        public int StatusId { get; set; }
    }
}
