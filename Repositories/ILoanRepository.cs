using LoanManagementSystemAssignment.Models;

namespace LoanManagementSystemAssignment.Repositories
{
	public interface ILoanRepository
	{
		Task<List<LoanApplication>> GetAllAsync();
	}
}
