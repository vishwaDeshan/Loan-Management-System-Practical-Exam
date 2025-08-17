using LoanManagementSystemAssignment.Enums;
using LoanManagementSystemAssignment.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace LoanManagementSystemAssignment.Validators
{
	public static class LoanValidator
	{
		public static void Validate(LoanApplicationViewModel viewModel)
		{
			if (viewModel == null)
				throw new ArgumentNullException(nameof(viewModel));

			var context = new ValidationContext(viewModel);
			var results = new List<ValidationResult>();

			if (!Validator.TryValidateObject(viewModel, context, results, true))
			{
				var errors = string.Join("; ", results.Select(r => r.ErrorMessage));
				throw new ValidationException(errors);
			}

			if (viewModel.LoanAmount < 1000)
			{
				throw new ValidationException("Loan Amount must be at least 1000.");
			}

			if (!Enum.IsDefined(typeof(LoanStatus), viewModel.Status))
			{
				throw new ValidationException("Invalid Loan Status.");
			}
		}
	}
}
