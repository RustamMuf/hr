using System.Collections.Generic;
using Utg.HR.Common.Models.ClientModel;

namespace Utg.HR.Common.Services
{
	public interface IHrRequestService
	{
		IEnumerable<HrRequestModel> Get();
	}
}
