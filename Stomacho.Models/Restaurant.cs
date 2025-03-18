using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
        [MaxLength(200)]
        public string Cuisine { get; set; } // Example: Italian, Chinese, Indian, etc.

        [ValidateNever]
        public string? ImageUrl { get; set; } // Store restaurant images

        public bool IsActive { get; set; } = true; // Active or Inactive status

        [JsonIgnore]
        public ICollection<MenuItem>? MenuItems { get; set; }

        //public ICollection<Review>? Reviews { get; set; }
    }
}
