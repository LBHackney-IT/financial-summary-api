using AutoFixture;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FinancialSummaryApi.Tests.V1.E2ETests
{
    public class DynamoDbRentGroupIntegrationTest : DynamoDbIntegrationTests<Startup>
    {
        private readonly Fixture _fixture = new Fixture();

        /// <summary>
        /// Method to construct a test entity that can be used in a test
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private RentGroupSummary ConstructRentGroupSummary()
        {
            var entity = _fixture.Create<RentGroupSummary>();

            entity.TargetType = TargetType.RentGroup;
            entity.SubmitDate = DateTime.UtcNow;

            return entity;
        }

        /// <summary>
        /// Method to add an entity instance to the database so that it can be used in a test.
        /// Also adds the corresponding action to remove the upserted data from the database when the test is done.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task SetupTestData(RentGroupSummary entity)
        {
            await DynamoDbContext.SaveAsync(entity.ToDatabase()).ConfigureAwait(false);

            CleanupActions.Add(async () => await DynamoDbContext.DeleteAsync<FinanceSummaryDbEntity>(entity.Id).ConfigureAwait(false));
        }

        [Test]
        public async Task GetRentGroupBydIdNotFoundReturns404()
        {
            string rentGroupName = "SomeInvalidName";

            var uri = new Uri($"api/v1/rent-group-summary/{rentGroupName}", UriKind.Relative);
            var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<BaseErrorResponse>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Message.Should().BeEquivalentTo("Rent Group with provided name cannot be found!");
            apiEntity.StatusCode.Should().Be(404);
            apiEntity.Details.Should().BeEquivalentTo(string.Empty);
        }
    }
}
