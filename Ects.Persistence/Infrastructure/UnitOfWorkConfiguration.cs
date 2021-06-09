using System;
using Art.Persistence.Infrastructure.Abstractions;

namespace Art.Persistence.Infrastructure
{
    public class UnitOfWorkConfiguration : IUnitOfWorkConfiguration
    {
        public string ConnectionString { get; }

        public UnitOfWorkConfiguration(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException(nameof(connectionString));
            }

            ConnectionString = connectionString;
        }
    }
}
