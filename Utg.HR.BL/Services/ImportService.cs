using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientModel.Import;
using Utg.HR.Common.Models.Domain;
using Utg.HR.Common.Repositories;
using Utg.HR.Common.Services;

namespace Utg.HR.BL.Services
{
    public class ImportService :IImportService
    {
        private readonly IVacationRepository _vacationRepository;
		private readonly IBalanceVacationRepository _balanceRepository;
		private readonly IDataService _dataService;
        private readonly IMapper _mapper;
		private readonly ILogger<ImportService> _logger;
		private readonly IFactVacationRepository _factVacationRepository;
		public ImportService(IVacationRepository vacationRepository,
			IBalanceVacationRepository balanceRepository,
			IDataService dataService,
			IMapper mapper,
			ILogger<ImportService>  logger,
			IFactVacationRepository factVacationRepository)
		{
			_vacationRepository = vacationRepository;
			_mapper = mapper;
			_balanceRepository = balanceRepository;
			_dataService = dataService;
			_logger = logger;
			_factVacationRepository = factVacationRepository;
		}

		public async Task<IEnumerable<string>> ImportVacation(IEnumerable<ExternalVacationImportModel> historyItems)
		{
			var userprofileIds = historyItems.Select(hItem => hItem.UserProfileOuterId).Distinct();
			var userProfiles = await _dataService.GetUserProfiles(userprofileIds);

			var list = new List<string>();
			var userProfilesToAdd = new List<int>();

			foreach (var vacationItem in historyItems)
			{
				try
				{
					var userProfile = userProfiles.First(userP => userP.OuterId == vacationItem.UserProfileOuterId);
				
					var vacation = _mapper.Map<Vacation>(vacationItem);
				

					vacation.UserProfileId = userProfile.Id;
					vacation.CompanyId = userProfile.CompanyId;
					vacation.VacationType = Common.Models.Domain.Enum.VacationType.Planed;
					vacation.IsPayment = true;
					if (vacationItem.ShiftedDays != 0 )
						_vacationRepository.UpdateWithShiftedDays(vacation, vacationItem);
					else
						_vacationRepository.Import(vacation);

					userProfilesToAdd.Add(userProfile.Id);
				}
				catch (Exception ex)
				{
					list.Add($"{JsonConvert.SerializeObject(vacationItem)} {ex}");
				}
			}
			return list;
		}

		public async Task<IEnumerable<string>> ImportBalance(IEnumerable<ExternalBalanceVacationImportModel> balanceItems)
		{
			var list = new List<string>();
			try
			{
				
				var userprofileIds = balanceItems.Select(hItem => hItem.UserProfileOuterId).Distinct();
				var userProfiles = await _dataService.GetUserProfiles(userprofileIds);
				_logger.LogDebug($" получили userProfiles.Count ={ userprofileIds?.Count()}");
				
				var userProfilesToAdd = new List<int>();
				foreach (var balanceItem in balanceItems)
				{
					try
					{
						var userProfile = userProfiles.First(userP => userP.OuterId == balanceItem.UserProfileOuterId);

						//_logger.LogDebug($" получили userProfile ={ userProfile.Id} fio = {userProfile.FIO}");
						var balance = _mapper.Map<BalanceVacation>(balanceItem);
						balance.UserProfileId = userProfile.Id;					
						_balanceRepository.Import(balance);	
						userProfilesToAdd.Add(userProfile.Id);
					}
					catch (Exception ex)
					{
						_logger.LogError($"{JsonConvert.SerializeObject(balanceItem)} {ex}");
						list.Add($"{JsonConvert.SerializeObject(balanceItem)} {ex}");
					}
				}
			}
			catch(Exception ex)
            {
				_logger.LogError(ex.ToString());
				throw ex;
			}
			return list;
		}

        public async Task<IEnumerable<string>>  ImportFactVacation(IEnumerable<ExternalFactVacationImportModel> vacationItems)
        {
			var userprofileIds = vacationItems.Select(hItem => hItem.UserProfileOuterId).Distinct();
			var userProfiles = await _dataService.GetUserProfiles(userprofileIds);

			_logger.LogInformation($" получили userProfiles.Count ={ userprofileIds?.Count()}");
			var list = new List<string>();		

			foreach (var vacationItem in vacationItems)
			{
				try
				{
					var userProfile = userProfiles.FirstOrDefault(userP => userP.OuterId == vacationItem.UserProfileOuterId) 
						?? throw new Exception($"Не удалось найти пользователя с OuterId= {vacationItem.UserProfileOuterId}");
					
					var vacation = _mapper.Map<FactVacation>(vacationItem);
					vacation.UserProfileId = userProfile.Id;
					 _factVacationRepository.Import(vacation);
				}
				catch (Exception ex)
				{
					_logger.LogError($"{JsonConvert.SerializeObject(vacationItem)} {ex}");
					list.Add($"{JsonConvert.SerializeObject(vacationItem)} {ex}");
				}
			}
			return list;
		}
    }
}
