using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Stomacho.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; }  // To enable/disable menu items

        // Foreign key
        public int RestaurantId { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public Restaurant? Restaurant { get; set; }
    }
}
