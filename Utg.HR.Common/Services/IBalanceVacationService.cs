using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientModel;

namespace Utg.HR.Common.Services
{
    public interface IBalanceVacationService
    {
        Task<BalanceVacationViewModel> GetBalanceById(int id);
    }
}
