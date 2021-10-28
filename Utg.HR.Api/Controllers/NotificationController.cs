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
    public class NotificationController : HrControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service,
            ILogger<HrControllerBase> logger) : base(logger)
        {
            _service = service;
        }

        /// <summary>
        /// Получаем все уведомления 
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NotificationViewModel>))]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] NotificationClientRequest clientRequest)
        {
            try
            {
                var authRequest = GetAuthInfo();
                clientRequest.UserId = authRequest.UserProfileId;
                string auth = Request.Headers["Authorization"].ToString();
                //return Ok(await _service.GetAllNotifications(clientRequest));
                //Сотрудник
                if (authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRUser) || authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRHead))
                {
                    clientRequest.Role = Common.Models.Domain.Enum.Role.HRUser;
                    return Ok(await _service.GetAllNotifications(clientRequest, auth));
                }
                else if (authRequest.Roles.Contains((int)Common.Models.Domain.Enum.Role.HRPersonal) )
                {
                    clientRequest.Role = Common.Models.Domain.Enum.Role.HRPersonal;
                    return Ok(await _service.GetAllNotifications(clientRequest, auth));
                }
                else
                {
                    StatusCode(StatusCodes.Status401Unauthorized);
                }
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Notification][GetAll]");
                return BadRequest(ex.ToString());
            }
        }



        /// <summary>
        /// прочитано сообщение 
        /// </summary>
        /// <param name="readed"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ChangeState([FromQuery] bool readed, int notificationId)
        {
            try
            {               
                _service.ChangeState(readed, notificationId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Notification][ChangeState]");
                return BadRequest(ex.ToString());
            }
        }
    }
}
