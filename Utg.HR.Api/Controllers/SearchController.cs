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
    [Route("hr/[controller]")]
    [ApiController]
    public class SearchController:HrControllerBase
    {
        private readonly IVacationService _vacationService;
        private readonly IVacationRequestService _vacationRequestService;
        private readonly INotificationService _notificationService;
        private readonly ILogger _logger;
        public SearchController(IVacationRequestService vacationRequestService, IVacationService vacationService,ILogger<SearchController> logger,INotificationService notificationService):base(logger)
        {
            _vacationService = vacationService;
            _vacationRequestService = vacationRequestService;
            _notificationService = notificationService;
        }
        [HttpGet("GetNotifications")]
        public IActionResult GetNotification([FromQuery] string surname)
        {
            string auth = Request.Headers["Authorization"].ToString();
            var vacations = _notificationService.GetVacationsByUserSurname(surname, auth);
            return Ok(vacations);
        }
        [HttpGet("GetVacation")]
        public IActionResult GetVacation([FromQuery]string surname)
        {
            string auth = Request.Headers["Authorization"].ToString();
            var vacations = _vacationService.GetVacationsByUserSurname(surname,auth);
            return Ok(vacations);
        }
        [HttpGet("GetVacationRequest")]
        public IActionResult GetVacationRequest([FromQuery]string surname)

        {
            string auth = Request.Headers["Authorization"].ToString();
            var vacationRequests = _vacationRequestService.GetVacationsByUserSurname(surname,auth);
            return Ok(vacationRequests);
        }
    }
}
