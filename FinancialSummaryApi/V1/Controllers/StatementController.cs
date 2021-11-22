using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/statements")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class StatementController : BaseController
    {
        private readonly IGetStatementListUseCase _getListUseCase;
        private readonly IAddStatementListUseCase _addListUseCase;
        private readonly IExportStatementUseCase _exportStatementUseCase;
        private readonly IExportSelectedStatementUseCase _exportSelectedItemUseCase;

        public StatementController(
            IGetStatementListUseCase getListUseCase,
            IAddStatementListUseCase addListUseCase,
            IExportStatementUseCase exportStatementUseCase,
            IExportSelectedStatementUseCase exportSelectedItemUseCase)
        {
            _getListUseCase = getListUseCase;
            _addListUseCase = addListUseCase;
            _exportStatementUseCase = exportStatementUseCase;
            _exportSelectedItemUseCase = exportSelectedItemUseCase;
        }

        /// <summary>
        /// Get a list of statements for specified asset
        /// </summary>
        /// <param name="correlationId">The value that is used to combine several requests into a common group</param>
        /// <param name="token">The jwt token value</param>
        /// <param name="assetId">The value by which we are looking for a list of statements</param>
        /// <param name="request">The parameter containing fields for pagination (page number and page size)  </param>
        /// <response code="200">Success. Statement models for specified asset were received successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(StatementListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("{assetId}")]
        public async Task<IActionResult> GetList([FromHeader(Name = "Authorization")] string token,
                                                [FromHeader(Name = "x-correlation-id")] string correlationId,
                                                [FromRoute] Guid assetId,
                                                [FromQuery] GetStatementListRequest request)
        {
            if (assetId == Guid.Empty)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "AssetId should be provided!"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, GetErrorMessage(ModelState)));
            }

            if (DatesArePartialProvided(request.StartDate, request.EndDate))
            {
                return BadRequest(new BaseErrorResponse(
                    (int) HttpStatusCode.BadRequest,
                    "StartDate and EndDate cannot be partial provided. Dates can be both empty or both provided"));
            }

            if (DatesProvidedAndInvalid(request.StartDate, request.EndDate))
            {
                return BadRequest(new BaseErrorResponse(
                    (int) HttpStatusCode.BadRequest,
                    "StartDate can't be greater than EndDate."));
            }

            var statementList = await _getListUseCase.ExecuteAsync(assetId, request).ConfigureAwait(false);

            return Ok(statementList);
        }

        /// <summary>
        /// Create new list of Statement models
        /// </summary>
        /// <param name="token">The jwt token value</param>
        /// <param name="correlationId">The value that is used to combine several requests into a common group</param>
        /// <param name="statements">List of Statement models for creation</param>
        /// <response code="201">Created. Statement models were created successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(StatementResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromHeader(Name = "Authorization")] string token,
                                                [FromHeader(Name = "x-correlation-id")] string correlationId,
                                                [FromBody] List<AddStatementRequest> statements)
        {
            if (statements == null || statements.Count == 0)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "Statement models cannot be null or empty"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, GetErrorMessage(ModelState)));
            }

            var resultStatements = await _addListUseCase.ExecuteAsync(statements).ConfigureAwait(false);

            return StatusCode((int) HttpStatusCode.Created, resultStatements);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("export")]
        public async Task<IActionResult> ExportStatementReportAsync([FromBody] ExportStatementRequest request)
        {
            var result = await _exportStatementUseCase.ExecuteAsync(request).ConfigureAwait(false);
            if (result == null)
                return NotFound("No record found");
            if (request?.FileType == "pdf")
            {
                return File(result, "application/pdf", $"{request.TypeOfStatement}_{DateTime.UtcNow.Ticks}.{request.FileType}");
            }
            return File(result, "text/csv", $"{request.TypeOfStatement}_{DateTime.UtcNow.Ticks}.{request.FileType}");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("selection/export")]
        public async Task<IActionResult> ExportSelectedItemAsync([FromBody] ExportSelectedStatementRequest request)
        {
            var result = await _exportSelectedItemUseCase.ExecuteAsync(request).ConfigureAwait(false);
            if (result == null)
                return NotFound("No record found");
            return File(result, "text/csv", $"export_{DateTime.UtcNow.Ticks}.csv");
        }
        private static bool DatesArePartialProvided(DateTime startDate, DateTime endDate)
            => startDate != DateTime.MinValue ^ endDate != DateTime.MinValue;

        private static bool DatesProvidedAndInvalid(DateTime startDate, DateTime endDate)
            => !DatesArePartialProvided(startDate, endDate) &&
               startDate > endDate;
    }
}
