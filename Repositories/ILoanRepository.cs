using LoanManagementSystemAssignment.Models;

namespace LoanManagementSystemAssignment.Repositories
{
	public interface ILoanRepository
	{
		Task<List<LoanApplication>> GetAllAsync();

		Task<LoanApplication> GetByIdAsync(int id);

		Task AddAsync(LoanApplication loan);

	}
}
