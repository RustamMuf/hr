using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Repositories;
using Utg.HR.Common.Services;

namespace Utg.HR.BL.Services
{
	class HrRequestService : IHrRequestService
	{
		private IConfiguration _configuration;
		private readonly IHrRequestRepository _hrRequestRepository;
		private readonly IMapper _mapper;

		public HrRequestService(IConfiguration configuration, IMapper mapper, IHrRequestRepository hrRequestRepository)
		{
			_configuration = configuration;
			_hrRequestRepository = hrRequestRepository;
			_mapper = mapper;
		}

		public IEnumerable<HrRequestModel> Get()
		{
			var rawList = _hrRequestRepository.Get();
			var list = _mapper.Map<IEnumerable<HrRequestModel>>(rawList);
			return list;
		}
	}
}
