using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("customer")]
    public class customer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public int? Quantity { get; set; }
        public string BuyType { get; set; }

        public int? EvoucherID { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? Discount { get; set; }
        public string PaymentMethod { get; set; }


    }
}
