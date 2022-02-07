using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FinancialSummaryApi.V1.Exceptions.Models;

namespace FinancialSummaryApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/rent-group-summary")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class RentGroupsController : BaseController
    {
        private readonly IAddRentGroupSummaryListUseCase _addListUseCase;
        private readonly IGetRentGroupSummaryByNameUseCase _getByNameUseCase;
        private readonly IGetAllRentGroupSummariesUseCase _getAllUseCase;

        public RentGroupsController(IAddRentGroupSummaryListUseCase addListUseCase,
            IGetRentGroupSummaryByNameUseCase getByNameUseCase,
            IGetAllRentGroupSummariesUseCase getAllUseCase)
        {
            _addListUseCase = addListUseCase;
            _getByNameUseCase = getByNameUseCase;
            _getAllUseCase = getAllUseCase;
        }

        /// <summary>
        /// Get a list of Rent Group summary models
        /// </summary>
        /// <param name="correlationId">The value that is used to combine several requests into a common group</param>
        /// <param name="token">The jwt token value</param>
        /// <param name="submitDate">The date when the requested data was generated</param>
        /// <response code="200">Rent Group summary models was received successfully</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(List<RentGroupSummaryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader(Name = "Authorization")] string token,
                                                [FromHeader(Name = "x-correlation-id")] string correlationId,
                                                [FromQuery] DateTime submitDate)
        {
            var rentGroups = await _getAllUseCase.ExecuteAsync(submitDate).ConfigureAwait(false);

            return Ok(rentGroups);
        }

        /// <summary>
        /// Get Rent Group summary model by provided groupName
        /// </summary>
        /// <param name="correlationId">The value that is used to combine several requests into a common group</param>
        /// <param name="token">The jwt token value</param>
        /// <param name="rentGroupName">The rent group name to get the data for</param>
        /// <param name="submitDate">The date when the requested data was generated</param>
        /// <response code="200">Rent Group summary models was received successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Rent Group with provided name cannot be found!</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(RentGroupSummaryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{rentGroupName}")]
        public async Task<IActionResult> Get([FromHeader(Name = "Authorization")] string token,
                                             [FromHeader(Name = "x-correlation-id")] string correlationId,
                                             [FromRoute] string rentGroupName,
                                             [FromQuery] DateTime submitDate)
        {
            var rentGroup = await _getByNameUseCase.ExecuteAsync(rentGroupName, submitDate).ConfigureAwait(false);

            if (rentGroup == null)
            {
                return NotFound(new BaseErrorResponse((int) HttpStatusCode.NotFound, "Rent Group with provided name cannot be found!"));
            }

            return Ok(rentGroup);
        }

        /// <summary>
        /// Create new list of rent group summary models
        /// </summary>
        /// <param name="token">The jwt token value</param>
        /// <param name="correlationId">The value that is used to combine several requests into a common group</param>
        /// <param name="summaryRequests">List of rent group summary models for creation</param>
        /// <response code="201">Created. Rent group summary models were created successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(StatementResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromHeader(Name = "Authorization")] string token,
                                                [FromHeader(Name = "x-correlation-id")] string correlationId,
                                                [FromBody] List<AddRentGroupSummaryRequest> summaryRequests)
        {
            if (summaryRequests == null || summaryRequests.Count == 0)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "Rent group summary models cannot be null or empty"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, GetErrorMessage(ModelState)));
            }

            var resultSummaries = await _addListUseCase.ExecuteAsync(summaryRequests).ConfigureAwait(false);

            return StatusCode((int) HttpStatusCode.Created, resultSummaries);
        }
    }
}
