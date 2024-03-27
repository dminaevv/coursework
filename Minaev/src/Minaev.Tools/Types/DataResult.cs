using System.Diagnostics.CodeAnalysis;

namespace Minaev.Tools.Types;

public class DataResult<T> : BaseResult
{
    public T? Data { get; }

    [MemberNotNullWhen(true, nameof(Data))]
    public override Boolean IsSuccess => !Errors.Any();

    private DataResult(T data)
    {
        Data = data;
    }

    private DataResult(IEnumerable<Error> errors) : base(errors.ToArray())
    {
        Data = default;
    }

    public Boolean IsFail([NotNullWhen(false)] out T? data)
    {
        data = Data;
        return !IsSuccess;
    }

    public static DataResult<T> Success(T data) => new(data);

    public static DataResult<T> Fail(String error) => new(error.ToErrors());
    public static DataResult<T> Fail(IEnumerable<Error> errors) => new(errors);

    public void Deconstruct(out Boolean isSuccess, out T? data)
    {
        isSuccess = IsSuccess;
        data = Data;
    }

    public void Deconstruct(out Boolean isSuccess, out Error[] errors, out T? data)
    {
        isSuccess = IsSuccess;
        errors = Errors;
        data = Data;
    }

    public Result ToResult() => IsSuccess ? Result.Success() : Result.Fail(Errors);
}