using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FinancialSummaryApi.Tests
{
    public class DynamoDbIntegrationTests<TStartup> : IDisposable where TStartup : class
    {
        protected HttpClient Client { get; private set; }
        private readonly DynamoDbMockWebApplicationFactory<TStartup> _factory;
        protected IDynamoDBContext DynamoDbContext => _factory?.DynamoDbContext;
        protected List<Action> CleanupActions { get; set; }

        private readonly List<TableDef> _tables = new List<TableDef>
        {
            new TableDef { Name = "FinancialSummaries", KeyName = "id", KeyType = ScalarAttributeType.S },
            new TableDef { Name = "TransactionSummaries", KeyName = "id", KeyType = ScalarAttributeType.S }
        };

        private static void EnsureEnvVarConfigured(string name, string defaultValue)
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(name)))
            {
                Environment.SetEnvironmentVariable(name, defaultValue);
            }
        }

        public DynamoDbIntegrationTests()
        {
            EnsureEnvVarConfigured("DynamoDb_LocalMode", "true");
            EnsureEnvVarConfigured("DynamoDb_LocalServiceUrl", "http://localhost:8000");
            EnsureEnvVarConfigured("DynamoDb_LocalSecretKey", "8kmm3g");
            EnsureEnvVarConfigured("DynamoDb_LocalAccessKey", "fco1i2");
            _factory = new DynamoDbMockWebApplicationFactory<TStartup>(_tables);

            Client = _factory.CreateClient();
            CleanupActions = new List<Action>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                foreach (var act in CleanupActions)
                {
                    act();
                }
                Client.Dispose();

                if (null != _factory)
                    _factory.Dispose();
                _disposed = true;
            }
        }
    }

    public class TableDef
    {
        public string Name { get; set; }
        public string KeyName { get; set; }
        public ScalarAttributeType KeyType { get; set; }
    }
}
