using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models.Domain;
using Utg.HR.Common.Repositories;
using Utg.HR.Common.Services;
using Utg.HR.Dal.SqlContext;

namespace Utg.HR.Dal.Repositories
{
    public class BalanceVacationRepository : IBalanceVacationRepository
    {
        private readonly UtgContext _dbContext;

        public BalanceVacationRepository(UtgContext dbContext)
        {
            _dbContext = dbContext;
        }
       

        public async Task<BalanceVacation> GetBalanceById(int id)
        {
            var entity = await _dbContext.BalanceVacations.FirstOrDefaultAsync(item => item.UserProfileId.Equals(id));

            return entity;
        }

        public int Import(BalanceVacation importedItem)
        {
            var existing = _dbContext.BalanceVacations
                .FirstOrDefault(vacation =>
                    vacation.UserProfileId == importedItem.UserProfileId);


            if (existing == null)
            {
                _dbContext.BalanceVacations.Add(importedItem);
                _dbContext.SaveChanges();
                return importedItem.BalanceId;
            }
            else
            {
                existing.BalanceOfVacation = importedItem.BalanceOfVacation;
                _dbContext.BalanceVacations.Update(existing);
                _dbContext.SaveChanges();
            }
            return existing.BalanceId;
        }
    }
}
