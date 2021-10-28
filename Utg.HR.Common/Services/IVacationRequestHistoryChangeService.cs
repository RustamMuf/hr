using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Models.ClientRequest;

namespace Utg.HR.Common.Services
{
    public interface IVacationRequestHistoryChangeService
    {
        Task<PagedResult<VacationRequestHistoryChangeViewModel>> GetAllStatuses(VacationRequestHistoryChangeClientRequest clientRequest);
    }
}
