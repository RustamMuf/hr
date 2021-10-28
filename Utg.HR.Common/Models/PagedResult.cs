using System.Collections.Generic;

namespace Utg.HR.Common.Models
{
	public class PagedResult<T>
	{
		public int Total { get; set; }

		public IEnumerable<T> Result {get; set; }
	}
}
