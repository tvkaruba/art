namespace Art.Persistence.Infrastructure.Abstractions
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}