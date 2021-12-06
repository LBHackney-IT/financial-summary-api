using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Hackney.Core.DynamoDb;
using Hackney.Core.Testing.DynamoDb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FinancialSummaryApi.Tests
{
    public class MockWebApplicationFactory<TStartup>
       : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly List<TableDef> _tables = new List<TableDef>
        {
            new TableDef
            {
                Name = "FinancialSummaries",
                KeyName = "target_id",
                KeyType = ScalarAttributeType.S,
                RangeKeyName = "id",
                RangeKeyType = ScalarAttributeType.S
            }
        };

        public HttpClient Client { get; private set; }
        public IDynamoDbFixture DynamoDbFixture { get; private set; }

        public MockWebApplicationFactory()
        {
            EnsureEnvVarConfigured("DynamoDb_LocalMode", "true");
            EnsureEnvVarConfigured("DynamoDb_LocalServiceUrl", "http://localhost:8000");

            Client = CreateClient();
        }

        private bool _disposed;

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                if (null != DynamoDbFixture)
                    (DynamoDbFixture as DynamoDbFixture).Dispose();
                if (null != Client)
                    Client.Dispose();

                base.Dispose(true);

                _disposed = true;
            }
        }

        private static void EnsureEnvVarConfigured(string name, string defaultValue)
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(name)))
                Environment.SetEnvironmentVariable(name, defaultValue);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(b => b.AddEnvironmentVariables())
                .UseStartup<Startup>();
            builder.ConfigureServices(services =>
            {
                services.ConfigureDynamoDB();
                services.ConfigureDynamoDbFixture();

                var serviceProvider = services.BuildServiceProvider();

                DynamoDbFixture = serviceProvider.GetRequiredService<IDynamoDbFixture>();
                DynamoDbFixture.EnsureTablesExist(_tables);
            });
        }
    }
}
