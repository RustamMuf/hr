using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models.Domain;

namespace Utg.HR.Common.Repositories
{
    public interface IVacationRequestHistoryChangeRepository
    {
        Task<ICollection<VacationRequestHistoryChange>> GetAllStatuses(int requestId);
    }
}
