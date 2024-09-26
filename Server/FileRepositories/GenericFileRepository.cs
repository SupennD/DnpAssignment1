using System.Text.Json;

namespace FileRepositories;

public abstract class GenericFileRepository<T>
{
    private readonly string _filePath;

    private readonly JsonSerializerOptions _writeOptions = new() { WriteIndented = true };

    protected GenericFileRepository(string path)
    {
        // Get the users documents folder path
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Store json files in the users documents folder under DNPAssignment
        _filePath = Path.Combine(documentsPath, "DNPAssignment", path);

        // Create the DNPAssignment folder if it does not exist
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        // Initialize the json file with an empty collection if it does not exist
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    public abstract Task<T> AddAsync(T entity);

    public abstract Task UpdateAsync(T entity);

    public abstract Task DeleteAsync(int id);

    public abstract Task<T> GetSingleAsync(int id);

    public abstract IQueryable<T> GetMany();

    protected async Task<List<T>> ReadFromJsonAsync()
    {
        string json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<T>>(json)!;
    }

    protected Task WriteToJsonAsync(List<T> value)
    {
        string json = JsonSerializer.Serialize(value, _writeOptions);
        return File.WriteAllTextAsync(_filePath, json);
    }
}
