namespace Art.Persistence.Infrastructure.Abstractions
{
    public interface IUnitOfWorkConfiguration
    {
        string ConnectionString { get; }
    }
}
