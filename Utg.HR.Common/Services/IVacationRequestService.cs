using System.Collections.Generic;
using System.Threading.Tasks;
using Utg.HR.Common.Models;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Models.Domain;

namespace Utg.HR.Common.Services
{
    public interface IVacationRequestService
    {
        Task<PagedResult<VacationRequestViewModel>> GetAllAsync(VacationRequestClientRequest clientRequest, string auth);
        PagedResult<VacationRequestViewModel> GetAllTest(VacationRequestClientRequest clientRequest, string auth);
        void Add(VacationRequestViewModel model);
        Task<IEnumerable<VacationRequestViewModel>> GetVacationsByUserSurname(string surname, string auth);
        Task<VacationRequest> ChangeRequestState(VacationRequestClientRequest clientRequest, string auth);
        VacationRequest Delete(int id);
        void ChangeRequest(VacationRequestViewModel model);
        VacationRequestViewModel GetById(int id);
    }
}

