namespace Contoso.Phoenix.Common.Configuration.Abstractions
{
    public interface IOptionsAccessor<out TOptions>
        where TOptions : class, new()
    {
        TOptions Value { get; }
    }
}
