using System.Collections.Generic;
using System.Threading.Tasks;
using Utg.HR.Common.Models;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Models.ClientRequest;

namespace Utg.HR.Common.Services
{
    public interface INotificationService
    {
        Task<PagedResult<NotificationViewModel>> GetAllNotifications(NotificationClientRequest clientRequest, string auth);
        Task<IEnumerable<NotificationViewModel>> GetVacationsByUserSurname(string surname, string auth);
        void ChangeState(bool readed, int notificationId);
    }
}
