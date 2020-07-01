using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
    public class Transaction
    {
        [Required]
        public long TransactionID { get; set; }
        [Required]
        [DataType(DataType.CreditCard)]
        public long cardNumber { get; set; }
        [Required]
        public string cardholderName { get; set; }
        [Required]
        [Range(01,12,ErrorMessage ="Enter valid month.")]
        public int expMonth { get; set; }
        [Required]
        [MinLength(4,ErrorMessage ="Enter valid year.")]
        [MaxLength(4,ErrorMessage ="Enter valid year.")]
        public string expYear { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public int cvv { get; set; }

    }
}