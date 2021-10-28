using Microsoft.EntityFrameworkCore;
using Utg.HR.Common.Models.Domain;

namespace Utg.HR.Dal.SqlContext
{
	public class UtgContext : DbContext
	{
		public UtgContext(DbContextOptions<UtgContext> options)
			: base(options)
		{
		}

		public DbSet<HrRequest> HrRequests { get; set; }
		public DbSet<VacationRequest> VacationRequests { get; set; }
		public DbSet<Vacation> Vacations { get; set; }
		
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<VacationRequestHistoryChange> VacationRequestHistoryChanges { get; set; }
		public DbSet<VacationOrder> VacationOrders { get; set; }
		public DbSet<BalanceVacation> BalanceVacations { get; set; }

		public DbSet<FactVacation> FactVacations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("public");
			modelBuilder.AddEnumConverters();
		}
	}
}
