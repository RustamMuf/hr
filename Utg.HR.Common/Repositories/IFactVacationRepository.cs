using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models.Domain;

namespace Utg.HR.Common.Repositories
{
   public interface IFactVacationRepository
    {
       public void Import(FactVacation vacation);

        public IQueryable<FactVacation> GetAll();

    }
}
