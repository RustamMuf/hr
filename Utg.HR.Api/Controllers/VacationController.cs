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
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Services;

namespace Utg.HR.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("hr/[controller]")]
    public class VacationController : HrControllerBase
    {
        private readonly IVacationService _service;

        public VacationController(IVacationService service,
            ILogger<HrControllerBase> logger) : base(logger)
        {
            _service = service;
        }

        /// <summary>
        /// Получаем все отпуска 
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VacationViewModel>))]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] VacationClientRequest clientRequest)
        {
            try
            {
                var authRequest = GetAuthInfo();
                clientRequest.UserId = authRequest.UserProfileId;
                //руководитель
                if (authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRHead))
                {
                    string auth = Request.Headers["Authorization"].ToString();
                    clientRequest.Role = Common.Models.Domain.Enum.Role.HRHead;
                    return Ok(await _service.GetAllAsync(clientRequest, auth));
                }
                //Сотрудник
               else if (authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRUser))
                {
                    string auth = Request.Headers["Authorization"].ToString();
                    clientRequest.Role = Common.Models.Domain.Enum.Role.HRUser;
                    return Ok(await _service.GetAllAsync(clientRequest, auth));
                }
                //кадровик
                else if (authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRPersonal))
                {
                    string auth = Request.Headers["Authorization"].ToString();
                    clientRequest.Role = Common.Models.Domain.Enum.Role.HRPersonal;
                    return Ok(await _service.GetAllAsync(clientRequest, auth));
                }
                else
                {
                    StatusCode(StatusCodes.Status401Unauthorized);
                }
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Vacation][GetAll]");
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var model = _service.Delete(id);

                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Vacation][Delete]");
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Получаем все отпуска для теста 
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VacationViewModel>))]
        [Route("test")]
        [HttpGet]
        public IActionResult GetAllTest([FromQuery] VacationClientRequest clientRequest)
        {
            try
            {
                string auth = Request.Headers["Authorization"].ToString();
                return Ok( _service.GetAllTest(clientRequest, auth));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Vacation][GetAllTest]");
                return BadRequest(ex.ToString());
            }
        }

    }
}
