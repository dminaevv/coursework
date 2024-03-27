using static Minaev.Tools.Extensions.EnumerableExtensions;

namespace Minaev.Tools.Types;

public class Result : BaseResult
{
    public override Boolean IsSuccess => !Errors.Any();

    private Result(params Error[] errors) : base(errors) { }

    public void Deconstruct(out Boolean isSuccess, out Error[] errors)
    {
        isSuccess = IsSuccess;
        errors = Errors;
    }

    public static Result Success() => new();

    public static Result Fail(Error error) => new(error);
    public static Result Fail(String error) => new(new Error(error));
    public static Result Fail(IEnumerable<Error> errors) => new(errors.ToArray());

    public static Result operator &(Result a, Result b)
    {
        Error[] errors = Concat(a.Errors, b.Errors).ToArray();
        return new Result(errors);
    }

    public static Boolean operator false(Result result) => !result.IsSuccess;
    public static Boolean operator true(Result result) => result.IsSuccess;
}