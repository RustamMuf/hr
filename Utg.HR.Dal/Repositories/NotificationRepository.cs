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
    public class NotificationRepository :INotificationRepository
    {
        private readonly UtgContext _dbContext;

        public NotificationRepository(UtgContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ChangeState(bool readed, int notificationId)
        {
            var entity = _dbContext.Notifications.FirstOrDefault(item => item.Id.Equals(notificationId));

            if(readed == true)
            {
                entity.ReadedDate = DateTime.Now;
                entity.Readed = true;
                _dbContext.Notifications.Update(entity);
                _dbContext.SaveChanges();
            }
            else
            {
                return;
            }
        }
        public IQueryable<Notification> GetAll()
        {
            var result = _dbContext.Notifications.Include(x => x.Vacation);
            return result;
        }
        public async Task<ICollection<Notification>> GetAllNotifications(NotificationClientRequest clientRequest)
        {
            var raw = await _dbContext.Notifications.Include(item=>item.Vacation).OrderBy(item => item.Id).ToListAsync();

            if (clientRequest.NotificationTypeFilter != null)
            {
                raw = raw.Where(item => item.NotificationType.ToString().ToString().Equals(clientRequest.NotificationTypeFilter)).ToList();
            }

            if (clientRequest.UserIdSearch != null)
            {
                raw = raw.Where(item => item.Vacation.UserProfileId.Equals(clientRequest.UserIdSearch)).ToList();
            }

            if (clientRequest.Readed =="Readed")
            {
                raw = raw.AsEnumerable().Where(item => item.Readed==true).ToList();
            }

            if (clientRequest.Readed == "NoReaded")
            {
                raw = raw.AsEnumerable().Where(item => item.Readed == false).ToList();
            }

            if (clientRequest.FirstDate != null && clientRequest.SecondDate != null)
            {
                raw = raw.Where(i => i.Vacation.StartDate >= clientRequest.FirstDate && i.Vacation.EndDate <= clientRequest.SecondDate).ToList();
            }

            return raw;
        }
    }
}
