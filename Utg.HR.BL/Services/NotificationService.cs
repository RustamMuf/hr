using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Models.Domain;
using Utg.HR.Common.Repositories;
using Utg.HR.Common.Services;

namespace Utg.HR.BL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IDataService _dataService;

        public NotificationService(INotificationRepository notificationRepository,
        IDataService dataService,
        IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _dataService = dataService;
        }

        public void ChangeState(bool readed, int notificationId)
        {
            _notificationRepository.ChangeState(readed, notificationId);
        }

        public async Task<IEnumerable<NotificationViewModel>> GetVacationsByUserSurname(string surname, string auth)
        {
            var not = new NotificationClientRequest { };
            var vacations = _notificationRepository.GetAll();


            var userIds = vacations.Select(vacationReq => vacationReq.Vacation.UserProfileId).Distinct();


            var userProfiles = await _dataService.GetUserProfiles(userIds, auth);

            var vacationWithUsers = vacations.Join(
                userProfiles,
                vacation => vacation.Vacation.UserProfileId,
                user => user.Id,
                (vacation, user) => new NotificationViewModel
                {
                    Id = vacation.Id,
                    UserSurname = user.Surname,
                    UserName = user.Name,
                    CreatedDate = vacation.CreatedDate,
                    EndDate = vacation.Vacation.EndDate,
                    StartDate = vacation.Vacation.StartDate
                }
                );
            var result = vacationWithUsers.Where(x => x.UserSurname.Contains(surname));
            return result;

        }
        public async Task<PagedResult<NotificationViewModel>> GetAllNotifications(NotificationClientRequest clientRequest, string auth)
        {
            var query = await _notificationRepository.GetAllNotifications(clientRequest);

            if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRUser)
            {
                query = query.Where(item => item.Vacation.UserProfileId.Equals(clientRequest.UserId)).ToList();
            }

            var userIds = query.Select(vacationReq => vacationReq.Vacation.UserProfileId).Distinct();

            var userProfiles = await _dataService.GetUserProfiles(userIds, auth);

            var total = query.Count();
            var model = _mapper.Map<List<NotificationViewModel>>(query);

            model.ForEach(item =>
            {
                var userProfile = userProfiles.FirstOrDefault(userP => userP.Id == item.UserId);
                item.UserName = userProfile?.Name;
                item.UserPatronymic = userProfile?.Patronymic;
                item.UserSurname = userProfile?.Surname;
                item.DepartmentName = userProfile?.DepartmentName;
                item.DepartmentId = userProfile?.DepartmentId;
                item.CompanyId = userProfile?.CompanyId;
                item.CompanyName = userProfile?.CompanyName;
                item.TabN = userProfile?.TabN;
            });


            var result = model.Skip(clientRequest.Skip).Take(clientRequest.Take);

            return new PagedResult<NotificationViewModel>
            {
                Result = result,
                Total = total
            };
        }


    }
}
