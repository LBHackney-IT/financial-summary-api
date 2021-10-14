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
        private readonly IGetStatementsListUseCase _getListUseCase;
        private readonly IAddStatementUseCase _addUseCase;

        public StatementController(
            IGetStatementsListUseCase getListUseCase,
            IAddStatementUseCase addStatementUseCase)
        {
            _getListUseCase = getListUseCase;
            _addUseCase = addStatementUseCase;
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
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(GetStatementListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("{assetId}")]
        public async Task<IActionResult> GetList([FromHeader(Name = "Authorization")] string token,
                                                [FromHeader(Name = "x-correlation-id")] string correlationId,
                                                [FromRoute] Guid assetId,
                                                [FromQuery] GetStatementListRequest request) 
        {
            if(assetId == Guid.Empty)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "AssetId should be provided!"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, GetErrorMessage(ModelState)));
            }

            var statementsList = await _getListUseCase.ExecuteAsync(assetId, request).ConfigureAwait(false);

            if(statementsList == null)
            {
                return NotFound(new BaseErrorResponse((int) HttpStatusCode.NotFound, "No statements for provided assetId found!"));
            }

            return Ok(statementsList);
        }

        /// <summary>
        /// Create new Statement model
        /// </summary>
        /// <param name="token">The jwt token value</param>
        /// <param name="correlationId">The value that is used to combine several requests into a common group</param>
        /// <param name="statement">Statement model for create</param>
        /// <response code="201">Created. Statement model was created successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(AssetSummaryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromHeader(Name = "Authorization")] string token,
                                                [FromHeader(Name = "x-correlation-id")] string correlationId,
                                                [FromBody] AddStatementRequest statement)
        {
            if (statement == null)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "Statement model cannot be null"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, GetErrorMessage(ModelState)));
            }

            var resultStatement = await _addUseCase.ExecuteAsync(statement).ConfigureAwait(false);

            return Created((Uri)null, resultStatement);
        }
    }
}
