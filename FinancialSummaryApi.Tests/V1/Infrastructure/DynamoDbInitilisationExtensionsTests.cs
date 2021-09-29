using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using FinancialSummaryApi.V1.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Infrastructure
{
    // Hanna Holasava, [03.08.2021]
    // We need to specify a collection for unit tests to prevent parallelism for E2E tests
    // https://xunit.net/docs/running-tests-in-parallel
    [Collection("MainCollection")]
    public class DynamoDbInitilisationExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("false")]
        [InlineData("true")]
        public void ConfigureDynamoDBTestNoLocalModeEnvVarUsesAWSService(string localModeEnvVar)
        {
            Environment.SetEnvironmentVariable("DynamoDb_LocalMode", localModeEnvVar);

            ServiceCollection services = new ServiceCollection();
            services.ConfigureDynamoDB();

            services.Any(x => x.ServiceType == typeof(IAmazonDynamoDB)).Should().BeTrue();
            services.Any(x => x.ServiceType == typeof(IDynamoDBContext)).Should().BeTrue();

            Environment.SetEnvironmentVariable("DynamoDb_LocalMode", null);
        }
    }
}
