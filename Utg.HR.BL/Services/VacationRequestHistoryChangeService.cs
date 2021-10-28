using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Models.ClientRequest;
using Utg.HR.Common.Repositories;
using Utg.HR.Common.Services;

namespace Utg.HR.BL.Services
{
    public class VacationRequestHistoryChangeService : IVacationRequestHistoryChangeService
    {
        private readonly IVacationRequestHistoryChangeRepository _vacationRequestHistoryChangeRepository;
        private readonly IMapper _mapper;

        public VacationRequestHistoryChangeService(IVacationRequestHistoryChangeRepository vacationRequestHistoryChangeRepository,
           IMapper mapper)
        {
            _vacationRequestHistoryChangeRepository = vacationRequestHistoryChangeRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<VacationRequestHistoryChangeViewModel>> GetAllStatuses(VacationRequestHistoryChangeClientRequest clientRequest)
        {
            var query = await _vacationRequestHistoryChangeRepository.GetAllStatuses(clientRequest.RequestId);

            var total = query.Count();

            var model = _mapper.Map<List<VacationRequestHistoryChangeViewModel>>(query);

            return new PagedResult<VacationRequestHistoryChangeViewModel>
            {

                Total = total,
                Result = model
            };
        }
    }
}
