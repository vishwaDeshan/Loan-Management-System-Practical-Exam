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
					"ORDER BY Id ", conn))
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

		public async Task<LoanApplication> GetByIdAsync(int id)
		{
			LoanApplication? loan = null;
			using (var conn = new SqlConnection(_connectionString))
			{
				await conn.OpenAsync();
				using (var cmd = new SqlCommand("SELECT * FROM LoanApplications WHERE Id = @Id", conn))
				{
					cmd.Parameters.AddWithValue("@Id", id);
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						if (await reader.ReadAsync())
						{
							loan = new LoanApplication
							{
								Id = reader.GetInt32(0),
								CustomerName = reader.GetString(1),
								NicPassport = reader.GetString(2),
								LoanType = Enum.Parse<LoanType>(reader.GetString(3)),
								InterestRate = reader.GetDecimal(4),
								LoanAmount = reader.GetDecimal(5),
								DurationMonths = reader.GetInt32(6),
								Status = Enum.Parse<LoanStatus>(reader.GetString(7))
							};
						}
					}
				}
			}
			return loan;
		}

		public async Task AddAsync(LoanApplication loan)
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				await conn.OpenAsync();
				using (var cmd = new SqlCommand(
					@"INSERT INTO LoanApplications (CustomerName, NicPassport, LoanType, InterestRate, LoanAmount, DurationMonths, Status)
                  VALUES (@CustomerName, @NicPassport, @LoanType, @InterestRate, @LoanAmount, @DurationMonths, @Status)", conn))
				{
					cmd.Parameters.AddWithValue("@CustomerName", loan.CustomerName);
					cmd.Parameters.AddWithValue("@NicPassport", loan.NicPassport);
					cmd.Parameters.AddWithValue("@LoanType", loan.LoanType.ToString());
					cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
					cmd.Parameters.AddWithValue("@LoanAmount", loan.LoanAmount);
					cmd.Parameters.AddWithValue("@DurationMonths", loan.DurationMonths);
					cmd.Parameters.AddWithValue("@Status", loan.Status.ToString());
					await cmd.ExecuteNonQueryAsync();
				}
			}
		}

		public async Task UpdateStatusAsync(int id, string status)
		{
			if (!Enum.TryParse<LoanStatus>(status, out _))
			{
				throw new ArgumentException("Invalid status value");
			}

			using (var conn = new SqlConnection(_connectionString))
			{
				await conn.OpenAsync();
				using (var cmd = new SqlCommand(
					"UPDATE LoanApplications SET Status = @Status WHERE Id = @Id", conn))
				{
					cmd.Parameters.AddWithValue("@Status", status);
					cmd.Parameters.AddWithValue("@Id", id);
					await cmd.ExecuteNonQueryAsync();
				}
			}
		}

		public async Task UpdateAsync(LoanApplication loan)
		{
			if (loan == null) throw new ArgumentNullException(nameof(loan));

			using (var conn = new SqlConnection(_connectionString))
			{
				await conn.OpenAsync();

				var query = @"
            UPDATE LoanApplications
            SET CustomerName = @CustomerName,
                NicPassport = @NicPassport,
                LoanType = @LoanType,
                InterestRate = @InterestRate,
                LoanAmount = @LoanAmount,
                DurationMonths = @DurationMonths,
                Status = @Status
            WHERE Id = @Id";

				using (var cmd = new SqlCommand(query, conn))
				{
					cmd.Parameters.AddWithValue("@CustomerName", loan.CustomerName);
					cmd.Parameters.AddWithValue("@NicPassport", loan.NicPassport);
					cmd.Parameters.AddWithValue("@LoanType", loan.LoanType.ToString());
					cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
					cmd.Parameters.AddWithValue("@LoanAmount", loan.LoanAmount);
					cmd.Parameters.AddWithValue("@DurationMonths", loan.DurationMonths);
					cmd.Parameters.AddWithValue("@Status", loan.Status.ToString());
					cmd.Parameters.AddWithValue("@Id", loan.Id);

					int rowsAffected = await cmd.ExecuteNonQueryAsync();
					if (rowsAffected == 0)
					{
						throw new ArgumentException("Loan not found or could not be updated");
					}
				}
			}
		}

	}
}
