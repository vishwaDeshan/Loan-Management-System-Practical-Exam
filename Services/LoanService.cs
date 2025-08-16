using LoanManagementSystemAssignment.Enums;
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

		public async Task AddAsync(LoanApplicationViewModel viewModel)
		{
			if (string.IsNullOrWhiteSpace(viewModel.CustomerName))
			{
				throw new ArgumentException("Customer Name cannot be empty.");
			}
			if (string.IsNullOrWhiteSpace(viewModel.NicPassport))
			{
				throw new ArgumentException("NIC/Passport cannot be empty.");
			}
			if (!Enum.IsDefined(typeof(LoanType), viewModel.LoanType))
			{
				throw new ArgumentException("Invalid Loan Type.");
			}
			if (viewModel.LoanAmount <= 0)
			{
				throw new ArgumentException("Loan Amount must be positive.");
			}
			if (viewModel.DurationMonths <= 0 || viewModel.DurationMonths > 360)
			{
				throw new ArgumentException("Duration must be between 1 and 360 months.");
			}
			if (!Enum.IsDefined(typeof(LoanStatus), viewModel.Status))
			{
				throw new ArgumentException("Invalid Status.");
			}

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