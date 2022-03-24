using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using Hackney.Core.DynamoDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FinancialSummaryApi.V1.UseCase.Interfaces.Statements;

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
        private readonly IExportCsvStatementUseCase _exportCsvStatementUseCase;
        private readonly IExportPdfStatementUseCase _exportPdfStatementUseCase;
        private readonly IGetStatementByIdUseCase _getStatementByIdUseCase;
        private readonly IGetBatchSatementsByIdsUseCase _getBatchSatementsByIdsUseCase;

        public StatementController(
            IGetStatementListUseCase getListUseCase,
            IAddStatementListUseCase addListUseCase,
            IExportStatementUseCase exportStatementUseCase,
            IExportSelectedStatementUseCase exportSelectedItemUseCase,
            IExportCsvStatementUseCase exportCsvStatementUseCase,
            IExportPdfStatementUseCase exportPdfStatementUseCase,
            IGetStatementByIdUseCase getStatementByIdUseCase,
            IGetBatchSatementsByIdsUseCase getBatchSatementsByIdsUseCase)
        {
            _getListUseCase = getListUseCase;
            _addListUseCase = addListUseCase;
            _exportStatementUseCase = exportStatementUseCase;
            _exportSelectedItemUseCase = exportSelectedItemUseCase;
            _exportCsvStatementUseCase = exportCsvStatementUseCase;
            _exportPdfStatementUseCase = exportPdfStatementUseCase;
            _getStatementByIdUseCase = getStatementByIdUseCase;
            _getBatchSatementsByIdsUseCase = getBatchSatementsByIdsUseCase;
        }

        /// <summary>
        /// Load statement by id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="correlationId"></param>
        /// <param name="statementId"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PagedResult<StatementResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("single/{statementId}")]
        public async Task<IActionResult> GetById([FromHeader(Name = "Authorization")] string token,
                                                [FromHeader(Name = "x-correlation-id")] string correlationId,
                                                [FromRoute] Guid statementId, [FromQuery] Guid targetId)
        {
            if (statementId == Guid.Empty)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "Statement id cannot be null!"));
            }

            var statement = await _getStatementByIdUseCase.ExecuteAsync(statementId, targetId).ConfigureAwait(false);

            return Ok(statement);
        }

        /// <summary>
        /// Gets a list of statements by a list of targetIds
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<StatementResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("batch")]
        public async Task<IActionResult> GetBatchStatements([FromHeader(Name = "Authorization")] string token,
            [FromHeader(Name = "x-correlation-id")]
            string correlationId, [FromBody] GetBatchStatementsRequest request)
        {
            if (request == null)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "Request model cannot be empty!"));
            }

            if (request.TargetIds == null || request.TargetIds.Count == 0)
            {
                return Ok(new List<StatementResponse>());
            }

            var statements = await _getBatchSatementsByIdsUseCase.ExecuteAsync(request.TargetIds).ConfigureAwait(false);

            return Ok(statements);
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
        [ProducesResponseType(typeof(PagedResult<StatementResponse>), StatusCodes.Status200OK)]
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
            switch (request?.FileType)
            {
                case "pdf":
                    {
                        var pdfResult = await _exportPdfStatementUseCase.ExecuteAsync(request).ConfigureAwait(false);
                        if (pdfResult == null)
                            return NotFound($"No records found for the following ID: {request.TargetId}");
                        return Ok(pdfResult);
                    }

                case "csv":
                    {

                        var csvResult = await _exportCsvStatementUseCase.ExecuteAsync(request).ConfigureAwait(false);
                        if (csvResult == null)
                            return NotFound($"No records found for the following ID: {request.TargetId}");

                        return File(csvResult, "text/csv", $"{request.TypeOfStatement}_{DateTime.UtcNow.Ticks}.{request.FileType}");
                    }

                default:
                    return BadRequest("Format not supported");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("selection/export")]
        public async Task<IActionResult> ExportSelectedItemAsync([FromBody] ExportSelectedStatementRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, GetErrorMessage(ModelState)));
            }
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
