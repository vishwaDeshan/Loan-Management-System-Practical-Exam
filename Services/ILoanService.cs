using LoanManagementSystemAssignment.Models;

namespace LoanManagementSystemAssignment.Services
{
	public interface ILoanService
	{
		Task<List<LoanApplication>> GetAllAsync();
	}
}
