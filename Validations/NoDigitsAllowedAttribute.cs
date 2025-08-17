using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LoanManagementSystemAssignment.Validations
{
	public class NoDigitsAllowedAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null) return ValidationResult.Success;

			string input = value.ToString()!.Trim();

			if (Regex.IsMatch(input, @"\d"))
			{
				return new ValidationResult(ErrorMessage ??
					$"{validationContext.DisplayName} cannot contain digits");
			}

			return ValidationResult.Success;
		}
	}
}
