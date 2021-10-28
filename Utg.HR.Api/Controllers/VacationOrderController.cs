using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Models.Domain;
using Utg.HR.Common.Services;

namespace Utg.HR.API.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("hr/[controller]")]
    public class VacationOrderController : HrControllerBase
    {
        private readonly IVacationOrderService _service;

        public VacationOrderController(IVacationOrderService service,
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
        public async Task<IActionResult> GetAll([FromQuery] VacationOrderClientRequest clientRequest)
        {
            try
            {
                var authRequest = GetAuthInfo();
                clientRequest.UserId = authRequest.UserProfileId;
                //Сотрудник и руководитель
                if (authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRUser)|| authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRHead))
                {
                    string auth = Request.Headers["Authorization"].ToString();
                    clientRequest.Role = Common.Models.Domain.Enum.Role.HRUser;
                    return Ok(await _service.GetAllAsync(clientRequest, auth));
                }
                ///кадровик
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
                _logger.LogError(ex, "[VacationOrder][GetAll]");
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Соглосование заявки изменение статуса
        /// </summary>
        /// <param name="requestId"></param>//id заявки 
        /// <param name="state"></param>//одобрено мли нет 
        /// <param name="requestState"></param>//статус заявки
        /// <param name="id"></param>//
        /// <returns></returns>
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public IActionResult ChangeState([FromQuery] VacationOrderClientRequest clientRequest)
        {
            try
            {
                var authRequest = GetAuthInfo();
                clientRequest.UserId = authRequest.UserProfileId;

                //Сотрудник и руководитель
                if (authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRUser)|| authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRHead))
                {
                    clientRequest.Role = Common.Models.Domain.Enum.Role.HRUser;
                    var obj = _service.ChangeState(clientRequest);
                    return Ok(obj);
                }
             
                //кадровик
                else if (authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRPersonal))
                {
                    clientRequest.Role = Common.Models.Domain.Enum.Role.HRPersonal;
                    var obj = _service.ChangeState(clientRequest);
                    return Ok(obj);
                }
                else
                {
                    StatusCode(StatusCodes.Status401Unauthorized);
                }
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Vacation][Put]");
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
                _logger.LogError(ex, "[VacationOrder][Delete]");
                return BadRequest(ex.ToString());
            }
        }
    }
}
