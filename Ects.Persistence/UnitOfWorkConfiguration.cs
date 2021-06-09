using System;
using Ects.Persistence.Abstractions;

namespace Ects.Persistence
{
    public class UnitOfWorkConfiguration
        : IUnitOfWorkConfiguration
    {
        public string ConnectionString { get; }

        public UnitOfWorkConfiguration(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException(nameof(connectionString));

            ConnectionString = connectionString;
        }
    }
}