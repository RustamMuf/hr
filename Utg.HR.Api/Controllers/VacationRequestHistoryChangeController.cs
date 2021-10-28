using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Services;

namespace Utg.HR.API.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("hr/[controller]")]
    public class VacationRequestHistoryChangeController : HrControllerBase
    {
        private readonly IVacationRequestHistoryChangeService _service;

        public VacationRequestHistoryChangeController(IVacationRequestHistoryChangeService service,
            ILogger<HrControllerBase> logger) : base(logger)
        {
            _service = service;
        }

        /// <summary>
        /// Получаем все статусы
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VacationRequestHistoryChangeViewModel>))]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] VacationRequestHistoryChangeClientRequest clientRequest)
        {
            try
            {
                return Ok(await _service.GetAllStatuses(clientRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[VacationRequestHistoryChange][GetAll]");
                return BadRequest(ex.ToString());
            }
        }
    }
}
