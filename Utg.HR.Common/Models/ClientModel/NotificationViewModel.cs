using System;
using System.Collections.Generic;
using System.Text;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.Common.Models.ClientModel
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public int VacationId { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public bool Readed { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ReadedDate { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string TabN { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPatronymic { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
