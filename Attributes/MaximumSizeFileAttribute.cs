using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
	public class MaximumSizeFileAttribute:ValidationAttribute
	{
		private int _maxFileSize;

		public MaximumSizeFileAttribute(int maxFileSize)
		{
			_maxFileSize = maxFileSize;
		}

		protected override ValidationResult? IsValid(object? value,
			ValidationContext validationContext)
		{

			var file = value as IFormFile;

			if (file != null && file.Length > _maxFileSize)
			{
				return new ValidationResult($"Maximum allowed file size is {_maxFileSize / (1024 * 1024)} MB.");
			}

			return ValidationResult.Success;
		}
	}
}
