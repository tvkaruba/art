namespace Ects.Persistence.Abstractions
{
    public interface IUnitOfWorkConfiguration
    {
        string ConnectionString { get; }
    }
}