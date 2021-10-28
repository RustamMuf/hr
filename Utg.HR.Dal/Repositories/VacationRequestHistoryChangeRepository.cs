using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models.Domain;
using Utg.HR.Common.Repositories;
using Utg.HR.Dal.SqlContext;

namespace Utg.HR.Dal.Repositories
{
    public class VacationRequestHistoryChangeRepository : IVacationRequestHistoryChangeRepository
    {
        private readonly UtgContext _dbContext;

        public VacationRequestHistoryChangeRepository(UtgContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ICollection<VacationRequestHistoryChange>> GetAllStatuses(int requestId)
        {
            var raw = await _dbContext.VacationRequestHistoryChanges.Where(item=>item.RequestId.Equals(requestId)).ToListAsync();

            return raw;
        }
    }
}
