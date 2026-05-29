using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CodeScanAI.Models;
using CodeScanAI.Services;

namespace CodeScanAI.ViewModels;

public partial class HistoryViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    [ObservableProperty] ObservableCollection<ScanHistory> items = new();

    public HistoryViewModel(DatabaseService db) => _db = db;

    [RelayCommand]
    async Task LoadAsync()
    {
        var list = await _db.GetHistoryAsync();
        Items = new ObservableCollection<ScanHistory>(list);
    }

    [RelayCommand]
    async Task DeleteAsync(ScanHistory item)
    {
        await _db.DeleteAsync(item);
        Items.Remove(item);
    }
}
