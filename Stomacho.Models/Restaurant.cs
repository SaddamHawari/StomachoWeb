using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stomacho.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Location { get; set; }

        [Required]
        [MaxLength(50)]
        public string Cuisine { get; set; } // Example: Italian, Chinese, Indian, etc.

        [Range(0, 5)]
        public double Rating { get; set; } // Average rating (calculated from reviews)

        [Required]
        public string ImageUrl { get; set; } // Store restaurant images

        public bool IsActive { get; set; } = true; // Active or Inactive status
    }
}
