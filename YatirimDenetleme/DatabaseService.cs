using SQLite;

namespace YatirimDenetleme;

public class DatabaseService
{
    private SQLiteAsyncConnection _db;

    private async Task Init()
    {
        if (_db != null)
            return;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "YatirimVerileri.db3");
        _db = new SQLiteAsyncConnection(dbPath);
        await _db.CreateTableAsync<Asset>();
    }

    public async Task AddAssetAsync(Asset asset)
    {
        await Init();
        await _db.InsertAsync(asset);
    }

    public async Task<List<Asset>> GetAssetsAsync()
    {
        await Init();
        return await _db.Table<Asset>().ToListAsync();
    }

    public async Task UpdateAssetAsync(Asset asset)
    {
        await Init();
        await _db.UpdateAsync(asset);
    }

    public async Task DeleteAssetAsync(Asset asset)
    {
        await Init();
        await _db.DeleteAsync(asset);
    }
}