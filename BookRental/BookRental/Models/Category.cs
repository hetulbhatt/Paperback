using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
    public class Category
    {
        [Required]
        public string categoryID { get; set; }
        [Required, StringLength(35, ErrorMessage = "Category Name should be of maximum 35 characters.")]
        public string categoryName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}