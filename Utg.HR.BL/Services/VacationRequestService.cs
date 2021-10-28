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
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.BL.Services
{
    public class VacationRequestService : IVacationRequestService
    {
        private readonly IVacationRequestRepository _vacationRequestRepository;
        private readonly IVacationRepository _vacationRepository;
        private readonly IMapper _mapper;
        private readonly IDataService _dataService;

        public VacationRequestService(IVacationRequestRepository vacationRequestRepository,
            IDataService dataService,
            IMapper mapper,
            IVacationRepository vacationRepository)
        {
            _vacationRequestRepository = vacationRequestRepository;
            _dataService = dataService;
            _mapper = mapper;
            _vacationRepository = vacationRepository;
        }

        public void Add(VacationRequestViewModel model)
        {
            var entity = _mapper.Map<VacationRequest>(model);

            _vacationRequestRepository.Add(entity);

        }
        public async Task<IEnumerable<VacationRequestViewModel>> GetVacationsByUserSurname(string surname, string auth)
        {
            var vacations = _vacationRequestRepository.GetAllRequests(new VacationRequestClientRequest { });

            var userIds = vacations.Select(x => x.UserProfileId).Distinct();

            var userProfiles = await _dataService.GetUserProfiles(userIds, auth);

            var vacationWithUsers = vacations.Join(
                userProfiles,
                vacation => vacation.UserProfileId,
                user => user.Id,
                (vacation, user) => new VacationRequestViewModel
                {
                    Id = vacation.Id,
                    UserSurname = user.Surname,
                    Days = vacation.Days,
                    UserName = user.Name,
                    CreatedDate = vacation.CreatedDate,
                    EndDate = vacation.EndDate,
                    StartDate = vacation.StartDate,
                    CompanyId = vacation.CompanyId,
                    TabN = user.TabN,
                    UserProfileId = user.UserId,
                    VacationType = vacation.VacationType,
                    VacationRequestState = vacation.VacationRequestState,
                    ChangeDate = vacation.ChangeDate,
                    ChangeVacationId = vacation.ChangeVacationId,
                    AnswerComment = vacation.AnswerComment,
                    ChiefFullName = user.ChiefFullName,
                    ChiefId = user.ChiefId,
                    Comment = vacation.Comment,
                    RequestType = vacation.RequestType
                }
                );
            var result = vacationWithUsers.Where(x => x.UserSurname.Contains(surname));
            return result;
        }
        public async Task<PagedResult<VacationRequestViewModel>> GetAllAsync(VacationRequestClientRequest clientRequest, string auth)
        {




            var query = _vacationRequestRepository.GetAllRequests(clientRequest);

            if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRUser)
            {
                query = query.Where(item => item.UserProfileId.Equals(clientRequest.UserId)).ToList();
            }

            if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRHead)
            {
                query = query.OrderBy(item => item.Id).ToList();
            }

            if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRPersonal)
            {
                query = query.Where(item => item.VacationRequestState != Common.Models.Domain.Enum.VacationRequestState.Draft).OrderBy(item => item.Id).ToList();
            }

            if (clientRequest.VacationRequestStates != null && clientRequest.VacationRequestStates.Length != 0)
            {
                query = query.Where(item => item != null ? clientRequest.VacationRequestStates.Contains(item.VacationRequestState) : false).ToList();
            }

            if (clientRequest.IsRequestClosed)
            {
                VacationRequestState[] vacationRequestStates = new VacationRequestState[] { VacationRequestState.LeedRejected, VacationRequestState.PersonalServiceRejected, VacationRequestState.ApprovedPersonalService };
                query = query.Where(item => item?.VacationRequestState != null && !vacationRequestStates.Contains(item.VacationRequestState)).ToList();
            }

            var userIds = query.Select(vacationReq => vacationReq.UserProfileId).Distinct();

            var userProfiles = await _dataService.GetUserProfiles(userIds, auth);

            var total = query.Count();
            var model = _mapper.Map<List<VacationRequestViewModel>>(query);

            model.ForEach(item =>
            {
                var userProfile = userProfiles.FirstOrDefault(userP => userP.Id == item.UserProfileId);
                item.UserName = userProfile?.Name;
                item.UserPatronymic = userProfile?.Patronymic;
                item.UserSurname = userProfile?.Surname;
                item.ChiefId = userProfile?.ChiefId != null ? userProfile?.ChiefId : 0;
                item.ChiefFullName = userProfile?.ChiefFullName != null ? userProfile?.ChiefFullName : "Empty";
                item.TabN = userProfile?.TabN;
            });

            if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRHead)
            {
                model = model.Where(item => item.ChiefId.Equals(clientRequest.UserId) || item.UserProfileId.Equals(clientRequest.UserId)).ToList();
                total = model.Count();
            }

            if (!string.IsNullOrWhiteSpace(clientRequest.Search))
            {
                var clon = model.Where(item => item?.UserSurname!= null&& item.UserSurname.Contains(clientRequest.Search, StringComparison.OrdinalIgnoreCase)).ToList();
                total = clon.Count();
                model = clon;
            }

            var result = model.OrderBy(item => item.UserSurname).Skip(clientRequest.Skip).Take(clientRequest.Take);


            return new PagedResult<VacationRequestViewModel>
            {

                Total = total,
                Result = result
            };
        }

        public PagedResult<VacationRequestViewModel> GetAllTest(VacationRequestClientRequest clientRequest, string auth)
        {
            var query = _vacationRequestRepository.GetAllRequests(clientRequest);

            var total = query.Count();
            var model = _mapper.Map<List<VacationRequestViewModel>>(query);

            var result = model.Skip(clientRequest.Skip).Take(clientRequest.Take);

            return new PagedResult<VacationRequestViewModel>
            {

                Total = total,
                Result = result
            };
        }


        //соглосование заявки 
        public async Task<VacationRequest> ChangeRequestState(VacationRequestClientRequest clientRequest, string auth)
        {
            try
            {
                if (clientRequest.RequestId == 999)
                {
                    var currentRequest = _vacationRequestRepository.GetById(clientRequest.RequestId.Value);

                    if (clientRequest.Search == "1")
                    {
                        int userProfileId = currentRequest.UserProfileId;
                        var userIds = new int[] { userProfileId, clientRequest.UserId.Value }.AsEnumerable();
                        var userProfiles = await _dataService.GetUserProfiles(userIds, auth);
                        var first = userProfiles.First();
                        var second = userProfiles.FirstOrDefault(item => item.UserId.Equals(clientRequest.UserId));
                        if (clientRequest.UserId == clientRequest.UserId)
                        {
                            return new VacationRequest();
                        }
                    }
                }

                var model = _vacationRequestRepository.ChangeRequestState(clientRequest);
                if (model.VacationRequestState == Common.Models.Domain.Enum.VacationRequestState.ApprovedPersonalService)
                {
                    _vacationRepository.AddOrder(
                                    new VacationOrder
                                    {
                                        CreatedDate = model.CreatedDate,
                                        IsPayment = model.IsPayment,
                                        VacationId = model.Id
                                    }
                                    );
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VacationRequest Delete(int id)
        {
            var model = _vacationRequestRepository.Delete(id);

            return model;
        }

        public void ChangeRequest(VacationRequestViewModel model)
        {
            _vacationRequestRepository.ChangeRequest(model);
        }

        public VacationRequestViewModel GetById(int id)
        {
            var entity = _vacationRequestRepository.GetById(id);

            var model = _mapper.Map<VacationRequestViewModel>(entity);

            return model;
        }
    }


}
