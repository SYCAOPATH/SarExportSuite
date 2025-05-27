namespace SarExportSuite.Models
{
    public class Export
    {
        public int ExportId { get; set; }
        public string ExportName { get; set; } = string.Empty;
        public DateTime ExportDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
