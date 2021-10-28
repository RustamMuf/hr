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
    public class VacationOrderService : IVacationOrderService
    {
        private readonly IVacationOrderRepository _vacationOrderRepository;
        private readonly IMapper _mapper;
        private readonly IDataService _dataService;

        public VacationOrderService(IVacationOrderRepository vacationOrderRepository, IDataService dataService,
       IMapper mapper)
        {
            _vacationOrderRepository = vacationOrderRepository;
            _dataService = dataService;
            _mapper = mapper;
        }
        public async Task<PagedResult<VacationOrderViewModel>> GetAllAsync(VacationOrderClientRequest clientRequest, string auth)
        {
            var query = await _vacationOrderRepository.GetAllAsync(clientRequest);

            if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRUser)
            {
                query = query.Where(item => item.Vacation.UserProfileId.Equals(clientRequest.UserId)).ToList();
            }

            var userIds = query.Select(vacationReq => vacationReq.Vacation.UserProfileId).Distinct();

            var userProfiles = await _dataService.GetUserProfiles(userIds, auth);

            var total = query.Count();
            var model = _mapper.Map<List<VacationOrderViewModel>>(query);

            model.ForEach(item =>
            {
                var userProfile = userProfiles.FirstOrDefault(userP => userP.Id == item.UserProfileId);
                item.UserName = userProfile?.Name;
                item.UserPatronymic = userProfile?.Patronymic;
                item.UserSurname = userProfile?.Surname;
                item.TabN = userProfile?.TabN;
                item.CompanyName = userProfile?.CompanyName;
                //item.DepartmentId = userProfile?.DepartmentId;
                item.DepartmentName = userProfile?.DepartmentName;
                //item.PositionId = userProfile?.PositionId;
                item.PositionName = userProfile?.PositionName;
            });

            if (!string.IsNullOrWhiteSpace(clientRequest.Search))
            {
                var clon = model.Where(item => item.UserSurname.Contains(clientRequest.Search, StringComparison.OrdinalIgnoreCase));
                total = clon.Count();
                model = clon.ToList();
            }

            var result = model.Skip(clientRequest.Skip).Take(clientRequest.Take);

            return new PagedResult<VacationOrderViewModel>
            {

                Total = total,
                Result = result
            };
        }

        public VacationOrder Delete(int id)
        {
            var model = _vacationOrderRepository.Delete(id);

            return model;
        }

        public VacationOrder ChangeState(VacationOrderClientRequest clientRequest)
        {

            var model = _vacationOrderRepository.ChangeState(clientRequest);

            return model;
        }
    }
}
