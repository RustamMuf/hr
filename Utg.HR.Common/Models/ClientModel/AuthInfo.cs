using System.Collections.Generic;

namespace Utg.HR.Common.Models.ClientModel
{
	public class AuthInfo
	{
		public int UserId { get; set; }
		public int UserProfileId { get; set; }
		public string AuthToken { get; set; }
		public IEnumerable<int> Roles { get; set; }
	}
}
