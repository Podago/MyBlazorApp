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
        public int ID { get; set; }
        public string? Number { get; set; }
        //public DateTime CreateDate { get; set; }
        //public DateTime UpdateDate { get; set; }
        //public DateOnly OrderDate { get; set; }
        //public List<Client> Client { get; set; }
        //public List<Address> Address { get; set; }
        //public List<Ware> Wares { get; set; }
        //public string CarNumber { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string? Note { get; set; }
        public string? ImgUrl { get; set; }
    }
}
