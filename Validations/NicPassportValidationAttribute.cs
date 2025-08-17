using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LoanManagementSystemAssignment.Validations
{
	public class NicPassportValidationAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null)
				return new ValidationResult("NIC/Passport is required");

			string input = value.ToString()!.Trim();


			var nicOldPattern = @"^[0-9]{9}[vVxX]$";
			var nicNewPattern = @"^[0-9]{12}$";
			var passportPattern = @"^[A-Z][0-9]{6,8}$"; // Uppercase letter + digits(N1234567)

			if (Regex.IsMatch(input, nicOldPattern) ||
				Regex.IsMatch(input, nicNewPattern) ||
				Regex.IsMatch(input, passportPattern))
			{
				return ValidationResult.Success;
			}

			return new ValidationResult("Invalid NIC or Passport format");
		}
	}
}
