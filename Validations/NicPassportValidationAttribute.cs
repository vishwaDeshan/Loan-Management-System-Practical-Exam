using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.RegularExpressions;

namespace LoanManagementSystemAssignment.Validations
{
	public class NicPassportValidationAttribute : ValidationAttribute, IClientModelValidator
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null)
				return new ValidationResult("NIC/Passport is required");

			string input = value.ToString()!.Trim();

			var nicOldPattern = @"^[0-9]{9}[vVxX]$";
			var nicNewPattern = @"^[0-9]{12}$";
			var passportPattern = @"^[A-Z][0-9]{6,8}$";

			if (Regex.IsMatch(input, nicOldPattern) ||
				Regex.IsMatch(input, nicNewPattern) ||
				Regex.IsMatch(input, passportPattern))
			{
				return ValidationResult.Success;
			}

			return new ValidationResult("Invalid NIC or Passport format");
		}

		public void AddValidation(ClientModelValidationContext context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			context.Attributes.Add("data-val", "true");
			context.Attributes.Add("data-val-nicpassport", ErrorMessage ?? "Invalid NIC or Passport format");
		}
	}
}