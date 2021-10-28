using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utg.HR.Common.Models.Domain;
using Utg.HR.Common.Repositories;
using Utg.HR.Dal.SqlContext;

namespace Utg.HR.Dal.Repositories
{
    class FactVacationRepository : IFactVacationRepository
    {
        private readonly UtgContext _dbContext;

        public FactVacationRepository(UtgContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<FactVacation> GetAll()
        {
            return _dbContext.FactVacations;
        }

        public  void  Import(FactVacation vacation)
        {
            //var existing = _dbContext.FactVacations
            //   .FirstOrDefault(vacation =>
            //       vacation.UserProfileId == vacation.UserProfileId &&
            //       vacation.StartDate.Date == vacation.StartDate.Date && vacation.EndDate.Date == vacation.EndDate.Date);

            //if (existing == null)
            //{
                 _dbContext.FactVacations.Add(vacation);
                 _dbContext. SaveChanges();
            //}
        }



    }
}
