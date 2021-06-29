using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/asset-summary")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class AssetSummaryController : BaseController
    {
        private readonly IGetAllAssetSummariesUseCase _getAllUseCase;
        private readonly IGetAssetSummaryByIdUseCase _getByIdUseCase;
        private readonly IAddAssetSummaryUseCase _addUseCase;
        private readonly IUpdateAssetSummaryUseCase _updateUseCase;

        public AssetSummaryController(
            IGetAllAssetSummariesUseCase getAllUseCase,
            IGetAssetSummaryByIdUseCase getByIdUseCase,
            IAddAssetSummaryUseCase addAssetSummaryUseCase,
            IUpdateAssetSummaryUseCase updateUseCase)
        {
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _addUseCase = addAssetSummaryUseCase;
            _updateUseCase = updateUseCase;
        }

        /// <summary>
        /// Get a list of Asset summary models
        /// </summary>
        /// <response code="200">Success. Asset summary models was received successfully</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(AssetSummaryListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var assetSummaries = await _getAllUseCase.ExecuteAsync().ConfigureAwait(false);

            return Ok(assetSummaries);
        }

        /// <summary>
        /// Get Asset summary model by provided assetId
        /// </summary>
        /// <response code="200">Success. Asset summary models was received successfully</response>
        /// <response code="404">Asset with provided id cannot be found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(AssetSummaryListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{assetId}")]
        public async Task<IActionResult> Get([FromRoute]Guid assetId)
        {
            var assetSummary = await _getByIdUseCase.ExecuteAsync(assetId).ConfigureAwait(false);

            if (assetSummary == null)
            {
                return NotFound();
            }

            return Ok(assetSummary);
        }

        /// <summary>
        /// Create new Asset summary model
        /// </summary>
        /// <response code="200">Success. Asset summary model was created successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(AssetSummaryListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddAssetSummaryRequest assetSummary)
        {
            if (assetSummary == null)
            {
                return BadRequest("AssetSummary model cannot be null");
            }

            await _addUseCase.ExecuteAsync(assetSummary).ConfigureAwait(false);

            // ToDo: join with asset table to get AssetId
            return RedirectToAction("Get", new { assetId = assetSummary.TargetId });
        }

        /// <summary>
        /// Updates Asset summary model by provided assetId
        /// </summary>
        /// <response code="200">Success. Asset summary model was updated successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(AssetSummaryListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("{assetId}")]
        public async Task<IActionResult> Update([FromRoute]Guid assetId, [FromBody] UpdateAssetSummaryRequest assetSummary)
        {
            if (assetSummary == null)
            {
                return BadRequest("AssetSummary model cannot be null");
            }

            var existentAssetSummatyModel = await _getByIdUseCase.ExecuteAsync(assetId).ConfigureAwait(false);
            if(existentAssetSummatyModel == null)
            {
                return NotFound("AssetModel with provided assetId cannot be found!");
            }

            if(existentAssetSummatyModel.Id != assetSummary.Id)
            {
                return BadRequest("Summary record by provided assetId and provided UpdateAssetModel don't match");
            }

            await _updateUseCase.ExecuteAsync(assetSummary).ConfigureAwait(false);

            // ToDo: join with asset table to get AssetId
            return RedirectToAction("Get", new { assetId = assetSummary.TargetId });
        }
    }
}
