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
    [Route("api/v1/rent-group-summary")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class RentGroupsController : BaseController
    {
        private readonly IAddRentGroupSummaryUseCase _addUseCase;
        private readonly IGetRentGroupSummaryByNameUseCase _getByNameUseCase;
        private readonly IGetAllRentGroupSummariesUseCase _getAllUseCase;

        public RentGroupsController(IAddRentGroupSummaryUseCase addUseCase,
            IGetRentGroupSummaryByNameUseCase getByNameUseCase,
            IGetAllRentGroupSummariesUseCase getAllUseCase)
        {
            _addUseCase = addUseCase;
            _getByNameUseCase = getByNameUseCase;
            _getAllUseCase = getAllUseCase;
        }

        /// <summary>
        /// Get a list of Rent Group summary models
        /// </summary>
        /// <param name="correlationId">The value that is used to combine several requests into a common group</param>
        /// <param name="apiKey">The api key value</param>
        /// <param name="submitDate">The date when the requested data was generated</param>
        /// <response code="200">Rent Group summary models was received successfully</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(List<RentGroupSummaryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader(Name = "x-api-key")] string apiKey,
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
        /// <param name="apiKey">The api key value</param>
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
        public async Task<IActionResult> Get([FromHeader(Name = "x-api-key")] string apiKey,
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
        /// Create new Rent Group summary model
        /// </summary>
        /// <param name="correlationId">The value that is used to combine several requests into a common group</param>
        /// <param name="apiKey">The api key value</param>
        /// <param name="summaryRequest">Rent Group summary model for create</param>
        /// <response code="201">Rent Group summary model was created successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(RentGroupSummaryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromHeader(Name = "x-api-key")] string apiKey,
                                                [FromHeader(Name = "x-correlation-id")] string correlationId,
                                                [FromBody] AddRentGroupSummaryRequest summaryRequest)
        {
            if (summaryRequest == null)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "Rent Group Summary model cannot be null"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, GetErrorMessage(ModelState)));
            }

            var resultRentGroupSummary = await _addUseCase.ExecuteAsync(summaryRequest).ConfigureAwait(false);

            return CreatedAtAction("Get", new { rentGroupName = summaryRequest.RentGroupName }, resultRentGroupSummary);
        }
    }
}
