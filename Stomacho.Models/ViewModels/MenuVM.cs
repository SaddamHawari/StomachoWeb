using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Stomacho.Models.ViewModels
{
    public class MenuVM
    {
        public MenuItem MenuItem { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RestaurantList { get; set; }
    }
}
