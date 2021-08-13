using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentMgt.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public string Phone { get; set; }
        public int ItemId { get; set; }
        public int Count { get; set; }
        public string Creator { get; set; }
        public string Title { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual Item Item { get; set; }

    }
}