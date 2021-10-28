using Microsoft.AspNetCore.Mvc;
using System;
using Utg.HR.Common.Services;

namespace Utg.HR.Api.Controllers
{
	[Route("hr/[controller]")]
	public class HealthController : ControllerBase
	{
		private readonly IHrRequestService _service;

		public HealthController(IHrRequestService  service)
		{
			_service =  service;
		}

		[HttpGet("ping")]
		public IActionResult Ping()
		{
			return Ok("hr");
		}

		[HttpGet("pingdb")]
		public IActionResult PingDB()
		{
			try
			{
				var list = _service.Get();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ToString());
			}
		}
	}
}
