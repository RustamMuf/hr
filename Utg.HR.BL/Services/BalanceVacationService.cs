using AutoMapper;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Repositories;
using Utg.HR.Common.Services;

namespace Utg.HR.BL.Services
{
    public class BalanceVacationService : IBalanceVacationService
    {
        private readonly IBalanceVacationRepository _balanceVacationRepository;
        private readonly IMapper _mapper;
        private readonly IDataService _dataService;

        public BalanceVacationService(IBalanceVacationRepository balanceVacationRepository, IDataService dataService,
       IMapper mapper)
        {
            _balanceVacationRepository = balanceVacationRepository;
            _dataService = dataService;
            _mapper = mapper;
        }

        public async Task<BalanceVacationViewModel> GetBalanceById(int id)
        {
            var balance = await _balanceVacationRepository.GetBalanceById(id);

            var model = _mapper.Map<BalanceVacationViewModel>(balance);

            return model;
        }
    }
}
