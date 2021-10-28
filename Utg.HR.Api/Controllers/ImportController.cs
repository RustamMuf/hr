using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientModel.Import;
using Utg.HR.Common.Services;

namespace Utg.HR.API.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Route("hr/[controller]")]
    public class ImportController : ControllerBase
    {
		private readonly IImportService _importService;
		public ImportController(IImportService importService)
		{
			_importService = importService;
		}

		[HttpPost]
		[Route("vacation")]
		public async Task<IActionResult> Post([FromBody] IEnumerable<ExternalVacationImportModel> vacationItems)
		{
			if (vacationItems == null || !vacationItems.Any())
			{
				return BadRequest("Невозможно загрузить пустой список");
			}
			return await ImportInternal(vacationItems);
		}

		private async Task<IActionResult> ImportInternal(IEnumerable<ExternalVacationImportModel> vacationItems)
		{
			try
			{
				var result = await _importService.ImportVacation(vacationItems);

				var importResult = new
				{
					Total = vacationItems.Count(),
					Errors = result.Count(),
					ErrorMessages = string.Join(", ", result)
				};

				return Ok(importResult);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ToString());
			}
		}

		[HttpPost]
		[Route("balance")]
		public async Task<IActionResult> PostBalance([FromBody] IEnumerable<ExternalBalanceVacationImportModel> balanceItems)
		{
			if (balanceItems == null || !balanceItems.Any())
			{
				return BadRequest("Невозможно загрузить пустой список");
			}
			return await ImportInternalBalance(balanceItems);
		}

		private async Task<IActionResult> ImportInternalBalance(IEnumerable<ExternalBalanceVacationImportModel> balanceItems)
		{
			try
			{
				var result = await _importService.ImportBalance(balanceItems);

				var importResult = new
				{
					Total = balanceItems.Count(),
					Errors = result.Count(),
					ErrorMessages = string.Join(", ", result)
				};

				return Ok(importResult);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ToString());
			}
		}


		[HttpPost]
		[Route("FactVacation")]
		public async Task<IActionResult> ImportFactVacation([FromBody] IEnumerable<ExternalFactVacationImportModel> factVacation)
		{
			if (factVacation == null || !factVacation.Any())
			{
				return BadRequest("Невозможно загрузить пустой список");
			}
			return await ImportInternalFactVacation(factVacation);
		}


		private async Task<IActionResult> ImportInternalFactVacation(IEnumerable<ExternalFactVacationImportModel> factVacation)
		{
			try
			{
				var result = await _importService.ImportFactVacation(factVacation);

				var importResult = new
				{
					Total = factVacation.Count(),
					Errors = result.Count(),
					ErrorMessages = string.Join(", ", result)
				};

				return Ok(importResult);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ToString());
			}
		}
	}
}
