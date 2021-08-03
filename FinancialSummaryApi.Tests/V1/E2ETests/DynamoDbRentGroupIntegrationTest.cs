using AutoFixture;
using FinancialSummaryApi.V1.Boundary;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.E2ETests
{
    [Collection("Collection1")]
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

        [Fact]
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

        [Fact]
        public async Task HealchCheckOkReturns200()
        {
            var uri = new Uri($"api/v1/healthcheck/ping", UriKind.Relative);
            var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<HealthCheckResponse>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Message.Should().BeNull();
            apiEntity.Success.Should().BeTrue();
        }

        [Fact]
        public async Task CreateRentGroupCreatedReturns201()
        {
            var rentGroupDomain = ConstructRentGroupSummary();

            await CreateRentGroupAndValidateResponse(rentGroupDomain).ConfigureAwait(false);
        }

        [Fact]
        public async Task CreateRentGroupAndThenGetByRentGroupName()
        {
            var rentGroupDomain = ConstructRentGroupSummary();

            await CreateRentGroupAndValidateResponse(rentGroupDomain).ConfigureAwait(false);

            await GetRentGroupByRentGroupNameAndValidateResponse(rentGroupDomain).ConfigureAwait(false);
        }

        [Fact]
        public async Task CreateRentGroupBadRequestReturns400()
        {
            var rentGroupDomain = ConstructRentGroupSummary();

            rentGroupDomain.ArrearsYTD = -100;
            rentGroupDomain.ChargedYTD = -99;
            rentGroupDomain.PaidYTD = -500;
            rentGroupDomain.TotalCharged = -100;
            rentGroupDomain.TotalPaid = -200;
            rentGroupDomain.TargetType = TargetType.Block;

            var uri = new Uri("api/v1/rent-group-summary", UriKind.Relative);
            string body = JsonConvert.SerializeObject(rentGroupDomain);

            HttpResponseMessage response;
            using (StringContent stringContent = new StringContent(body))
            {
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await Client.PostAsync(uri, stringContent).ConfigureAwait(false);
            }

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<BaseErrorResponse>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.StatusCode.Should().Be(400);
            apiEntity.Details.Should().Be(string.Empty);

            apiEntity.Message.Should().Contain($"The field PaidYTD must be between 0 and {(double) decimal.MaxValue}.");
            apiEntity.Message.Should().Contain($"The field TotalPaid must be between 0 and {(double) decimal.MaxValue}.");
            apiEntity.Message.Should().Contain($"The field ArrearsYTD must be between 0 and {(double) decimal.MaxValue}.");
            apiEntity.Message.Should().Contain($"The field ChargedYTD must be between 0 and {(double) decimal.MaxValue}.");
            apiEntity.Message.Should().Contain($"The field TotalCharged must be between 0 and {(double) decimal.MaxValue}.");
            apiEntity.Message.Should().Contain("TargetType should be in a range: [3(RentGroup)].");
        }

        [Fact]
        public async Task CreateTwoRentGroupsGetAllReturns200()
        {
            var rentGroupDomains = new[] { ConstructRentGroupSummary(), ConstructRentGroupSummary() };

            foreach (var rentGroupDomain in rentGroupDomains)
            {
                await CreateRentGroupAndValidateResponse(rentGroupDomain).ConfigureAwait(false);

                await GetRentGroupByRentGroupNameAndValidateResponse(rentGroupDomain).ConfigureAwait(false);
            }

            var uri = new Uri("api/v1/rent-group-summary", UriKind.Relative);
            using var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<List<RentGroupSummaryResponse>>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Count.Should().BeGreaterOrEqualTo(2);

            var firstRentGroup = apiEntity.Find(r => r.RentGroupName.Equals(rentGroupDomains[0].RentGroupName));
            var secondRentGroup = apiEntity.Find(r => r.RentGroupName.Equals(rentGroupDomains[1].RentGroupName));

            firstRentGroup.ShouldBeEqualTo(rentGroupDomains[0]);
            secondRentGroup.ShouldBeEqualTo(rentGroupDomains[1]);
        }

        [Fact]
        public async Task CreateRentGroupAndGetForYesterdayReturns200()
        {
            var rentGroupDomain = ConstructRentGroupSummary();
            await SetupTestData(rentGroupDomain).ConfigureAwait(false);

            var uri = new Uri($"api/v1/asset-summary?submitDate={rentGroupDomain.SubmitDate.AddDays(-2):yyyy-MM-dd}", UriKind.Relative);
            using var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<List<RentGroupSummaryResponse>>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Should().HaveCountGreaterOrEqualTo(0);

            var foundRentGroup = apiEntity.FirstOrDefault(r => r.RentGroupName.Equals(rentGroupDomain.RentGroupName));

            foundRentGroup.Should().BeNull();
        }

        private async Task CreateRentGroupAndValidateResponse(RentGroupSummary rentGroupSummary)
        {
            var uri = new Uri("api/v1/rent-group-summary", UriKind.Relative);

            string body = JsonConvert.SerializeObject(rentGroupSummary);

            using StringContent stringContent = new StringContent(body);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var response = await Client.PostAsync(uri, stringContent).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<RentGroupSummaryResponse>(responseContent);

            CleanupActions.Add(async () => await DynamoDbContext.DeleteAsync<FinanceSummaryDbEntity>(apiEntity.Id).ConfigureAwait(false));

            apiEntity.Should().NotBeNull();

            apiEntity.Should().BeEquivalentTo(rentGroupSummary, options => options.Excluding(a => a.Id));
        }

        private async Task GetRentGroupByRentGroupNameAndValidateResponse(RentGroupSummary rentGroupSummary)
        {
            var uri = new Uri($"api/v1/rent-group-summary/{rentGroupSummary.RentGroupName}", UriKind.Relative);
            using var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<RentGroupSummaryResponse>(responseContent);

            apiEntity.ShouldBeEqualTo(rentGroupSummary);
        }
    }
}
