using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Models.Domain;

namespace Utg.HR.Common.Repositories
{
    public interface IVacationRequestRepository
    {
        ICollection<VacationRequest> GetAllRequests(VacationRequestClientRequest clientRequest); 
        void Add(VacationRequest model);
        VacationRequest ChangeRequestState(VacationRequestClientRequest clientRequest);
        VacationRequest Delete(int id);
        void ChangeRequest(VacationRequestViewModel model);
        IQueryable<Vacation> ValidateRequest(DateTime thisYear, int userId);
        VacationRequest GetById(int id);
    }
}