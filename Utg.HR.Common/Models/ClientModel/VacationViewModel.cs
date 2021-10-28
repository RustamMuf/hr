using System;
using System.Collections.Generic;
using System.Text;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.Common.Models.ClientModel
{
    public class VacationViewModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserProfileId { get; set; }
        public DateTime CreatedDate { get; set; }
        public VacationType VacationType { get; set; }
        public int? CompanyId { get; set; }
        public string TabN { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPatronymic { get; set; }
        public int? Days { get; set; }
        public bool IsPayment { get; set; }

        public string ChiefFullName { get; set; }
        public bool IsFactVaction { get; set; }
    }
}
