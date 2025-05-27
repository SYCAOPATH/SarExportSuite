using SarExportSuite.Models;

namespace SarExportSuite.Repositories
{
    public interface IExportRepository
    {
        Task<IEnumerable<Export>> GetExportSummaryAsync();
        Task<Export?> GetExportByIdAsync(int id);
        Task<int> CreateExportAsync(Export export);
        Task<bool> UpdateExportAsync(Export export);
        Task<bool> DeleteExportAsync(int id);
    }
}
