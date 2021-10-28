using System;

namespace Utg.HR.Common.Models.ClientModel
{
	public class PositionsApiModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public Guid OuterId { get; set; }

		public int? DepartmentId { get; set; }
		public string DepartmentName { get; set; }

		public int? CompanyId { get; set; }
		public string CompanyName { get; set; }
		public decimal Rate { get; set; }

	}
}
