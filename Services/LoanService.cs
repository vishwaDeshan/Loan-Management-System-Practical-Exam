using LoanManagementSystemAssignment.Models;
using LoanManagementSystemAssignment.Repositories;

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

		Task<List<LoanApplication>> ILoanService.GetAllAsync()
		{
			try
			{
				var loans = _repository.GetAllAsync();
				return loans;
			}catch(Exception ex)
			{
				_logger.LogError(ex, "An error occurred while retrieving loan applications.");
				throw;
			}
		}
	}
}