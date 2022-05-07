namespace uBeac.Web.Logging.MongoDB;

public class HttpLoggingMongoDbOptions
{
    public string HttpLog2xxConnectionString { get; set; }
    public string HttpLog2xxCollectionName { get; set; } = "HttpLog2xx";

    public string HttpLog4xxConnectionString { get; set; }
    public string HttpLog4xxCollectionName { get; set; } = "HttpLog4xx";

    public string HttpLog5xxConnectionString { get; set; }
    public string HttpLog5xxCollectionName { get; set; } = "HttpLog5xx";
}