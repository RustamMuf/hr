using System;

namespace Utg.HR.Common.Models.ClientModel
{
	public class UserProfileApiModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string TabN { get; set; }

		public string Name { get; set; }
		public string Surname { get; set; }
		public string Patronymic { get; set; }
		public Guid OuterId { get; set; }

		public int? CompanyId { get; set; }
		public string CompanyName { get; set; }

		public int? DepartmentId { get; set; }
		public string DepartmentName { get; set; }
		public int? PositionId { get; set; }
		public string PositionName { get; set; }
		public string Status { get; set; }

		public DateTime? DismissalDate { get; set; }
		public string ChiefFullName { get; set; }
		public int? ChiefId { get; set; }

		public string FIO => $"{Surname} {Name} {Patronymic}";
	}
}
