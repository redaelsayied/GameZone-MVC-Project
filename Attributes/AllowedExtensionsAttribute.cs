using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
	public class AllowedExtensionsAttribute : ValidationAttribute
	{
		private string _allowedExtensions;

		public AllowedExtensionsAttribute(string allowedExtensions)
		{
			_allowedExtensions = allowedExtensions;
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile;

			if (file != null)
			{
				var extension = Path.GetExtension(file.FileName);
				bool isAllowed = _allowedExtensions.Split(',').Contains(extension,StringComparer.OrdinalIgnoreCase);

				if (!isAllowed)
				{
					return new ValidationResult(
						$"This file extension is not allowed. You should choose between this {_allowedExtensions}");
				}
			}

			return ValidationResult.Success;
		}
	}
}