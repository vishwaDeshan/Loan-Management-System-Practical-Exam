using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LoanManagementSystemAssignment.Validations
{
	public class NoDigitsAllowedAttribute : ValidationAttribute, IClientModelValidator
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null) return ValidationResult.Success;

			string input = value.ToString()!.Trim();

			if (Regex.IsMatch(input, @"\d"))
			{
				return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} cannot contain digits");
			}

			return ValidationResult.Success;
		}

		public void AddValidation(ClientModelValidationContext context)
		{
			context.Attributes.Add("data-val", "true");
			context.Attributes.Add("data-val-nodigits", ErrorMessage ?? $"{context.ModelMetadata.DisplayName} cannot contain digits");
		}
	}
}
