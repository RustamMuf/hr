using System.Collections.Generic;
using System.Threading.Tasks;
using Utg.HR.Common.Models;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Models.Domain;

namespace Utg.HR.Common.Services
{
    public interface IVacationService
    {
        Task<PagedResult<VacationViewModel>> GetAllAsync(VacationClientRequest clientRequest, string auth);
        Vacation Delete(int id);
        PagedResult<VacationViewModel> GetAllTest(VacationClientRequest clientRequest, string auth);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientRequest">В Search бахаем только фамилию</param>
        /// <param name="auth"></param>
        /// <returns></returns>
        Task<IEnumerable< VacationViewModel>> GetVacationsByUserSurname(string surname, string auth);    
    }
}
