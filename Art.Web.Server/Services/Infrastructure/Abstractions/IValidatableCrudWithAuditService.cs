namespace Art.Web.Server.Services.Infrastructure.Abstractions
{
    public interface IValidatableCrudWithAuditService<in TKey, in TPost, in TPut, TGet> : ICrudWithAuditService<TKey, TPost, TPut, TGet>
    {
    }
}
