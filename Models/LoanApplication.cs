using LoanManagementSystemAssignment.Enums;

namespace LoanManagementSystemAssignment.Models
{
	public class LoanApplication
	{
		public int Id { get; set; }
		public required string CustomerName { get; set; }
		public required string NicPassport { get; set; }
		public LoanType LoanType { get; set; }
		public decimal InterestRate { get; set; }
		public decimal LoanAmount { get; set; }
		public int DurationMonths { get; set; }
		public LoanStatus Status { get; set; }
	}
}
