using System.Collections.Generic;
using Utg.HR.Common.Models.Domain;

namespace Utg.HR.Common.Repositories
{
	public interface IHrRequestRepository
	{
		IEnumerable<HrRequest> Get();
	}
}
