using System.ComponentModel.DataAnnotations;
using GameZone.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModels;

public class CreateGameFormViewModel
{
	[Required] public string Name { get; set; }
	[Required] public string Description { get; set; }

	[Required]
	[AllowedExtensions(".jpg,.jpeg,.png")]
	[MaximumSizeFile(100*1024*1024)] // 100MB
	public IFormFile Cover { get; set; } = default!;

	[Required] public int CategoryID { get; set; }
	[Required] public List<int> SelectedDevices { get; set; }

	public IEnumerable<SelectListItem>? Categories { get; set; }
	public IEnumerable<SelectListItem>? Devices { get; set; }
}