using GameZone.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModels
{
    public class EditGameFormViewMode
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }

        [AllowedExtensions(".jpg,.jpeg,.png")]
        [MaximumSizeFile(100 * 1024 * 1024)] // 100MB
        public IFormFile? Cover { get; set; }
        public string? CurrentCover { get; set; }
        [Required] public int CategoryID { get; set; }
        [Required] public List<int> SelectedDevices { get; set; }

        public IEnumerable<SelectListItem>? Categories { get; set; }
        public IEnumerable<SelectListItem>? Devices { get; set; }
    }
}
