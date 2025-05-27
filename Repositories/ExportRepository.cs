using SarExportSuite.DataAccess;
using SarExportSuite.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SarExportSuite.Repositories
{
    public class ExportRepository : IExportRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ExportRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Export>> GetExportSummaryAsync()
        {
            var exports = new List<Export>();

            using var connection = (SqlConnection)_connectionFactory.CreateConnection();
            using var command = new SqlCommand("GetExportSummary", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                exports.Add(new Export
                {
                    ExportId = reader.GetInt32("ExportId"),
                    ExportName = reader.GetString("ExportName"),
                    ExportDate = reader.GetDateTime("ExportDate"),
                    Status = reader.GetString("Status")
                });
            }

            return exports;
        }

        public async Task<Export?> GetExportByIdAsync(int id)
        {
            using var connection = (SqlConnection)_connectionFactory.CreateConnection();
            using var command = new SqlCommand("SELECT ExportId, ExportName, ExportDate, Status FROM Exports WHERE ExportId = @Id", connection);
            
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Export
                {
                    ExportId = reader.GetInt32("ExportId"),
                    ExportName = reader.GetString("ExportName"),
                    ExportDate = reader.GetDateTime("ExportDate"),
                    Status = reader.GetString("Status")
                };
            }

            return null;
        }

        public async Task<int> CreateExportAsync(Export export)
        {
            using var connection = (SqlConnection)_connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "INSERT INTO Exports (ExportName, ExportDate, Status) " +
                "OUTPUT INSERTED.ExportId " +
                "VALUES (@ExportName, @ExportDate, @Status)", 
                connection);

            command.Parameters.AddWithValue("@ExportName", export.ExportName);
            command.Parameters.AddWithValue("@ExportDate", export.ExportDate);
            command.Parameters.AddWithValue("@Status", export.Status);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return (int)result!;
        }

        public async Task<bool> UpdateExportAsync(Export export)
        {
            using var connection = (SqlConnection)_connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "UPDATE Exports SET ExportName = @ExportName, ExportDate = @ExportDate, Status = @Status " +
                "WHERE ExportId = @ExportId", 
                connection);

            command.Parameters.AddWithValue("@ExportId", export.ExportId);
            command.Parameters.AddWithValue("@ExportName", export.ExportName);
            command.Parameters.AddWithValue("@ExportDate", export.ExportDate);
            command.Parameters.AddWithValue("@Status", export.Status);

            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteExportAsync(int id)
        {
            using var connection = (SqlConnection)_connectionFactory.CreateConnection();
            using var command = new SqlCommand("DELETE FROM Exports WHERE ExportId = @Id", connection);
            
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }
}
