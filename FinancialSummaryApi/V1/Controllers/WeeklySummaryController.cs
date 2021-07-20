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
    [Route("api/v1/weekly-summary")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class WeeklySummaryController : BaseController
    {
        private readonly IGetAllWeeklySummariesUseCase _getAllWeeklySummariesUseCase;
        private readonly IAddWeeklySummaryUseCase _addWeeklySummaryUseCase;
        private readonly IGetWeeklySummaryByIdUseCase _getWeeklySummaryByIdUseCase;

        public WeeklySummaryController(IGetAllWeeklySummariesUseCase getAllWeeklySummariesUseCase,
            IAddWeeklySummaryUseCase addWeeklySummaryUseCase,
            IGetWeeklySummaryByIdUseCase getWeeklySummaryByIdUseCase)
        {
            _getAllWeeklySummariesUseCase = getAllWeeklySummariesUseCase;
            _addWeeklySummaryUseCase = addWeeklySummaryUseCase;
            _getWeeklySummaryByIdUseCase = getWeeklySummaryByIdUseCase;
        }

        /// <summary>
        /// Get Weekly summary model by provided Id
        /// </summary>
        /// <param name="id">The id by which we are looking for an weekly summary</param>
        /// <response code="200">Success. Weekly summary models is saved successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Weekly Summary with provided id cannot be found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(WeeklySummaryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var weeklySummary = await _getWeeklySummaryByIdUseCase.ExecuteAsync(id).ConfigureAwait(false);

            if (weeklySummary == null)
            {
                return NotFound(new BaseErrorResponse((int) HttpStatusCode.NotFound, "Weekly Summary by provided Id not found!"));
            }

            return Ok(weeklySummary);
        }

        /// <summary>
        /// Get Weekly Transaction summary for a target id and given start date and end date range
        /// </summary>
        /// <param name="targetId">The target id for which we want the summaries result</param>
        /// <param name="startDate">The start date from when we want to filter for weekly summaries</param>
        /// <param name="endDate">The end date untill whene want to filter for weekly summaries</param>
        /// <response code="200">Success. Weekly summaries received successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Weekly Summary with provided id cannot be found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(List<WeeklySummaryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid targetId, [FromQuery] string startDate, [FromQuery] string endDate)
        {
            var weeklySummary = await _getAllWeeklySummariesUseCase.ExecuteAsync(targetId, startDate, endDate).ConfigureAwait(false);

            if (weeklySummary == null)
            {
                return NotFound(new BaseErrorResponse((int) HttpStatusCode.NotFound, "No Weekly Summary cannot be found!"));
            }

            return Ok(weeklySummary);
        }

        /// <summary>
        /// Create new Weekly summary model
        /// </summary>
        /// <param name="weeklySummary">Weekly summary model for create</param>
        /// <response code="201">Created. Weekly summary model was created successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(WeeklySummaryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWeeklySummaryRequest weeklySummary)
        {
            if (weeklySummary == null)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "WeeklySummary model cannot be null"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, GetErrorMessage(ModelState)));
            }

            var resultSummary = await _addWeeklySummaryUseCase.ExecuteAsync(weeklySummary).ConfigureAwait(false);

            return CreatedAtAction("Get", new { id = resultSummary.Id }, resultSummary);
        }
    }
}
