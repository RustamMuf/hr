using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Models.Domain;

namespace Utg.HR.Common.Repositories
{
    public interface INotificationRepository
    {
        public IQueryable<Notification> GetAll();
        Task<ICollection<Notification>> GetAllNotifications(NotificationClientRequest clientRequest);
        void ChangeState(bool readed, int notificationId);
    }
}
