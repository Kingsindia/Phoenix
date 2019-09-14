namespace Contoso.Phoenix.Data.Entity.Common
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
