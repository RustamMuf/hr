using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.Common.Models.Domain
{
    public class VacationRequest
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserProfileId { get; set; }
        public string Comment { get; set; }
        public string AnswerComment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ChangeDate { get; set; }
        public VacationRequestState VacationRequestState { get; set; }
        public bool IsPayment { get; set; }
        public VacationType VacationType { get; set; }
        //внеплпановый или который переносился 
        public RequestType RequestType { get; set; }
        public int? CompanyId { get; set; }
        public int? ChangeVacationId { get; set; }
        public int? Days { get; set; }
    }
}
