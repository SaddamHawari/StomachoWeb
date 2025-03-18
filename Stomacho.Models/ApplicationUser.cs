using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stomacho.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        // Navigation property
        public ICollection<Order>? Orders { get; set; }
    }
}
