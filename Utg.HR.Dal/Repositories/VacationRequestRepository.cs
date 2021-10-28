using System.Linq;
using System.Threading.Tasks;
using Utg.HR.Common.Models.Domain;
using Utg.HR.Common.Repositories;
using Utg.HR.Dal.SqlContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Utg.HR.Common.Models.ClientRequest;
using AutoMapper;
using Utg.HR.Common.Models.ClientModel;
using System;
using Microsoft.Extensions.Logging;

namespace Utg.HR.Dal.Repositories
{
    public class VacationRequestRepository : IVacationRequestRepository
    {
        private readonly UtgContext _dbContext;
        private readonly IMapper _mapper;
        ILogger<VacationRequestRepository> _logger;
        public VacationRequestRepository(UtgContext dbContext, IMapper mapper, ILogger<VacationRequestRepository> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public void Add(VacationRequest model)
        {
            var vacation = _dbContext.Vacations.FirstOrDefault(item=>item.StartDate>=model.StartDate);

            _dbContext.VacationRequests.Add(model);

            var changeHistory = new VacationRequestHistoryChange();
            changeHistory.Comment = model.Comment;
            changeHistory.ChangeDate = DateTime.Now;
            changeHistory.RequestId = model.Id;
            changeHistory.State = model.VacationRequestState;
            changeHistory.VacationRequest = model;
            changeHistory.AnswerComment = model.AnswerComment;
            _dbContext.VacationRequestHistoryChanges.Add(changeHistory);


            _dbContext.SaveChanges();
        }

        public ICollection<VacationRequest> GetAllRequests(VacationRequestClientRequest clientRequest)
        {
            var raw =  _dbContext.VacationRequests.OrderBy(item => item.Id).ToList();

            if (clientRequest.UserIdSearch != null)
            {
                raw = raw.Where(item => item.UserProfileId.Equals(clientRequest.UserIdSearch)).ToList();
            }

            if (clientRequest.RequestType == 1)
            {
                raw = raw.Where(item => item.RequestType.Equals(Common.Models.Domain.Enum.RequestType.UnPlaned)).ToList();
            }
            if (clientRequest.RequestType == 2)
            {
                raw = raw.Where(item => item.ChangeVacationId.HasValue).ToList();
            }

            if (clientRequest.FirstDate != null && clientRequest.SecondDate != null)
            {
                raw = raw.Where(i => i.StartDate >= clientRequest.FirstDate && i.EndDate <= clientRequest.SecondDate).ToList();
            }

            if (clientRequest.FilterStatus != null)
            {
                raw = raw.AsEnumerable().Where(item => item.VacationRequestState.ToString().Equals(clientRequest.FilterStatus.ToString())).ToList();
            }

          


            return raw;
        }

        public VacationRequest ChangeRequestState(VacationRequestClientRequest clientRequest)
        {

            try
            {
                _logger.LogDebug("Запустили", "[Vacation][ChangeRequestState]");
                var context = _dbContext;

                var entity = context.VacationRequests.ToList();
                var request = entity.FirstOrDefault(item => item.Id.Equals(clientRequest.RequestId));

                if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRUser)
                {
                    if (clientRequest.State == true && clientRequest.RequestState == 1)
                    {
                        request.VacationRequestState = Common.Models.Domain.Enum.VacationRequestState.NeedLeadApprove;
                    }
                    else if (clientRequest.State == false)
                    {
                        request.VacationRequestState = Common.Models.Domain.Enum.VacationRequestState.Rejected;
                    }
                }

                if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRHead)
                {
                    if (clientRequest.State == true && clientRequest.RequestState == 2)
                    {
                        request.VacationRequestState = Common.Models.Domain.Enum.VacationRequestState.NeedPersonalService;
                    }
                    else if (clientRequest.State == false)
                    {
                        request.VacationRequestState = Common.Models.Domain.Enum.VacationRequestState.LeedRejected;
                    }
                }

                if (clientRequest.Role == Common.Models.Domain.Enum.Role.HRPersonal)
                {
                    if (clientRequest.State == true && clientRequest.RequestState == 4)
                    {
                        request.VacationRequestState = Common.Models.Domain.Enum.VacationRequestState.ApprovedPersonalService;
                    }
                    else if (clientRequest.State == false)
                    {
                        request.VacationRequestState = Common.Models.Domain.Enum.VacationRequestState.PersonalServiceRejected;
                    }
                }

                if (request.VacationRequestState == Common.Models.Domain.Enum.VacationRequestState.ApprovedPersonalService)
                {
                    AddVacation(request);
                    var vacation = _mapper.Map<Vacation>(request);
                    context.VacationOrders.Add(new VacationOrder()
                    {

                        CreatedDate = DateTime.Now,
                        IsPayment = request.IsPayment,
                        Vacation = vacation,
                        VacationId = vacation.Id,
                    });
                }

                if (request == null) return null;

                _logger.LogDebug("Сохранение комента " + clientRequest.AnswerComment ?? "", "[Vacation][ChangeRequestState]");
                request.AnswerComment = clientRequest.AnswerComment;
                _logger.LogDebug("Сохранениил комент request.AnswerComment=  " + request.AnswerComment ?? "", "[Vacation][ChangeRequestState]");
                context.VacationRequests.Update(request);

                var changeHistory = _mapper.Map<VacationRequestHistoryChange>(request);
                changeHistory.VacationRequest = request;
                _dbContext.VacationRequestHistoryChanges.Add(changeHistory);

                context.SaveChanges();

                return request;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "[Vacation][ChangeRequestState]");
                throw ex;
            }
        }

        public VacationRequest Delete(int id)
        {

            using var context = _dbContext;

            var entity = context.VacationRequests.FirstOrDefault(item => item.Id.Equals(id));

            //if (entity == null) return;

            context.VacationRequests.Remove(entity);

            context.SaveChanges();

            return entity;
        }

        private void AddVacation(VacationRequest request)
        {

            var vacation = _mapper.Map<Vacation>(request);

            if (request.ChangeVacationId != null)
            {
                var context = _dbContext;

                var entity = context.Vacations.FirstOrDefault(item => item.Id.Equals(request.ChangeVacationId));

                if (entity == null) return;

                entity.EndDate = request.EndDate;
                entity.StartDate = request.StartDate;

                context.Vacations.Update(entity);
            }
            else
            {
                vacation.CreatedDate = DateTime.Now;
                _dbContext.Vacations.Add(vacation);
            }




            _dbContext.SaveChanges();
        }

        public void ChangeRequest(VacationRequestViewModel model)
        {
            var context = _dbContext;

            var entity = context.VacationRequests.FirstOrDefault(item => item.Id.Equals(model.Id));

            if (entity == null) return;

            entity.Comment = model.Comment;
            entity.EndDate = model.EndDate;
            entity.StartDate = model.StartDate;
            entity.VacationRequestState = model.VacationRequestState;
            entity.VacationType = model.VacationType;
            entity.RequestType = model.RequestType;

            context.VacationRequests.Update(entity);

            var changeHistory = new VacationRequestHistoryChange();
            changeHistory.Comment = model.Comment;
            changeHistory.ChangeDate = DateTime.Now;
            changeHistory.RequestId = model.Id;
            changeHistory.State = model.VacationRequestState;
            changeHistory.VacationRequest = entity;
            _dbContext.VacationRequestHistoryChanges.Add(changeHistory);

            context.SaveChanges();
        }

        public VacationRequest GetById(int id)
        {
            var context = _dbContext;

            var entity = context.VacationRequests.FirstOrDefault(item => item.Id.Equals(id));

            return entity;
        }

        public IQueryable<Vacation> ValidateRequest(DateTime thisYear, int userId)
        {
            var raw = _dbContext.Vacations.Where(item => ((item.StartDate >= thisYear)&&(item.UserProfileId.Equals(userId))));

            return raw;
        }
    }
}
