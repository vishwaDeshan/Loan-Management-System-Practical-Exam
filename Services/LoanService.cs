using LoanManagementSystemAssignment.Models;
using LoanManagementSystemAssignment.Repositories;
using LoanManagementSystemAssignment.ViewModels;

namespace LoanManagementSystemAssignment.Services
{
	public class LoanService : ILoanService
	{
		private readonly ILoanRepository _repository;
		private readonly ILogger<LoanService> _logger;

		public LoanService(ILoanRepository repository, ILogger<LoanService> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task<List<LoanApplication>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<LoanApplication> GetByIdAsync(int id)
		{
			var loan = await _repository.GetByIdAsync(id);
			if (loan == null)
			{
				throw new ArgumentException($"Loan application with ID {id} not found.");
			}
			return loan;
		}

	}
}