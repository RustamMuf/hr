using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Services;

namespace Utg.HR.API.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("hr/[controller]")]
    public class BalanceVacationController : HrControllerBase
    {
        private readonly IBalanceVacationService _service;

        public BalanceVacationController(IBalanceVacationService service,
            ILogger<HrControllerBase> logger) : base(logger)
        {
            _service = service;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BalanceVacationViewModel))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok( await _service.GetBalanceById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[BalanceVacation][GetById]");
                return BadRequest(ex.ToString());
            }

        }
    }
}
