using LoanManagementSystemAssignment.Enums;
using System.ComponentModel.DataAnnotations;

namespace LoanManagementSystemAssignment.ViewModels
{
	public class LoanApplicationViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Customer Name is required")]
		[StringLength(100)]
		public required string CustomerName { get; set; }

		[Required(ErrorMessage = "NIC/Passport is required")]
		[StringLength(50)]
		public required string NicPassport { get; set; }

		[Required(ErrorMessage = "Loan Type is required")]
		public LoanType LoanType { get; set; }

		[Display(Name = "Interest Rate (%)")]
		public decimal InterestRate { get; set; }

		[Required(ErrorMessage = "Loan Amount is required")]
		[Range(1, double.MaxValue, ErrorMessage = "Loan Amount must be positive")]
		public decimal LoanAmount { get; set; }

		[Required(ErrorMessage = "Duration is required")]
		[Range(1, 360, ErrorMessage = "Duration must be between 1 and 360 months")]
		public int DurationMonths { get; set; }

		public LoanStatus Status { get; set; } = LoanStatus.New;
	}
}
