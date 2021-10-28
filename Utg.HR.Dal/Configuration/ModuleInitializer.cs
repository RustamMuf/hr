using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utg.HR.Common.Repositories;
using Utg.HR.Dal.Repositories;
using Utg.HR.Dal.SqlContext;

namespace Utg.HR.Dal.Configuration
{
	public static class ModuleInitializer
	{
		public static IServiceCollection ConfigureDal(this IServiceCollection services, IConfiguration configuration)
		{
			SetSettings(services, configuration);
			AddDependenciesToContainer(services);

			return services;
		}

		private static void SetSettings(IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<UtgContext>(options =>
			{
				options.UseNpgsql(configuration.GetConnectionString("UTGDatabase"));
			});
		}

		private static void AddDependenciesToContainer(IServiceCollection services)
		{
			services.AddTransient<IHrRequestRepository, HrRequestRepository>();
			services.AddTransient<IVacationRequestRepository, VacationRequestRepository>();
			services.AddTransient<IVacationRepository, VacationRepository>();
			services.AddTransient<IFactVacationRepository, FactVacationRepository>();
			
			services.AddTransient<INotificationRepository, NotificationRepository>();
			services.AddTransient<IVacationRequestHistoryChangeRepository, VacationRequestHistoryChangeRepository>();
			services.AddTransient<IVacationOrderRepository, VacationOrderRepository>();
			services.AddTransient<IBalanceVacationRepository, BalanceVacationRepository>();
		}
	}
}
