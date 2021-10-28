using System;
using System.Collections.Generic;
using System.Text;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.Common.Models.ClientModel
{
    public class VacationOrderViewModel
    {
        public int Id { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public OrderPrinted OrderPrinted { get; set; }
        public OrderRecived OrderRecived { get; set; }
        public int VacationId { get; set; } 
        public string TabN { get; set; } 
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPatronymic { get; set; }
        public int UserProfileId { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public VacationType VacationType { get; set; }
        public bool IsPayment { get; set; }
    }
}
