using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class ImageRepository
        : RepositoryBase<Image, long>, IImageRepository
    {
        public ImageRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}