using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stomacho.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        public int Rating { get; set; }  // 1-5 scale
        public string? Comment { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
