using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Repositories;
using System.Linq;
using Utg.HR.Common.Services;
using Utg.HR.Common.Models.Domain;
using Utg.HR.Common.Models.ClientRequest;

namespace Utg.HR.BL.Services
{
    public class VacationService : IVacationService
    {
        private readonly IVacationRepository _vacationRepository;
        private readonly IMapper _mapper;
        private readonly IDataService _dataService;
        IFactVacationRepository _factVacationRepository;
        public VacationService(IVacationRepository vacationRepository, IDataService dataService,
        IMapper mapper,
        IFactVacationRepository factVacationRepository)
        {
            _vacationRepository = vacationRepository;
            _dataService = dataService;
            _mapper = mapper;
            _factVacationRepository = factVacationRepository;
        }
        public async Task<IEnumerable<VacationViewModel>> GetVacationsByUserSurname(string surname, string auth)
        {
            var vacations = _vacationRepository.GetAllVacations(new VacationClientRequest() { });

            var userIds = vacations.Select(x => x.UserProfileId).Distinct();

            var userProfiles = await _dataService.GetUserProfiles(userIds, auth);

            var vacationWithUsers = vacations.Join(
                userProfiles,
                vacation => vacation.UserProfileId,
                user => user.Id,
                (vacation, user) => new VacationViewModel
                {
                    Id = vacation.Id,
                    UserSurname = user.Surname,
                    Days = vacation.Days,
                    UserName = user.Name,
                    CreatedDate = vacation.CreatedDate,
                    EndDate = vacation.EndDate,
                    StartDate = vacation.StartDate,
                    CompanyId = vacation.CompanyId,
                    IsPayment = vacation.IsPayment,
                    TabN = user.TabN,
                    UserProfileId = user.UserId,
                    VacationType = vacation.VacationType
                }
                );
            var result = vacationWithUsers.Where(x => x.UserSurname.Contains(surname));
            return result;
        }
        public async Task<PagedResult<VacationViewModel>> GetAllAsync(VacationClientRequest clientRequest, string auth)
        {
            var query = _vacationRepository.GetAllVacations(clientRequest);

            AddNotification(query.ToList(), clientRequest.UserId);


            if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRUser)
            {
                query = query.Where(item => item.UserProfileId.Equals(clientRequest.UserId));
            }

            if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRHead)
            {
                query = query.Where(item => item.CompanyId.Equals(clientRequest.CompanyId) || item.UserProfileId.Equals(clientRequest.UserId));
            }

            if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRPersonal)
            {
                query = query.Where(item => item.UserProfileId.Equals(clientRequest.UserId));
            }

            var userIds = query.Select(vacationReq => vacationReq.UserProfileId).Distinct();

            var userProfiles = await _dataService.GetUserProfiles(userIds, auth);

            var total = query.Count();
            var model = _mapper.Map<List<VacationViewModel>>(query);

            model.ForEach(item =>
            {

                var userProfile = userProfiles.FirstOrDefault(userP => userP.Id == item.UserProfileId);

                item.ChiefFullName = userProfile?.ChiefFullName;
                item.UserName = userProfile?.Name;
                item.UserPatronymic = userProfile?.Patronymic;
                item.UserSurname = userProfile?.Surname;
                item.TabN = userProfile?.TabN;
            });

            if (!string.IsNullOrWhiteSpace(clientRequest.Search))
            {
                var clon = model.Where(item => item.UserSurname.Contains(clientRequest.Search, StringComparison.OrdinalIgnoreCase));
                total = clon.Count();
                model = clon.ToList();
            }
            List<VacationViewModel> result = null;
            if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRHead)
                result = model.Where(x => x.ChiefFullName == clientRequest.ChiefFullName || x.UserProfileId == clientRequest.UserId).OrderBy(item => item.UserSurname).Skip(clientRequest.Skip).Take(clientRequest.Take).ToList();
            else
                result = model.OrderBy(item => item.UserSurname).Skip(clientRequest.Skip).Take(clientRequest.Take).ToList();


            var factVacations = _factVacationRepository.GetAll().ToList();
            result.ForEach(vacation =>
            {
                vacation.IsFactVaction = factVacations.Any(factVacation => factVacation.UserProfileId == vacation.UserProfileId
                                          && (IsBetween<DateTime>(factVacation.StartDate, vacation.StartDate, vacation.EndDate)
                                           || IsBetween<DateTime>(factVacation.EndDate, vacation.StartDate, vacation.EndDate)));
            });


            return new PagedResult<VacationViewModel>
            {

                Total = total,
                Result = result
            };
        }

        private bool IsBetween<T>(T item, T start, T end)
        {
            return Comparer<T>.Default.Compare(item, start) >= 0
                && Comparer<T>.Default.Compare(item, end) <= 0;
        }

        public Vacation Delete(int id)
        {
            var model = _vacationRepository.Delete(id);

            return model;
        }


        public PagedResult<VacationViewModel> GetAllTest(VacationClientRequest clientRequest, string auth)
        {
            var query = _vacationRepository.GetAllVacations(clientRequest);

            var total = query.Count();
            var model = _mapper.Map<List<VacationViewModel>>(query);

            var result = model.Skip(clientRequest.Skip).Take(clientRequest.Take);

            return new PagedResult<VacationViewModel>
            {

                Total = total,
                Result = result
            };
        }

        private void AddNotification(List<Vacation> vacations, int? userId)
        {
            DateTime Now = DateTime.Now;

            var currentVacations = vacations.Where(item => item.StartDate.AddDays(1) >= Now && item.UserProfileId.Equals(userId));

            foreach (var vac in currentVacations)
            {
                if (vac.StartDate.Date == Now.Date || vac.StartDate.AddDays((int)Common.Models.Domain.Enum.NotificationType.StartVacation).Date == Now)
                {
                    var notification = _mapper.Map<Notification>(vac);
                    notification.NotificationType = Common.Models.Domain.Enum.NotificationType.StartVacation;

                    _vacationRepository.AddNotification(notification);
                }

                if (vac.StartDate.AddDays(-(int)Common.Models.Domain.Enum.NotificationType.StartingVerySoon) <= Now && vac.StartDate > Now)
                {
                    var notification = _mapper.Map<Notification>(vac);
                    notification.NotificationType = Common.Models.Domain.Enum.NotificationType.StartingVerySoon;

                    _vacationRepository.AddNotification(notification);
                }

                if (vac.StartDate.AddDays(-(int)Common.Models.Domain.Enum.NotificationType.StartingSoon) <= Now && vac.StartDate.AddDays(-3) >= Now)
                {
                    var notification = _mapper.Map<Notification>(vac);
                    notification.NotificationType = Common.Models.Domain.Enum.NotificationType.StartingSoon;

                    _vacationRepository.AddNotification(notification);
                }
            }
        }

        private void AddOrder(List<Vacation> vacations)
        {
            var orders = _mapper.Map<List<VacationOrder>>(vacations);

            foreach (var i in orders)
            {
                _vacationRepository.AddOrder(i);
            }
        }
    }
}
