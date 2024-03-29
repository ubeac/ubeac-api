﻿namespace uBeac.Web.Logging.MongoDB;

public class MongoDbHttpLogOptions
{
    public string Status200CollectionName { get; set; }
    public string Status400CollectionName { get; set; }
    public string Status500CollectionName { get; set; }
    public int BypassLogTimeOut { get; set; } = 300; // Number of seconds to bypass insert log if there is an issue with Log DB

    public string GetCollectionName(int statusCode) => statusCode switch
    {
        < 500 and >= 400 => Status400CollectionName,
        >= 500 => Status500CollectionName,
        _ => Status200CollectionName
    };
}