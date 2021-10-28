using System.Collections.Generic;
using Utg.HR.Common.Models.Domain;
using Utg.HR.Common.Repositories;
using Utg.HR.Dal.SqlContext;

namespace Utg.HR.Dal.Repositories
{
	public class HrRequestRepository : IHrRequestRepository
	{
		private readonly UtgContext dbContext;

		public HrRequestRepository(UtgContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public IEnumerable<HrRequest> Get()
		{
			return this.dbContext.HrRequests;
		}
	}
}
