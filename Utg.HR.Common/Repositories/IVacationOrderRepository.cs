using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Models.Domain;

namespace Utg.HR.Common.Repositories
{
    public interface IVacationOrderRepository
    {
         Task<ICollection<VacationOrder>> GetAllAsync(VacationOrderClientRequest clientRequest);
        VacationOrder Delete(int id);
        VacationOrder ChangeState(VacationOrderClientRequest clientRequest);
    }
}
