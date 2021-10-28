using Microsoft.Extensions.DependencyInjection;
using Utg.HR.BL.Services;
using Utg.HR.Common.Services;

namespace Utg.HR.BL.Configuration
{
	public static class ModuleInitializer
	{
		public static IServiceCollection ConfigureBL(this IServiceCollection services)
		{
			services.AddTransient<IHrRequestService, HrRequestService>();
			services.AddTransient<IImportService, ImportService>();
			services.AddTransient<IVacationRequestService, VacationRequestService>();
			services.AddTransient<IVacationService, VacationService>();
			services.AddTransient<IDataService, DataService>();
			services.AddTransient<INotificationService, NotificationService>();
			services.AddTransient<IVacationRequestHistoryChangeService, VacationRequestHistoryChangeService>();
			services.AddTransient<IVacationOrderService, VacationOrderService>();
			services.AddTransient<IBalanceVacationService, BalanceVacationService>();

			return services;
		}
	}
}
