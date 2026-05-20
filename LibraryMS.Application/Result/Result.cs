namespace LibraryMS.Application.Result;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }
    public Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public static Result Success => new Result(true, string.Empty);
    public static Result Failure(string error) => new Result(false, error);
}

public class Result<T> : Result
{
    public T? Data { get; }

    private Result(bool isSuccess, T? data, string error) : base(isSuccess, error)
    {
        Data = data;
    }
    
    public new static Result<T> Success(T value) => new(true, value, string.Empty);
    public new static Result<T> Failure(string error) => new(false, default, error);
}