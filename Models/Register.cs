using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RentMgt.Models
{

    public class Register
    {
        [ScaffoldColumn(false)]
        [Key]
        public int UserId { get; set; }
        [DisplayName("Username")]
        public string Username { get; set; }
        //public URole Role { get; set; }
        [DisplayName("Role")]
        public string Role { get; set; }
        [DisplayName("Phone Number")]
        public string Phone { get; set; }
        public string Email { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("State")]
        public string State { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
        [StringLength(160)]
        public string Password { get; set; }
        
    }

    //public enum URole
    //{
    //    Buyer,
    //    Seller
    //}
}