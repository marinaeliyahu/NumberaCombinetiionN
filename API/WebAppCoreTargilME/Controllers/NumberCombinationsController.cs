using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAppCoreTargilME.ApiServices;
using WebAppCoreTargilME.Common.BaseModel;
using WebAppCoreTargilME.Common.ModelsData;
using WebAppCoreTargilME.Common.QueryData;

namespace WebAppCoreTargilME.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberCombinationsController : ControllerBase
    {
        private readonly INumberCombinationsService _service;
        private readonly ILogger<NumberCombinationsController> _logger;

        public NumberCombinationsController(ILogger<NumberCombinationsController> logger, INumberCombinationsService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        [SwaggerOperation("Get all Combinations for N")]
        [ProducesResponseType(typeof(BaseViewModel), 401)]
        [ProducesResponseType(typeof(BaseViewModel), 403)]
        [ProducesResponseType(statusCode: 200, Type = typeof(BaseViewModel<NumberCombinationsModel>))]
        public async Task<ActionResult<BaseViewModel<NumberCombinationsModel>>> GetAllAPI([FromQuery] QueryPaginationDataModel<NQuery> query)
        {
            var vm = await _service.GetAllAPI(query);
            if (vm == null)
                return NotFound();
            return Ok(vm);
        }

        [HttpPost]
        [Route("GetNextApi")]
        [SwaggerOperation("Get next combination")]
        [ProducesResponseType(typeof(BaseViewModel), 401)]
        [ProducesResponseType(typeof(BaseViewModel), 403)]
        [ProducesResponseType(statusCode: 200, Type = typeof(BaseViewModel<NumberCombinationsModel>))]
        public async Task<ActionResult<BaseViewModel<NumberCombinationsModel>>> GetNextApi([FromBody] QueryDataModel<GetNextApiQuery> query)
        {
            var vm = await _service.GetNextApi(query);
            if (vm == null)
                return NotFound();
            return Ok(vm);
        }

        [HttpGet]
        [Route("StartApi")]
        [SwaggerOperation("Get first Combinations for N and count")]
        [ProducesResponseType(typeof(BaseViewModel), 401)]
        [ProducesResponseType(typeof(BaseViewModel), 403)]
        [ProducesResponseType(statusCode: 200, Type = typeof(BaseViewModel<StartApiModel>))]
        public async Task<ActionResult<BaseViewModel<StartApiModel>>> GetStartApi([FromQuery] QueryDataModel<NQuery> query)
        {
            var vm = await _service.StartApi(query);
            if (vm == null)
                return NotFound();
            return Ok(vm);
        }

        [HttpPost]
        [Route("GetAllNextApiByCurrent")]
        [SwaggerOperation("Get next combination")]
        [ProducesResponseType(typeof(BaseViewModel), 401)]
        [ProducesResponseType(typeof(BaseViewModel), 403)]
        [ProducesResponseType(statusCode: 200, Type = typeof(BaseViewModel<NumberCombinationsModel>))]
        public async Task<ActionResult<BaseViewModel<NumberCombinationsModel>>> GetAllNextApiByCurrent([FromBody] QueryPaginationDataModel<GetNextApiQuery> query)
        {
            var vm = await _service.GetAllNextApiByCurrent(query);
            if (vm == null)
                return NotFound();
            return Ok(vm);
        }
    }
}
