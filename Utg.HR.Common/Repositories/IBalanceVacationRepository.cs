using System.Threading.Tasks;
using Utg.HR.Common.Models.Domain;

namespace Utg.HR.Common.Repositories
{
    public interface IBalanceVacationRepository
    {
        Task<BalanceVacation> GetBalanceById(int id);
        int Import(BalanceVacation balance);
    }
}
