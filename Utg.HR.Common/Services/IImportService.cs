using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientModel.Import;

namespace Utg.HR.Common.Services
{
    public interface IImportService
    {
        Task<IEnumerable<string>> ImportVacation(IEnumerable<ExternalVacationImportModel> vacationItems);
        Task<IEnumerable<string>> ImportBalance(IEnumerable<ExternalBalanceVacationImportModel> balanceItems);

        Task<IEnumerable<string>> ImportFactVacation(IEnumerable<ExternalFactVacationImportModel> vacationItems);
        
    }
}
