namespace CodeScanAI.Services;

public interface IAiService
{
    Task<string> AnalyzeAsync(string code, string analysisType);
}
