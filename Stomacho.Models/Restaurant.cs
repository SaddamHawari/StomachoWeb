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
        [Display(Name = "Restaurant Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Location { get; set; }

        [Required]
        [MaxLength(50)]
        public string Cuisine { get; set; } // Example: Italian, Chinese, Indian, etc.

        public string? ImageUrl { get; set; } // Store restaurant images

        public bool IsActive { get; set; } = true; // Active or Inactive status

        public ICollection<MenuItem>? MenuItems { get; set; }

        public ICollection<Review>? Reviews { get; set; }
    }
}
