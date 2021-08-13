using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentMgt.Models
{
    public class BookDetail
    {
        public int BookDetailId { get; set; }
        public int BookId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime BookDate { get; set; }
        public decimal UnitPrice { get; set; }
        public string Creator { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public virtual Item Item { get; set; }
        public virtual Book Book { get; set; }
    }
}