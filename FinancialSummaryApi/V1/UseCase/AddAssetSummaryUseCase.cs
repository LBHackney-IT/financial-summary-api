using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class AddAssetSummaryUseCase : IAddAssetSummaryUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        private readonly ITenureInfoDbGateway _tenureInfoGateway;

        public AddAssetSummaryUseCase(IFinanceSummaryGateway financeSummaryGateway,
            ITenureInfoDbGateway tenureInfoGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
            _tenureInfoGateway = tenureInfoGateway;
        }

        public async Task ExecuteAsync(AddAssetSummaryRequest assetSummary)
        {
            var domainModel = assetSummary.ToDomain();

            //var existentTenure = await _tenureInfoGateway.GetTenureInfoAsync(assetSummary.TargetId).ConfigureAwait(false);
            //if(existentTenure == null)
            //{
            //    throw new ArgumentException("Tenure info by provided targetId cannot be found!");
            //}

            domainModel.Id = Guid.NewGuid();

            await _financeSummaryGateway.AddAsync(domainModel).ConfigureAwait(false);
        }
    }
}
