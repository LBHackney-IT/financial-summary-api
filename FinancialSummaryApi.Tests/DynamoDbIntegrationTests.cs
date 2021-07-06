using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FinancialSummaryApi.Tests
{
    public class DynamoDbIntegrationTests<TStartup> where TStartup : class
    {
        protected HttpClient Client { get; private set; }
        private DynamoDbMockWebApplicationFactory<TStartup> _factory;
        protected IDynamoDBContext DynamoDbContext => _factory?.DynamoDbContext;
        protected List<Action> CleanupActions { get; set; }

        private readonly List<TableDef> _tables = new List<TableDef>
        {
            new TableDef { Name = "finance_summary_table", KeyName = "id", KeyType = ScalarAttributeType.S }
        };

        private static void EnsureEnvVarConfigured(string name, string defaultValue)
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(name)))
            {
                Environment.SetEnvironmentVariable(name, defaultValue);
            }
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            EnsureEnvVarConfigured("DynamoDb_LocalMode", "true");
            EnsureEnvVarConfigured("DynamoDb_LocalServiceUrl", "http://localhost:8000");
            EnsureEnvVarConfigured("DynamoDb_LocalSecretKey", "8kmm3g");
            EnsureEnvVarConfigured("DynamoDb_LocalAccessKey", "fco1i2");
            _factory = new DynamoDbMockWebApplicationFactory<TStartup>(_tables);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _factory.Dispose();
        }

        [SetUp]
        public void BaseSetup()
        {
            Client = _factory.CreateClient();
            CleanupActions = new List<Action>();
        }

        [TearDown]
        public void BaseTearDown()
        {
            foreach (var act in CleanupActions)
            {
                act();
            }
            Client.Dispose();
        }
    }

    public class TableDef
    {
        public string Name { get; set; }
        public string KeyName { get; set; }
        public ScalarAttributeType KeyType { get; set; }
    }
}
