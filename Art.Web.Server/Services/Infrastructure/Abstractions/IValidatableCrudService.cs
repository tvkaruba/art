namespace Art.Web.Server.Services.Infrastructure.Abstractions
{
    public interface IValidatableCrudService<in TKey, in TPost, in TPut, TGet> : ICrudService<TKey, TPost, TPut, TGet>
    {
    }
}
