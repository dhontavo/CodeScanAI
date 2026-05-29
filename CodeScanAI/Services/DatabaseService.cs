using SQLite;
using CodeScanAI.Models;

namespace CodeScanAI.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection? _db;

    private async Task InitAsync()
    {
        if (_db != null) return;
        var path = Path.Combine(FileSystem.AppDataDirectory, "codescan.db3");
        _db = new SQLiteAsyncConnection(path);
        await _db.CreateTableAsync<ScanHistory>();
    }

    public async Task SaveAsync(ScanHistory item)
    {
        await InitAsync();
        await _db!.InsertAsync(item);
    }

    public async Task<List<ScanHistory>> GetHistoryAsync()
    {
        await InitAsync();
        return await _db!.Table<ScanHistory>()
            .OrderByDescending(x => x.CreatedAt)
            .Take(10)
            .ToListAsync();
    }

    public async Task DeleteAsync(ScanHistory item)
    {
        await InitAsync();
        await _db!.DeleteAsync(item);
    }
}