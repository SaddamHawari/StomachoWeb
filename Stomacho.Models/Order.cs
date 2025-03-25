using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stomacho.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // Foreign key
        public ApplicationUser? User { get; set; }

        public DateTime OrderDate { get; set; }
        public string Status { get; set; }  // "Pending", "Preparing", "Out for Delivery", "Delivered"

        // Navigation property
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
