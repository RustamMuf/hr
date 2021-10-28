using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Models.Domain;
using Utg.HR.Common.Repositories;
using Utg.HR.Dal.SqlContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Models.ClientModel.Import;

namespace Utg.HR.Dal.Repositories
{
    public class VacationRepository : IVacationRepository
    {
        private readonly UtgContext _dbContext;

        public VacationRepository(UtgContext dbContext)
        {
            _dbContext = dbContext;
        }
     
        public IQueryable<Vacation> GetAllVacations(VacationClientRequest clientRequest)
        {
            var raw =  _dbContext.Vacations.OrderBy(item => item.Id);


            if (clientRequest.UserIdSearch != null)
            {
                return raw.Where(item => item.UserProfileId.Equals(clientRequest.UserIdSearch));
            }

            if (clientRequest.VacationType == 2)
            {
                return raw.Where(item => item.VacationType.Equals(Common.Models.Domain.Enum.VacationType.Planed));
            }
            if (clientRequest.VacationType == 1)
            {
                return raw.Where(item => item.VacationType.Equals(Common.Models.Domain.Enum.VacationType.UnPlaned));
            }

            if (clientRequest.FirstDate != null && clientRequest.SecondDate != null)
            {
                return raw.Where(i => i.StartDate >= clientRequest.FirstDate && i.StartDate <= clientRequest.SecondDate);
            }

            return raw;
        }

        public Vacation Delete(int id)
        {

            using var context = _dbContext;

            var entity = context.Vacations.FirstOrDefault(item => item.Id.Equals(id));

            //if (entity == null) return;

            context.Vacations.Remove(entity);

            context.SaveChanges();

            return entity;
        }

        public void AddNotification(Notification notification)
        {
            notification.CreatedDate = DateTime.Now;
            var context = _dbContext;

            var query = _dbContext.Notifications.Include(item=>item.Vacation).Where(item => item.VacationId.Equals(notification.VacationId));
            bool not = false;

            foreach (var c in query)
            {
                if (c.NotificationType.Equals(notification.NotificationType))
                {
                    not = true;
                }
            }

            if(not!=true)
            {
                context.Add(notification);
                context.SaveChanges();
            }
        }

        public void AddOrder(VacationOrder vacationOrder)
        {
            var existing = _dbContext.VacationOrders.Include(item=>item.Vacation)
               .FirstOrDefault(order =>
                   order.Vacation.Id == vacationOrder.VacationId);

            if (existing == null)
            {
                vacationOrder.OrderPrinted = Common.Models.Domain.Enum.OrderPrinted.WithoutAction;
                vacationOrder.OrderRecived = Common.Models.Domain.Enum.OrderRecived.WithoutAction;
                vacationOrder.CreatedDate = DateTime.Now;
                _dbContext.VacationOrders.Add(vacationOrder);
                _dbContext.SaveChanges();
            }
        }

        public int Import(Vacation importedItem)
        {
            var existing = _dbContext.Vacations
                .FirstOrDefault(vacation =>
                    vacation.UserProfileId == importedItem.UserProfileId &&
                    vacation.StartDate.Date == importedItem.StartDate.Date&& vacation.EndDate.Date == importedItem.EndDate.Date);
 

            if (existing == null)
            {
                _dbContext.Vacations.Add(importedItem);
                _dbContext.SaveChanges();
                return importedItem.Id;
            }
            return existing.Id;
        }

        public int UpdateWithShiftedDays(Vacation importedItem, ExternalVacationImportModel vacationImportModel)
        {
            var existing = _dbContext.Vacations
               .FirstOrDefault(vacation =>
                   vacation.UserProfileId == importedItem.UserProfileId &&
                   (vacation.StartDate.Date == importedItem.StartDate.Date && vacation.EndDate.Date == importedItem.EndDate.Date
                   || vacation.StartDate.Date == vacationImportModel.ShiftedStartDate.Date && vacation.EndDate.Date == vacationImportModel.ShiftedEndDate));

            if (existing == null)
            {
                importedItem.StartDate = vacationImportModel.ShiftedStartDate;
                importedItem.EndDate = vacationImportModel.ShiftedEndDate;
                _dbContext.Vacations.Add(importedItem);
                _dbContext.SaveChanges();
                return importedItem.Id;
            }
            else
            {

                existing.StartDate = vacationImportModel.ShiftedStartDate;
                existing.EndDate = vacationImportModel.ShiftedEndDate;

                _dbContext.Vacations.Update(existing);

                _dbContext.SaveChanges();
                return existing.Id;
            }
        }
    }
}
