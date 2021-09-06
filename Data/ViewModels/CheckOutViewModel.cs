using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.ViewModels
{
   public class CheckOutViewModel
    {
        public evoucher evoucher { get; set; }
        public customer customer { get; set; }
        public List<purchase> purchase { get; set; }

        public string secretkey { get; set; }
    }
}
