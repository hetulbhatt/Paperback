using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
    public class Book
    {
        [Required]
        public string bookID { get; set; }
        [Required]
        [Display(Name ="Book Name")]
        public string bookName { get; set; }
        [Required]
        [Display(Name ="Author")]
        public string author { get; set; }
        [Required]
        public string publisher { get; set; }
        [Required]
        public string language { get; set; }
        [Required]
        public long isbn { get; set; }
        [Required]
        public int pages { get; set; }
        [Required,StringLength(1000,ErrorMessage ="Description should be of maximum 1000 characters.")]
        public string description { get; set; }
        [Required,StringLength(4,ErrorMessage ="Enter valid year.")]
        [Display(Name ="Year of Publication")]
        public string yearofPublication { get ; set; }
        [Required]
        public float rent { get; set; }
        [Required,Display(Name ="Upload Image"),DataType(DataType.Upload)]
        public string imgurl { get; set; }
        [Required, Display(Name = "Upload pdf"), DataType(DataType.Upload)]
        public string pdfurl { get; set; }
        [Required]
        public string categoryID { get; set; }

        [ForeignKey("categoryID")]
        public Category Category { get; set; }

    }
}