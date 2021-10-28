using System;
using System.Collections.Generic;
using System.Text;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.Common.Models.ClientModel
{
    public class VacationRequestViewModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserProfileId { get; set; }
        public string Comment { get; set; }
        public string AnswerComment { get; set; }
        public DateTime CreatedDate { get; set; }
        public VacationRequestState VacationRequestState { get; set; }
        public VacationType VacationType { get; set; }
        public RequestType RequestType { get; set; }
        public DateTime? ChangeDate { get; set; }
        public int? CompanyId { get; set; }
        public string TabN { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPatronymic { get; set; }
        public string ChiefFullName { get; set; }
        public int? ChiefId { get; set; }
        public int? ChangeVacationId { get; set; }
        public int? Days { get; set; }
    }
}