using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
    public class Booking
    {
        [Required]
        public long bookingID { get; set; }
        [Required]
        public long transactionID { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string bookID { get; set; }
        [Required]
        public float subcriptionfees { get; set; }
        [Required]
        [Display(Name ="For Months")]
        public int subscriptionPeriod { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime subscriptionDate { get; set; }
        [Required]
        [Display(Name = "Expires on")]
        [DataType(DataType.Date)]
        public DateTime subscriptionexpiryDate { get; set; }

        [ForeignKey("transactionID")]
        public Transaction transaction { get; set; }
        [ForeignKey("bookID")]
        public Book book { get; set; }
    }
}