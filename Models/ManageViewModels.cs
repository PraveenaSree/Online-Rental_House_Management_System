using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace RentMgt.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }  
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
  
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class EditViewModel
    {
        [Required]
        [Display(Name = "New Username")]
        public string NewUsername { get; set; }

        [Required]
        [Display(Name = "New Phone Number")]
        public string NewPhone { get; set; }

        [Required]
        [Display(Name = "New Address")]
        public string NewAddress { get; set; }

        [Required]
        [Display(Name = "New City")]
        public string NewCity { get; set; }

        [Required]
        [Display(Name = "New State")]
        public string NewState { get; set; }

        [Required]
        [Display(Name = "New Postal Code")]
        public string NewPostalCode { get; set; }

        [Required]
        [Display(Name = "New Country")]
        public string NewCountry { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    
}