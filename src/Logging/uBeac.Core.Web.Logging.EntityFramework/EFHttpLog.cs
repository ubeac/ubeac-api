namespace uBeac.Web.Logging.EntityFramework;

public class EFHttpLog2xx : HttpLog
{
    public EFHttpLog2xx()
    {
    }

    public EFHttpLog2xx(HttpLog log) : base(log)
    {
    }
}

public class EFHttpLog4xx : HttpLog
{
    public EFHttpLog4xx()
    {
    }

    public EFHttpLog4xx(HttpLog log) : base(log)
    {
    }
}

public class EFHttpLog5xx : HttpLog
{
    public EFHttpLog5xx()
    {
    }

    public EFHttpLog5xx(HttpLog log) : base(log)
    {
    }
}