using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportStore.API.Models
{
    public class ProductVM
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string BriefDescription { get; set; }
        [Required, StringLength(500)]
        public string Description { get; set; }
        public string Picture { get; set; }
        public int Stocklevel { get; set; }
        [Required, StringLength(30)]
        public string Category { get; set; }
    }
}