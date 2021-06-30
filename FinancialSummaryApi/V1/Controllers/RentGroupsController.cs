using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [ProducesResponseType(typeof(List<RentGroupSummaryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rentGroups = await _getAllUseCase.ExecuteAsync().ConfigureAwait(false);

            return Ok(rentGroups);
        }

        [ProducesResponseType(typeof(RentGroupSummaryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{rentGroupName}")]
        public async Task<IActionResult> Get([FromRoute] string rentGroupName)
        {
            var rentGroup = await _getByNameUseCase.ExecuteAsync(rentGroupName).ConfigureAwait(false);

            if(rentGroup == null)
            {
                return NotFound(new BaseErrorResponse("Rent Group with provided name cannot be found!"));
            }

            return Ok(rentGroup);
        }

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
