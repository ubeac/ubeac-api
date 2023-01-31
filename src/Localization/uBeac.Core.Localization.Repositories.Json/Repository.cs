using System.Text;
using Newtonsoft.Json;

namespace uBeac.Localization.Repositories.Json;

public interface IJsonLocalizationRepository : ILocalizationRepository
{
}

public class JsonLocalizationRepository : IJsonLocalizationRepository
{
    protected readonly string ContentRootPath;
    protected readonly JsonLocalizationOptions Options;

    protected readonly JsonSerializerSettings SerializerSettings = new();

    protected readonly string DirectoryPath;

    public JsonLocalizationRepository(string contentRootPath = null, JsonLocalizationOptions options = null)
    {
        ContentRootPath = contentRootPath ?? Environment.CurrentDirectory;
        Options = options ?? new JsonLocalizationOptions();

        DirectoryPath = Path.Combine(ContentRootPath, Options.FolderName);
    }

    public async Task<IEnumerable<LocalizationValue>> GetAll(CancellationToken cancellationToken = default)
    {
        var result = new List<LocalizationValue>();

        foreach (var file in Directory.GetFiles(DirectoryPath))
        {
            if (Path.GetExtension(file) != ".json") continue;

            var cultureName = Path.GetFileNameWithoutExtension(file);
            var dictionary = ReadDictionaryFromCultureFile(cultureName);
            var values = MapDictionaryToLocalizationValues(cultureName, dictionary);

            result.AddRange(values);
        }

        return await Task.FromResult(result);
    }

    public Task Upsert(LocalizationValue entity, CancellationToken cancellationToken = default)
    {
        var values = ReadDictionaryFromCultureFile(entity.CultureName);
        
        TryRemove(values, entity.Key);

        values.Add(entity.Key, entity.Value);

        WriteValuesToCultureFile(entity.CultureName, values);

        return Task.CompletedTask;
    }

    public Task Delete(string key, string cultureName, CancellationToken cancellationToken = default)
    {
        var values = ReadDictionaryFromCultureFile(cultureName);
        
        TryRemove(values, key);

        WriteValuesToCultureFile(cultureName, values);

        return Task.CompletedTask;
    }

    public string GetCultureFilePath(string cultureName) => Path.Combine(DirectoryPath, $"{cultureName}.json");

    public IDictionary<string, string> ReadDictionaryFromCultureFile(string cultureName)
    {
        var file = GetCultureFilePath(cultureName);
        var fileContent = File.ReadAllText(file, Encoding.UTF8);
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(fileContent, SerializerSettings);
    }

    public void WriteValuesToCultureFile(string cultureName, IDictionary<string, string> values)
    {
        var file = GetCultureFilePath(cultureName);
        var fileContent = JsonConvert.SerializeObject(values, SerializerSettings);
        File.WriteAllText(file, fileContent, Encoding.UTF8);
    }

    public void TryRemove(IDictionary<string, string> values, string key)
    {
        if (values.ContainsKey(key)) values.Remove(key);
    }

    public IEnumerable<LocalizationValue> MapDictionaryToLocalizationValues(string cultureName, IDictionary<string, string> dictionary)
    {
        return dictionary.Select(x => new LocalizationValue
        {
            Key = x.Key,
            Value = x.Value,
            CultureName = cultureName
        });
    }
}
