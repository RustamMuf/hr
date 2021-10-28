using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Utg.HR.API.Validation
{
	public static class FluentValidationInitializer
	{
		public static IMvcBuilder RegisterFluentValidation(this IMvcBuilder service)
		{
			return service.AddFluentValidation();
		}
	}
}
