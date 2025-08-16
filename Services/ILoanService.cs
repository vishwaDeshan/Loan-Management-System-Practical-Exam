using LoanManagementSystemAssignment.Models;
using LoanManagementSystemAssignment.ViewModels;

namespace LoanManagementSystemAssignment.Services
{
	public interface ILoanService
	{
		Task<List<LoanApplication>> GetAllAsync();

		Task<LoanApplication> GetByIdAsync(int id);
	}
}
