namespace Ects.Persistence.Models.Abstractions
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}