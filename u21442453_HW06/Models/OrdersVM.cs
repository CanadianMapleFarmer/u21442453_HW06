using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21442453_HW06.Models
{
    public class OrdersVM
    {
        public products products { get; set; }
        public orders orders { get; set; }
        public order_items order_Items { get; set; }
        public stocks stocks { get; set; }
    }
}