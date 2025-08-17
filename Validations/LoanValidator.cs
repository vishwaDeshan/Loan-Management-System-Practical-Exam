using LoanManagementSystemAssignment.Enums;
using LoanManagementSystemAssignment.ViewModels;

namespace LoanManagementSystemAssignment.Validations
{
	public static class LoanValidator
	{
		public static void Validate(LoanApplicationViewModel viewModel)
		{
			if (viewModel == null)
				throw new ArgumentNullException(nameof(viewModel));

			if (string.IsNullOrWhiteSpace(viewModel.CustomerName))
				throw new ArgumentException("Customer Name cannot be empty.");

			if (viewModel.CustomerName.Any(char.IsDigit))
				throw new ArgumentException("Customer Name cannot contain numbers.");

			if (string.IsNullOrWhiteSpace(viewModel.NicPassport))
				throw new ArgumentException("NIC/Passport cannot be empty.");

			if (!Enum.IsDefined(typeof(LoanType), viewModel.LoanType))
				throw new ArgumentException("Invalid Loan Type.");

			if (viewModel.LoanAmount <= 0)
				throw new ArgumentException("Loan Amount must be positive.");

			if (viewModel.DurationMonths <= 0 || viewModel.DurationMonths > 360)
				throw new ArgumentException("Duration must be between 1 and 360 months.");

			if (!Enum.IsDefined(typeof(LoanStatus), viewModel.Status))
				throw new ArgumentException("Invalid Status.");
		}
	}
}
