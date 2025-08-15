using LoanManagementSystemAssignment.Enums;
using LoanManagementSystemAssignment.Models;
using Microsoft.Data.SqlClient;

namespace LoanManagementSystemAssignment.Repositories
{
	public class LoanRepository : ILoanRepository
	{
		private readonly string _connectionString;

		public LoanRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection")
				?? throw new ArgumentNullException(nameof(configuration), "Connection string 'DefaultConnection' cannot be null.");
		}

		public async Task<List<LoanApplication>> GetAllAsync()
		{
			var loans = new List<LoanApplication>();
			using (var conn = new SqlConnection(_connectionString))
			{
				await conn.OpenAsync();
				using (var cmd = new SqlCommand(
					"SELECT Id, CustomerName, NicPassport, LoanType, InterestRate, LoanAmount, DurationMonths, Status " +
					"FROM LoanApplications " +
					"ORDER BY Id " , conn))
				{
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							loans.Add(new LoanApplication
							{
								Id = reader.GetInt32(0),
								CustomerName = reader.GetString(1),
								NicPassport = reader.GetString(2),
								LoanType = Enum.Parse<LoanType>(reader.GetString(3)),
								InterestRate = reader.GetDecimal(4),
								LoanAmount = reader.GetDecimal(5),
								DurationMonths = reader.GetInt32(6),
								Status = Enum.Parse<LoanStatus>(reader.GetString(7))
							});
						}
					}
				}
			}
			return loans;
		}
	}
}
