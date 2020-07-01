using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
    public class User
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string fullName { get; set; }
        [Required]
        public string password { get; set; }
        [Required, EmailAddress]
        public string email { get; set; }
        [Required, Phone]
        public string contact { get; set; }
    }
}