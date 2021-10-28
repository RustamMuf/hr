using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Models.Domain;
using Utg.HR.Common.Repositories;
using Utg.HR.Dal.SqlContext;

namespace Utg.HR.Dal.Repositories
{
    public class VacationOrderRepository : IVacationOrderRepository
    {
        private readonly UtgContext _dbContext;

        public VacationOrderRepository(UtgContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<VacationOrder>> GetAllAsync(VacationOrderClientRequest clientRequest)
        {
            var raw = await _dbContext.VacationOrders.Include(item=>item.Vacation).OrderBy(item => item.Id).ToListAsync();

            if (clientRequest.FirstDate != null && clientRequest.SecondDate != null)
            {
                raw = raw.Where(i => i.Vacation.StartDate >= clientRequest.FirstDate && i.Vacation.EndDate <= clientRequest.SecondDate).ToList();
            }

            if (clientRequest.OrderPrintedFilter != null)
            {
                raw = raw.AsEnumerable().Where(item => item.OrderPrinted.ToString().Equals(clientRequest.OrderPrintedFilter.ToString())).ToList();
            }

            if (clientRequest.OrderRecivedFilter != null)
            {
                raw = raw.AsEnumerable().Where(item => item.OrderRecived.ToString().Equals(clientRequest.OrderRecivedFilter.ToString())).ToList();
            }


            return raw;
        }

        public VacationOrder Delete(int id)
        {

            using var context = _dbContext;

            var entity = context.VacationOrders.FirstOrDefault(item => item.Id.Equals(id));

            //if (entity == null) return;

            context.VacationOrders.Remove(entity);

            context.SaveChanges();

            return entity;
        }

        public VacationOrder ChangeState(VacationOrderClientRequest clientRequest)
        {


            var context = _dbContext;

            var entity = context.VacationOrders.ToList();
            var order = entity.FirstOrDefault(item => item.Id.Equals(clientRequest.OrderId));

            if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRUser)
            {
                if (clientRequest.OrderPrinted==2)
                {
                   order.OrderPrinted = Common.Models.Domain.Enum.OrderPrinted.Printed;
                }
                if (clientRequest.OrderPrinted == 1)
                {
                    order.OrderPrinted = Common.Models.Domain.Enum.OrderPrinted.NoPrinted;
                }
            }

            if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRPersonal)
            {
                if (clientRequest.OrderRecived == 2)
                {
                    order.OrderRecived = Common.Models.Domain.Enum.OrderRecived.Recived;
                }
                if (clientRequest.OrderRecived == 1)
                {
                    order.OrderRecived = Common.Models.Domain.Enum.OrderRecived.NoRecived;
                }
            }


            if (order == null) return null;


            context.VacationOrders.Update(order);

            context.SaveChanges();

            return order;
        }
    }
}
