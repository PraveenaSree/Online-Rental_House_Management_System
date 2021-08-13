using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RentMgt.Models
{
    
    public class Item
    {
        [ScaffoldColumn(false)]
        public int ItemId { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [DisplayName("Producer")]
        public int ProducerId { get; set; }
        [Required(ErrorMessage ="A property title is required")]
        [StringLength(160)]
        public string Title { get; set; }
        [StringLength(160)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0.1,1000000000000000000, ErrorMessage ="Price must be greater than 0")]
        public decimal Price { get; set; }
        [DisplayName("Item item URL")]
        [StringLength(1024)]
        public string ItemArtUrl { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Features")]
        public string Features { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }        
        [DisplayName("Area")]
        public string Area { get; set; }        
        [DisplayName("Total Floors")]
        public string TotalFloor { get; set; }
        [DisplayName("Type of Flooring")]
        public string Flooring { get; set; }
        [DisplayName("Property Age")]
        public string PropertyAge { get; set; }
        public virtual Category Category { get; set; }
        public virtual Producer Producer { get; set; }
    }
}