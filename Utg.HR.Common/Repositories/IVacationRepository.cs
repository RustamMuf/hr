using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientModel.Import;
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Models.Domain;

namespace Utg.HR.Common.Repositories
{
    public interface IVacationRepository
    {
        IQueryable<Vacation> GetAllVacations(VacationClientRequest clientRequest);
        Vacation Delete(int id);
        void AddNotification(Notification notification);
        int Import(Vacation vacation);
        void AddOrder(VacationOrder vacationOrder);

        int UpdateWithShiftedDays(Vacation vacation, ExternalVacationImportModel vacationImportModel);
    }
}
