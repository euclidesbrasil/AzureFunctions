using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Payment.AzureFunction.Shared
{
    public class OrderModel
    {
        public string OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public decimal Amount { get; set; }
    }
}
