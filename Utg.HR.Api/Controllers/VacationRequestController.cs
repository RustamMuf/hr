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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("hr/[controller]")]
    public class VacationRequestController : HrControllerBase
    {
        private readonly IVacationRequestService _service;

        public VacationRequestController(IVacationRequestService service,
             ILogger<HrControllerBase> logger) : base(logger)
        {
            _service = service;
        }

        /// <summary>
        /// Получаем все заявки по отпускам 
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VacationRequestViewModel>))]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] VacationRequestClientRequest clientRequest)
        {
            try
            {
                var authRequest = GetAuthInfo();
                clientRequest.UserId = authRequest.UserProfileId;

                //Сотрудник
                if (authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRUser))
                {
                    string auth = Request.Headers["Authorization"].ToString();
                    clientRequest.Role = Common.Models.Domain.Enum.Role.HRUser;
                    return Ok(await _service.GetAllAsync(clientRequest, auth));
                }
                //руководитель
                else if (authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRHead))
                {
                    string auth = Request.Headers["Authorization"].ToString();
                    clientRequest.Role = Common.Models.Domain.Enum.Role.HRHead;
                    return Ok(await _service.GetAllAsync(clientRequest, auth));
                }
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
        /// <summary>
        /// Получаем все заявки по отпускам 
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VacationRequestViewModel>))]
        [Route("test")]
        [HttpGet]
        public  IActionResult GetAllTest([FromQuery] VacationRequestClientRequest clientRequest)
        {
            try
            {
                string auth = Request.Headers["Authorization"].ToString();
                return Ok( _service.GetAllAsync(clientRequest, auth));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Vacation][GetAllTest]");
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Добавление заявки на отпуск
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] VacationRequestViewModel model)
        {
            try
            {
                var authRequest = GetAuthInfo();
                var id = GetCurrentUserProfileId();
                model.UserProfileId = id.Value;

                if (model == null) return BadRequest("Setting is required");

                _service.Add(model);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Vacation][Add]");
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
        public IActionResult Put([FromQuery] VacationRequestClientRequest clientRequest)
        {
            try
            {   
                var authRequest = GetAuthInfo();
                clientRequest.UserId = authRequest.UserProfileId;
                string auth = Request.Headers["Authorization"].ToString();
                //Сотрудник
                if (authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRUser))
                {
                    clientRequest.Role = Common.Models.Domain.Enum.Role.HRUser;
                    var obj = _service.ChangeRequestState(clientRequest, auth);
                    return Ok(obj);
                }
                //руководитель
                else if (authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRHead))
                {
                    clientRequest.Role = Common.Models.Domain.Enum.Role.HRHead;
                    var obj = _service.ChangeRequestState(clientRequest, auth);
                    return Ok(obj);
                }
                //кадровик
                else if (authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRPersonal))
                {
                    clientRequest.Role = Common.Models.Domain.Enum.Role.HRPersonal;
                    var obj = _service.ChangeRequestState(clientRequest, auth);
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

        [HttpPut]
        [Route("changeRequest")]
        public IActionResult ChangeRequest(VacationRequestViewModel model)
        {
            try
            {
                if (model == null) return BadRequest("model is required");

                _service.ChangeRequest(model);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Vacation][ChangeRequest]");
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
                _logger.LogError(ex, "[VacationRequest][Delete]");
                return BadRequest(ex.ToString());
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VacationRequestViewModel))]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[VacationRequest][GetById]");
                return BadRequest(ex.ToString());
            }

        }


    }
}
