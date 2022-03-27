namespace uBeac;

public interface IResult
{
    List<Error> Errors { get; }
    List<object> Debug { get; set; }
    string TraceId { get; set; }
    string SessionId { get; set; }
    double Duration { get; set; }
    int Code { get; set; }
}

public interface IResult<TData> : IResult
{
    TData Data { get; set; }
}

public class Result : IResult
{
    public List<Error> Errors { get; } = new List<Error>();
    public List<object> Debug { get; set; } = new List<object>();
    public string TraceId { get; set; }
    public string SessionId { get; set; }
    public double Duration { get; set; } = 0;
    public int Code { get; set; } = 200;

    public Result()
    {
    }

    public Result(Exception exception)
    {
        Errors.Add(new Error(exception));
        Code = 500;
    }
}

public class Result<TData> : Result, IResult<TData>
{
    public TData Data { get; set; }

    public Result(TData data)
    {
        Data = data;
    }

    public Result(Exception exception) : base(exception)
    {
    }

    public Result()
    {

    }
}