
namespace Utg.HR.Common.Models.ClientModel
{
	public class UserProfileViewModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }

		public string TabN { get; set; }
		public string CompanyId { get; set; }
		public string CompanyName { get; set; }
		public string DepartmentId { get; set; }
		public string DepartmentName { get; set; }

		public string Name { get; set; }
		public string Surname { get; set; }
		public string Patronymic { get; set; }

		public string FIO => $"{Surname} {Name} {Patronymic}";
	}
}
