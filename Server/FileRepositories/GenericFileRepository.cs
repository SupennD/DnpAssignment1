using System.Text.Json;

namespace FileRepositories;

public abstract class GenericFileRepository<T>
{
  protected abstract string FilePath { get; }
  private readonly JsonSerializerOptions _writeOptions = new()
  {
    WriteIndented = true
  };

  public GenericFileRepository()
  {
    // Initialize the json file with an empty collection
    if (!File.Exists(FilePath)) File.WriteAllText(FilePath, "[]");
  }

  public abstract Task<T> AddAsync(T entity);

  public abstract Task UpdateAsync(T entity);

  public abstract Task DeleteAsync(int id);

  public abstract Task<T> GetSingleAsync(int id);

  public abstract IQueryable<T> GetMany();

  protected async Task<List<T>> ReadFromJsonAsync()
  {
    string json = await File.ReadAllTextAsync(FilePath);
    return JsonSerializer.Deserialize<List<T>>(json)!;
  }

  protected Task WriteToJsonAsync(List<T> value)
  {
    string json = JsonSerializer.Serialize(value, _writeOptions);
    return File.WriteAllTextAsync(FilePath, json);
  }
}
