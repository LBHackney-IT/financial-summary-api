using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        /// <param name="submitDate">The date when the requested data was generated</param>
        /// <response code="200">Rent Group summary models was received successfully</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(List<RentGroupSummaryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime submitDate)
        {
            var rentGroups = await _getAllUseCase.ExecuteAsync(submitDate).ConfigureAwait(false);

            return Ok(rentGroups);
        }

        /// <summary>
        /// Get Rent Group summary model by provided groupName
        /// </summary>
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
        public async Task<IActionResult> Get([FromRoute] string rentGroupName, [FromQuery] DateTime submitDate)
        {
            var rentGroup = await _getByNameUseCase.ExecuteAsync(rentGroupName, submitDate).ConfigureAwait(false);

            if(rentGroup == null)
            {
                return NotFound(new BaseErrorResponse("Rent Group with provided name cannot be found!"));
            }

            return Ok(rentGroup);
        }

        /// <summary>
        /// Create new Rent Group summary model
        /// </summary>
        /// <param name="summaryRequest"></param>
        /// <response code="200">Rent Group summary model was created successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(RentGroupSummaryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AddRentGroupSummaryRequest summaryRequest)
        {
            if (summaryRequest == null)
            {
                return BadRequest(new BaseErrorResponse("Rent Group Summary model cannot be null"));
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(new BaseErrorResponse(GetErrorMessage(ModelState)));
            }

            await _addUseCase.ExecuteAsync(summaryRequest).ConfigureAwait(false);

            // ToDo: join with asset table to get AssetId
            return RedirectToAction("Get", new { rentGroupName = summaryRequest.RentGroupName });
        }
    }
}
