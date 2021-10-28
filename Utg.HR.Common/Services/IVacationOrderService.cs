using System.Threading.Tasks;
using Utg.HR.Common.Models;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Models.Domain;

namespace Utg.HR.Common.Services
{
    public interface IVacationOrderService
    {
        Task<PagedResult<VacationOrderViewModel>> GetAllAsync(VacationOrderClientRequest clientRequest, string auth);
        VacationOrder Delete(int id);
        VacationOrder ChangeState(VacationOrderClientRequest clientRequest);
    }
}
