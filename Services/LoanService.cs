using LoanManagementSystemAssignment.Enums;
using LoanManagementSystemAssignment.Models;
using LoanManagementSystemAssignment.Repositories;
using LoanManagementSystemAssignment.Validators;
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
				throw new ArgumentException($"Loan application with ID {id} not found.");

			return loan;
		}

		public async Task AddAsync(LoanApplicationViewModel viewModel)
		{
			LoanValidator.Validate(viewModel);

			var loan = new LoanApplication
			{
				CustomerName = viewModel.CustomerName,
				NicPassport = viewModel.NicPassport,
				LoanType = viewModel.LoanType,
				InterestRate = GetInterestRate(viewModel.LoanType.ToString()),
				LoanAmount = viewModel.LoanAmount,
				DurationMonths = viewModel.DurationMonths,
				Status = viewModel.Status
			};

			await _repository.AddAsync(loan);
		}

		public async Task UpdateAsync(LoanApplicationViewModel viewModel)
		{
			LoanValidator.Validate(viewModel);

			var loan = new LoanApplication
			{
				Id = viewModel.Id,
				CustomerName = viewModel.CustomerName,
				NicPassport = viewModel.NicPassport,
				LoanType = viewModel.LoanType,
				InterestRate = GetInterestRate(viewModel.LoanType.ToString()),
				LoanAmount = viewModel.LoanAmount,
				DurationMonths = viewModel.DurationMonths,
				Status = viewModel.Status
			};

			await _repository.UpdateAsync(loan);
		}

		public async Task UpdateStatusAsync(int id, string status)
		{
			if (!Enum.TryParse<LoanStatus>(status, out _))
				throw new ArgumentException("Invalid status value.");

			var loan = await GetByIdAsync(id);
			await _repository.UpdateStatusAsync(id, status);
		}

		public decimal GetInterestRate(string loanType)
		{
			return loanType switch
			{
				"Personal" => 5.0m,
				"Housing" => 3.0m,
				"Vehicle" => 7.0m,
				_ => throw new ArgumentException("Invalid Loan Type.")
			};
		}
	}
}