namespace Minaev.Tools.Types;

public struct Error
{
    public String Key { get; }
    public String Message { get; }

    public Error(String message)
    {
        Key = String.Empty;
        Message = message;
    }

    public Error(String key, String message)
    {
        Key = key;
        Message = message;
    }
}

public static class ErrorExtensions
{
    public static Error[] ToErrors(this String str) => new[] { new Error(str) };
    public static Error[] ToErrors(this String[] strs) => strs.SelectMany(s => s.ToErrors()).ToArray();
    public static String AsString(this IEnumerable<Error> errors) => String.Join("; ", errors.Select(e => e.Message));
}