using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models;
using Utg.HR.Common.Models.ClientModel;

namespace Utg.HR.Common.Services
{
    public interface IDataService
    {
		Task<IEnumerable<UserProfileApiModel>> GetUserProfiles(IEnumerable<Guid> userProfileOuterIds, string auth = "");

		Task<IEnumerable<UserProfileApiModel>> GetUserProfiles(IEnumerable<int> userprofileIds, string auth = "");

		Task<IEnumerable<string>> AddToAcceasProfile(List<int> userProfilesToAdd);
		Task<PagedResult<UserProfileApiModel>> GetAddressBook(AdressbookRequest clientRequest, string auth = "");
		Task<PositionsApiModel> GetPosition(int id, string auth = "");
	}
}
