using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System;
namespace wedding_planner.Models
{
    public class WeddingViews{
        [Required]
        [MinLength(4)]
        [DataType(DataType.Text)]
        
        public string WedderOne{get;set;}

        [Required]
        [MinLength(4)]
        [DataType(DataType.Text)]
        
        public string WedderTwo{get;set;}

        [Required]
        [CheckFuture]
        [DataType(DataType.Date)]

        public DateTime Date{get;set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Text)]
        public string Address{get;set;}

    }
}