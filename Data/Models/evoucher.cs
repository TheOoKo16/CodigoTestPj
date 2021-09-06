using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Text;

namespace Data.Models
{
    [Table("evoucher")]
    public class evoucher
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Image { get; set; }
        public decimal? Amount { get; set; }
       
      
        public int? Quantity { get; set; }
        public string BuyType { get; set; }
        public bool? IsActive { get; set; }
       
        public int? Qtylimit { get; set; }

      
        public string PhotoUrl
        {
            get
            {
                if (this.Image != null)
                {
                
                    return string.Format(" https://localhost:44324/Evoucher/{0}", Image);
                }
                else
                {
                    return "https://zettabyteplus.net/themes/img/blackplaceholder.png";
                }
            }
        }

    }
}
