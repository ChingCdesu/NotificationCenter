using System.Text.Json;
using StackExchange.Redis;

namespace NotificationCenter.Storage;

public class RedisCache
{
    private readonly IDatabase _database;
    
    public RedisCache(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("Redis:ConnectionString");
        if (connectionString == null)
        {
            throw new ApplicationException("Redis connection string is not configured.");
        }

        var connection = ConnectionMultiplexer.Connect(connectionString);
        _database = connection.GetDatabase();
    }
    
    public async Task<bool> SetAsync(string key, string value)
    {
        return await _database.StringSetAsync(key, value);
    }
    
    public async Task<string?> GetAsync(string key)
    {
        return await _database.StringGetAsync(key);
    }
    
    public async Task<bool> DeleteAsync(string key)
    {
        return await _database.KeyDeleteAsync(key);
    }
    
    public async Task<bool> SetObjectAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        return await _database.StringSetAsync(key, json);
    }
    
    public async Task<T?> GetObjectAsync<T>(string key)
    {
        var json = await _database.StringGetAsync(key);
        return json.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(json!);
    }
}