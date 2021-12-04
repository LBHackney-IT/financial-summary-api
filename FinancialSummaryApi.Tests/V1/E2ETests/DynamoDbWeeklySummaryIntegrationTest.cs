using AutoFixture;
using FinancialSummaryApi.V1.Boundary;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Controllers;
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
    // Hanna Holasava, [03.08.2021]
    // We need to specify a collection for unit tests to prevent parallelism for E2E tests
    // https://xunit.net/docs/running-tests-in-parallel
    [Collection("MainCollection")]
    public class DynamoDbWeeklySummaryIntegrationTest : DynamoDbIntegrationTests<Startup>
    {
        private readonly Fixture _fixture = new Fixture();

        /// <summary>
        /// Method to construct a test entity that can be used in a test
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private WeeklySummary ConstructWeeklySummary()
        {
            var entity = _fixture.Create<WeeklySummary>();

            return entity;
        }

        /// <summary>
        /// Method to add an entity instance to the database so that it can be used in a test.
        /// Also adds the corresponding action to remove the upserted data from the database when the test is done.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task SetupTestData(WeeklySummary entity)
        {
            await DynamoDbContext.SaveAsync(entity.ToDatabase()).ConfigureAwait(false);

            CleanupActions.Add(async () => await DynamoDbContext.DeleteAsync<WeeklySummaryDbEntity>(entity.TargetId, entity.Id).ConfigureAwait(false));
        }

        [Fact]
        public async Task GetWeeklySummaryByIdNotFoundReturns404()
        {
            Guid id = Guid.NewGuid();

            var uri = new Uri($"api/v1/weekly-summary/{id}", UriKind.Relative);
            var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<BaseErrorResponse>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Message.Should().BeEquivalentTo("Weekly Summary by provided Id not found!");
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
        public async Task CreateWeeklySummaryCreatedReturns201()
        {
            var weeklySummaryDomain = ConstructWeeklySummary();

            await CreateWeeklySummaryAndValidateResponse(weeklySummaryDomain).ConfigureAwait(false);
        }

        [Fact]
        public async Task CreateWeeklySummaryAndThenGetByTargetId()
        {
            var weeklySummaryDomain = ConstructWeeklySummary();

            var createdEntity = await CreateWeeklySummaryAndValidateResponse(weeklySummaryDomain).ConfigureAwait(false);
            weeklySummaryDomain.Id = createdEntity.Id;
            await GetWeeklySummaryByTargetIdAndValidateResponse(weeklySummaryDomain).ConfigureAwait(false);
        }

        [Fact]
        public async Task CreateWeeklySummaryBadRequestReturns400()
        {
            var weeklySummaryDomain = ConstructWeeklySummary();

            weeklySummaryDomain.PeriodNo = -1;
            weeklySummaryDomain.FinancialYear = -1;
            weeklySummaryDomain.FinancialMonth = -1;
            weeklySummaryDomain.ChargedAmount = -1;
            weeklySummaryDomain.PaidAmount = -1;
            weeklySummaryDomain.BalanceAmount = -1;
            weeklySummaryDomain.HousingBenefitAmount = -1;
            weeklySummaryDomain.WeekStartDate = DateTime.MinValue;

            var uri = new Uri($"api/v1/weekly-summary", UriKind.Relative);
            string body = JsonConvert.SerializeObject(weeklySummaryDomain);

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

            apiEntity.Message.Should().Contain("The field PeriodNo must be between 0 and 32767.");
            apiEntity.Message.Should().Contain("The field FinancialYear must be between 0 and 32767.");
            apiEntity.Message.Should().Contain("The field FinancialMonth must be between 0 and 32767.");
            apiEntity.Message.Should().Contain($"The field PaidAmount must be between 0 and {(double) decimal.MaxValue}.");
            apiEntity.Message.Should().Contain($"The field BalanceAmount must be between 0 and {(double) decimal.MaxValue}.");
            apiEntity.Message.Should().Contain($"The field ChargedAmount must be between 0 and {(double) decimal.MaxValue}.");
            apiEntity.Message.Should().Contain($"The field HousingBenefitAmount must be between 0 and {(double) decimal.MaxValue}.");
            apiEntity.Message.Should().Contain("WeekStartDate cannot be default value");
        }

        [Fact]
        public async Task CreateTwoWeeklySummaryGetAllReturns200()
        {
            var weeklySummaryDomains = new[] { ConstructWeeklySummary(), ConstructWeeklySummary() };

            var targetId = weeklySummaryDomains[0].TargetId;
            weeklySummaryDomains[1].TargetId = targetId;

            foreach (var summary in weeklySummaryDomains)
            {
                var createdEntity = await CreateWeeklySummaryAndValidateResponse(summary).ConfigureAwait(false);
                summary.Id = createdEntity.Id;
                await GetWeeklySummaryByTargetIdAndValidateResponse(summary).ConfigureAwait(false);
            }

            var uri = new Uri($"api/v1/weekly-summary?targetId={targetId}", UriKind.Relative);
            using var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<List<WeeklySummaryResponse>>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Count.Should().BeGreaterOrEqualTo(2);

            var firstSummary = apiEntity.Find(a => a.Id == weeklySummaryDomains[0].Id);
            var secondSummary = apiEntity.Find(a => a.Id == weeklySummaryDomains[1].Id);

            firstSummary.ShouldBeEqualTo(weeklySummaryDomains[0]);
            secondSummary.ShouldBeEqualTo(weeklySummaryDomains[1]);
        }

        [Fact]
        public async Task CreateWeeklySummaryAndGetForYesterdayReturns200()
        {
            var summary = ConstructWeeklySummary();
            await SetupTestData(summary).ConfigureAwait(false);

            var uri = new Uri($"api/v1/weekly-summary?targetId={summary.Id}&startDate={summary.WeekStartDate.AddDays(-2):yyyy-MM-dd}&endDate={summary.WeekStartDate.AddDays(2):yyyy-MM-dd}", UriKind.Relative);
            using var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<List<WeeklySummaryResponse>>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Count.Should().BeGreaterOrEqualTo(0);

            var foundSummary = apiEntity.FirstOrDefault(a => a.TargetId == summary.TargetId);

            foundSummary.Should().BeNull();
        }

        private async Task<WeeklySummaryResponse> CreateWeeklySummaryAndValidateResponse(WeeklySummary weeklySummary)
        {
            var uri = new Uri($"api/v1/weekly-summary", UriKind.Relative);

            string body = JsonConvert.SerializeObject(weeklySummary);

            using StringContent stringContent = new StringContent(body);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var response = await Client.PostAsync(uri, stringContent).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<WeeklySummaryResponse>(responseContent);

            CleanupActions.Add(async () => await DynamoDbContext.DeleteAsync<WeeklySummaryDbEntity>(apiEntity.TargetId, apiEntity.Id).ConfigureAwait(false));

            apiEntity.Should().NotBeNull();

            apiEntity.Should().BeEquivalentTo(weeklySummary, options => options
                                .Excluding(a => a.Id)
                                .Excluding(a => a.SubmitDate));
            return apiEntity;
        }

        private async Task GetWeeklySummaryByTargetIdAndValidateResponse(WeeklySummary weeklySummary)
        {
            var uri = new Uri($"api/v1/weekly-summary/{weeklySummary.Id}?targetId={weeklySummary.TargetId}", UriKind.Relative);
            using var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<WeeklySummaryResponse>(responseContent);

            apiEntity.ShouldBeEqualTo(weeklySummary);
        }
    }
}
