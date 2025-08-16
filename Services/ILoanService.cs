using LoanManagementSystemAssignment.Models;
using LoanManagementSystemAssignment.ViewModels;

namespace LoanManagementSystemAssignment.Services
{
	public interface ILoanService
	{
		Task<List<LoanApplication>> GetAllAsync();

		Task<LoanApplication> GetByIdAsync(int id);

		Task AddAsync(LoanApplicationViewModel viewModel);

		Task UpdateStatusAsync(int id, string status);

		Task UpdateAsync(LoanApplicationViewModel viewModel);

		decimal GetInterestRate(string loanType);

	}
}
