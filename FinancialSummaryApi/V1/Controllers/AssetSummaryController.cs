using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using Hackney.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
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
        private readonly IGetAssetSummaryByIdAndYearUseCase _getAssetSummaryByIdAndYearUseCase;
        private readonly IAddAssetSummaryUseCase _addUseCase;

        public AssetSummaryController(
            IGetAllAssetSummariesUseCase getAllUseCase,
            IGetAssetSummaryByIdUseCase getByIdUseCase,
            IGetAssetSummaryByIdAndYearUseCase getAssetSummaryByIdAndYearUseCase,
            IAddAssetSummaryUseCase addAssetSummaryUseCase)
        {
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _getAssetSummaryByIdAndYearUseCase = getAssetSummaryByIdAndYearUseCase;
            _addUseCase = addAssetSummaryUseCase;
        }

        /// <summary>
        /// Get a list of Asset summary models
        /// </summary>
        /// <param name="correlationId">The value that is used to combine several requests into a common group</param>
        /// <param name="token">The jwt token value</param>
        /// <param name="targetId"></param>
        /// <param name="submitDate">The date when the requested data was generated</param>
        /// <response code="200">Success. Asset summary models was received successfully</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(List<AssetSummaryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        //[LogCall(LogLevel.Information)]
        public async Task<IActionResult> GetAll([FromHeader(Name = "Authorization")] string token,
                                                [FromHeader(Name = "x-correlation-id")] string correlationId,
                                                [FromQuery] Guid targetId, [FromQuery] DateTime submitDate)
        {
            var assetSummaries = await _getAllUseCase.ExecuteAsync(targetId, submitDate).ConfigureAwait(false);

            return Ok(assetSummaries);
        }

        /// <summary>
        /// Get Asset summary model by provided assetId
        /// </summary>
        /// <param name="correlationId">The value that is used to combine several requests into a common group</param>
        /// <param name="token">The jwt token value</param>
        /// <param name="submitDate">The date when the requested data was generated</param>
        /// <param name="assetId">The value by which we are looking for an asset summary</param>
        /// <response code="200">Success. Asset summary models was received successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Asset with provided id cannot be found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(AssetSummaryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{assetId}")]
        public async Task<IActionResult> Get([FromHeader(Name = "Authorization")] string token,
                                             [FromHeader(Name = "x-correlation-id")] string correlationId,
                                             [FromRoute] Guid assetId,
                                             [FromQuery] DateTime submitDate)
        {
            var assetSummary = await _getByIdUseCase.ExecuteAsync(assetId, submitDate).ConfigureAwait(false);

            if (assetSummary == null)
            {
                return NotFound(new BaseErrorResponse((int) HttpStatusCode.NotFound, "No Asset Summary by provided assetId cannot be found!"));
            }

            return Ok(assetSummary);
        }

        /// <summary>
        /// Get Asset summary model by provided assetId
        /// </summary>
        /// <param name="correlationId">The value that is used to combine several requests into a common group</param>
        /// <param name="token">The jwt token value</param>
        /// <param name="summaryYear">The date when the requested data was generated</param>
        /// <param name="assetId">The value by which we are looking for an asset summary</param>
        /// <response code="200">Success. Asset summary models was received successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Asset with provided id cannot be found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(AssetSummaryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("estimates/{assetId}")]
        public async Task<IActionResult> Get([FromHeader(Name = "Authorization")] string token,
                                             [FromHeader(Name = "x-correlation-id")] string correlationId,
                                             [FromRoute] Guid assetId,
                                             [FromQuery] short summaryYear)
        {
            var assetSummary = await _getAssetSummaryByIdAndYearUseCase.ExecuteAsync(assetId, summaryYear).ConfigureAwait(false);

            if (assetSummary == null)
            {
                return NotFound(new BaseErrorResponse((int) HttpStatusCode.NotFound, "No Asset Summary by provided assetId cannot be found!"));
            }

            return Ok(assetSummary);
        }


        /// <summary>
        /// Create new Asset summary model
        /// </summary>
        /// <param name="token">The jwt token value</param>
        /// <param name="correlationId">The value that is used to combine several requests into a common group</param>
        /// <param name="assetSummary">Asset summary model for create</param>
        /// <response code="201">Created. Asset summary model was created successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(AssetSummaryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromHeader(Name = "Authorization")] string token,
                                                [FromHeader(Name = "x-correlation-id")] string correlationId,
                                                [FromBody] AddAssetSummaryRequest assetSummary)
        {
            if (assetSummary == null)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "AssetSummary model cannot be null"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, GetErrorMessage(ModelState)));
            }

            var resultAsset = await _addUseCase.ExecuteAsync(assetSummary).ConfigureAwait(false);

            return CreatedAtAction("Get", new { assetId = assetSummary.TargetId }, resultAsset);
        }
    }
}
