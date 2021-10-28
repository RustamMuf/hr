using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.API.Controllers
{
    public class HrControllerBase : ControllerBase
    {
		protected ILogger _logger;
		public HrControllerBase(ILogger logger)
		{
			_logger = logger;
		}

		protected bool CanGo(params Role[] allowedRoles)
		{
			var parsedRoles = allowedRoles.Select(role => (int)role);
			var currentUserRoles = GetCurrentUserRoles();
			return currentUserRoles.Any(currentUserRole => parsedRoles.Contains(currentUserRole));
		}

		protected string GetErrors()
		{
			StringBuilder modelErrors = new StringBuilder();
			foreach (var modelState in ModelState.Values)
			{
				foreach (var modelError in modelState.Errors)
				{
					modelErrors.Append(modelError.ErrorMessage);
				}
			}
			return modelErrors.ToString();
		}

		protected IActionResult BadRequestWithError(string error)
		{
			var details = new ProblemDetails { Detail = error, Status = 400 };
			return BadRequest(details);
		}

		protected int? GetCurrentUserId()
		{
			var user = HttpContext.User;
			_logger.LogWarning($"[ControllerBase][GetCurrentUserId] User {user} ");

            try
            {
				if (user == null)
					return null;
				var raw = user.FindFirstValue("userId");
				if (int.TryParse(raw, out var userId))
				{
					return userId;
				}
			}
            catch (Exception ex)
            {
				_logger.LogError(ex, "[ControllerBase][GetCurrentUserId] {userId}");
			}
			return null;
		}

		protected int? GetCurrentUserProfileId()
		{
			
			var user = HttpContext.User;
			_logger.LogWarning($"[ControllerBase][GetCurrentUserProfileId] User {user} ");
            try
            {
				if (user == null)
					return null;
				var raw = user.FindFirstValue("userProfileId");
				if (int.TryParse(raw, out var userId))
				{
					_logger.LogWarning($"[ControllerBase][GetCurrentUserProfileId] UserProfileId {userId} ");
					return userId;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "[ControllerBase][GetCurrentUserProfileId] ");
			}
			return null;
		}

		protected string GetClaimValue(string name)
		{
			var user = HttpContext.User;
			if (user == null)
				return null;
			var raw = user.FindFirstValue(name);
			return raw;
		}

		protected bool HasRole(Role role)
		{
			var user = HttpContext.User;
			if (user == null)
				return false;
			var roles = GetCurrentUserRoles();

			return roles.Contains((int)role);
		}

		protected IEnumerable<int> GetCurrentUserRoles()
		{
			var user = HttpContext.User;
			if (user == null)
				return Enumerable.Empty<int>();
			var roles = user.FindAll(ClaimTypes.Role).Select(claim => int.TryParse(claim.Value, out var value) ? value : 0).Distinct().ToList();
			return roles;
		}

		protected AuthInfo GetAuthInfo()
		{
			var userId = GetCurrentUserId().Value;
			var userProfileId = GetCurrentUserProfileId().Value;
			var auth = Request.Headers["Authorization"].ToString();
			var roles = GetCurrentUserRoles();
			return new AuthInfo
			{
				UserId = userId,
				UserProfileId = userProfileId,
				AuthToken = auth,
				Roles = roles
			};
		}
	}
}
