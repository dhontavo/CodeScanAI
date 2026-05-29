using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using CodeScanAI.Models;
using CodeScanAI.Services;

namespace CodeScanAI.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly IAiService _ai;
    private readonly DatabaseService _db;

    [ObservableProperty] string code = string.Empty;
    [ObservableProperty] string selectedAnalysis = "explain";
    [ObservableProperty] AnalysisType selectedAnalysisItem;
    [ObservableProperty] bool isLoading;

    public List<AnalysisType> AnalysisTypes { get; } = new()
    {
        new AnalysisType("explain",  "Explicar codigo"),
        new AnalysisType("bugs",     "Detectar bugs"),
        new AnalysisType("docs",     "Generar documentacion"),
        new AnalysisType("optimize", "Optimizar codigo")
    };

    public HomeViewModel(IAiService ai, DatabaseService db)
    {
        _ai = ai;
        _db = db;
        // Initialize selected item from key
        SelectedAnalysisItem = AnalysisTypes.FirstOrDefault(x => x.Key == SelectedAnalysis);
    }

    // This partial method will be called by the source generator when SelectedAnalysisItem changes
    partial void OnSelectedAnalysisItemChanged(AnalysisType value)
    {
        if (value is not null)
        {
            SelectedAnalysis = value.Key;
        }
    }

    [RelayCommand]
    async Task AnalyzeAsync()
    {
        if (string.IsNullOrWhiteSpace(Code)) return;

        IsLoading = true;
        try
        {
            var result = await _ai.AnalyzeAsync(Code, SelectedAnalysis);

            await _db.SaveAsync(new ScanHistory
            {
                CodeSnippet = Code,
                AnalysisType = SelectedAnalysis,
                Result = result
            });

            var label = AnalysisTypes.First(x => x.Key == SelectedAnalysis).Label;
            await Shell.Current.GoToAsync("result",
                new Dictionary<string, object> { ["Result"] = result, ["Title"] = label });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        finally { IsLoading = false; }
    }
}
