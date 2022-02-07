using AutoFixture;
using FinancialSummaryApi.V1.Boundary;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Exceptions.Models;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
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
    public class DynamoDbAssetsIntegrationTest : DynamoDbIntegrationTests<Startup>
    {
        private readonly Fixture _fixture = new Fixture();

        /// <summary>
        /// Method to construct a test entity that can be used in a test
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private AssetSummary ConstructAssetSummary()
        {
            var entity = _fixture.Create<AssetSummary>();

            entity.TargetType = TargetType.Block;
            entity.SubmitDate = DateTime.UtcNow;
            return entity;
        }

        /// <summary>
        /// Method to add an entity instance to the database so that it can be used in a test.
        /// Also adds the corresponding action to remove the upserted data from the database when the test is done.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task SetupTestData(AssetSummary entity)
        {
            await DynamoDbContext.SaveAsync(entity.ToDatabase()).ConfigureAwait(false);

            CleanupActions.Add(async () => await DynamoDbContext.DeleteAsync<AssetSummaryDbEntity>(entity.TargetId, entity.Id).ConfigureAwait(false));
        }

        [Fact]
        public async Task GetAssetByIdNotFoundReturns404()
        {
            Guid id = Guid.NewGuid();

            var uri = new Uri($"api/v1/asset-summary/{id}", UriKind.Relative);
            var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<BaseErrorResponse>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Message.Should().BeEquivalentTo("No Asset Summary by provided assetId cannot be found!");
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
        public async Task CreateAssetCreatedReturns201()
        {
            var assetDomain = ConstructAssetSummary();

            await CreateAssetAndValidateResponse(assetDomain).ConfigureAwait(false);
        }

        [Fact]
        public async Task CreateAssetWithSomeEmptyFieldsCreatedReturns201()
        {
            var request = new AssetSummary
            {
                TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                TargetType = TargetType.Estate,
                AssetName = "Estate 2",
                SubmitDate = new DateTime(2021, 7, 1)
            };

            await CreateAssetAndValidateResponse(request).ConfigureAwait(false);
        }

        [Fact]
        public async Task CreateAssetAndThenGetByTargetId()
        {
            var assetDomain = ConstructAssetSummary();

            var apiEntity = await CreateAssetAndValidateAndReturnResponse(assetDomain).ConfigureAwait(false);

            CleanupActions.Add(async () => await DynamoDbContext.DeleteAsync<AssetSummaryDbEntity>(apiEntity.TargetId, apiEntity.Id).ConfigureAwait(false));
            assetDomain.SubmitDate = apiEntity.SubmitDate;

            await GetAssetByTargetIdAndValidateResponse(assetDomain).ConfigureAwait(false);
        }

        [Fact]
        public async Task CreateAssetInvalidRequestReturns422()
        {
            var assetDomain = ConstructAssetSummary();

            assetDomain.TotalDwellingRent = -1;
            assetDomain.TotalNonDwellingRent = -1;
            assetDomain.TotalRentalServiceCharge = -1;
            assetDomain.TotalServiceCharges = -1;
            assetDomain.TotalIncome = -1;
            assetDomain.TotalExpenditure = -1;
            assetDomain.AssetName = string.Empty;

            var uri = new Uri($"api/v1/asset-summary", UriKind.Relative);
            string body = JsonConvert.SerializeObject(assetDomain);

            HttpResponseMessage response;
            using (StringContent stringContent = new StringContent(body))
            {
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await Client.PostAsync(uri, stringContent).ConfigureAwait(false);
            }

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<BaseErrorResponse>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
            apiEntity.Details.Should().Be(string.Empty);

            var errorList = apiEntity.Errors.Select(l => l.Message).ToList();
            errorList.Should().Contain("The AssetName field is required.");
            errorList.Should().Contain($"The field TotalDwellingRent must be between 0 and {(double) decimal.MaxValue}.");
            errorList.Should().Contain($"The field TotalServiceCharges must be between 0 and {(double) decimal.MaxValue}.");
            errorList.Should().Contain($"The field TotalNonDwellingRent must be between 0 and {(double) decimal.MaxValue}.");
            errorList.Should().Contain($"The field TotalRentalServiceCharge must be between 0 and {(double) decimal.MaxValue}.");
            errorList.Should().Contain($"The field TotalIncome must be between 0 and {(double) decimal.MaxValue}.");
            errorList.Should().Contain($"The field TotalExpenditure must be between 0 and {(double) decimal.MaxValue}.");
        }

        [Fact]
        public async Task CreateTwoAssetsGetAllReturns200()
        {
            var assetDomains = new[] { ConstructAssetSummary(), ConstructAssetSummary() };

            foreach (var asset in assetDomains)
            {
                asset.TargetId = assetDomains[0].TargetId;
                var createdResponse = await CreateAssetAndValidateAndReturnResponse(asset).ConfigureAwait(false);
            }

            var uri = new Uri($"api/v1/asset-summary?targetId={assetDomains[0].TargetId}", UriKind.Relative);
            using var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<List<AssetSummaryResponse>>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Count.Should().BeGreaterOrEqualTo(2);

            var firstAsset = apiEntity.Find(a => a.TargetId == assetDomains[0].TargetId);
            var secondAsset = apiEntity.Find(a => a.TargetId == assetDomains[1].TargetId);
        }

        [Fact]
        public async Task CreateAssetAndGetForYesterdayReturns200()
        {
            var asset = ConstructAssetSummary();
            await SetupTestData(asset).ConfigureAwait(false);

            var uri = new Uri($"api/v1/asset-summary?submitDate={asset.SubmitDate.AddDays(-2):yyyy-MM-dd}", UriKind.Relative);
            using var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<List<AssetSummaryResponse>>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Count.Should().BeGreaterOrEqualTo(0);

            var foundAsset = apiEntity.FirstOrDefault(a => a.TargetId == asset.TargetId);

            foundAsset.Should().BeNull();
        }

        private async Task CreateAssetAndValidateResponse(AssetSummary assetSummary)
        {
            var uri = new Uri($"api/v1/asset-summary", UriKind.Relative);

            string body = JsonConvert.SerializeObject(assetSummary);

            using StringContent stringContent = new StringContent(body);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var response = await Client.PostAsync(uri, stringContent).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<AssetSummaryResponse>(responseContent);

            CleanupActions.Add(async () => await DynamoDbContext.DeleteAsync<AssetSummaryDbEntity>(apiEntity.TargetId, apiEntity.Id).ConfigureAwait(false));

            apiEntity.Should().NotBeNull();

            apiEntity.Should().BeEquivalentTo(assetSummary, options => options.Excluding(a => a.Id));
        }

        private async Task<AssetSummaryResponse> CreateAssetAndValidateAndReturnResponse(AssetSummary assetSummary)
        {
            var uri = new Uri($"api/v1/asset-summary", UriKind.Relative);

            string body = JsonConvert.SerializeObject(assetSummary);

            using StringContent stringContent = new StringContent(body);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var response = await Client.PostAsync(uri, stringContent).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<AssetSummaryResponse>(responseContent);

            CleanupActions.Add(async () => await DynamoDbContext.DeleteAsync<AssetSummaryDbEntity>(apiEntity.TargetId, apiEntity.Id).ConfigureAwait(false));
            return apiEntity;
        }

        private async Task GetAssetByTargetIdAndValidateResponse(AssetSummary assetSummary)
        {
            var submitDate = assetSummary.SubmitDate.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ");
            var uri = new Uri($"api/v1/asset-summary/{assetSummary.TargetId}?submitDate={submitDate}", UriKind.Relative);
            using var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<AssetSummaryResponse>(responseContent);

            apiEntity.ShouldBeEqualTo(assetSummary);
        }
    }
}
