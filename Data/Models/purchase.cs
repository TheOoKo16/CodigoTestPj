using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("purchase")]
    public class purchase
    {
        [Key]
        public int ID { get; set; }
        public int? EvoucherID { get; set; }
        public string Title { get; set; }

        public int? CustomerID { get; set; }
        public string Name { get; set; }
        public string PaymentType { get; set; }
        public string Quantity { get; set; }
       
        public string PromoCode { get; set; }
        public bool? IsUsed { get; set; }
        public decimal? Amount { get; set; }
       



    }
}
