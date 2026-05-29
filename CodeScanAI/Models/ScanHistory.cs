using SQLite;

namespace CodeScanAI.Models;

public class ScanHistory
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string CodeSnippet { get; set; } = string.Empty;
    public string AnalysisType { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}