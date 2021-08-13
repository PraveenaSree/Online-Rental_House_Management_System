using RentMgt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentMgt.ViewModels
{
    public class BookingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}