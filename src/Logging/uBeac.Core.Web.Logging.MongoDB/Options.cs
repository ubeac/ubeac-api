namespace uBeac.Web.Logging.MongoDB;

public class MongoDbHttpLogOptions
{
    public string HttpLog2xxConnectionString { get; set; }
    public string HttpLog2xxCollectionName { get; set; }

    public string HttpLog4xxConnectionString { get; set; }
    public string HttpLog4xxCollectionName { get; set; }

    public string HttpLog5xxConnectionString { get; set; }
    public string HttpLog5xxCollectionName { get; set; }

    public string GetConnectionString(int statusCode) => statusCode switch
    {
        < 500 and >= 400 => HttpLog4xxConnectionString,
        >= 500 => HttpLog5xxConnectionString,
        _ => HttpLog2xxConnectionString
    };

    public string GetCollectionName(int statusCode) => statusCode switch
    {
        < 500 and >= 400 => HttpLog4xxCollectionName,
        >= 500 => HttpLog5xxCollectionName,
        _ => HttpLog2xxCollectionName
    };
}