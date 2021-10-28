using System;
using Utg.HR.Common.Helpers;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.Common.Models.ClientRequest
{
    public class VacationRequestClientRequest
    {
        public int Take { get; set; } = Const.PageSize;
        public int Skip { get; set; } = 0;

        public int? UserId { get; set; }

        public string Search { get; set; }

        public int? CompanyId { get; set; }

        public Role? Role { get; set; }

        public int? RequestId { get; set; }

        public int? RequestState { get; set; }

        public bool State { get; set; }
        public string Comment { get; set; }
        public string AnswerComment { get; set; }
        public DateTime? FirstDate { get; set; }
        public DateTime? SecondDate { get; set; }
        public int? VacationType { get; set; }
        public int? RequestType { get; set; }
        public int? UserIdSearch { get; set; }
        public string FilterStatus { get; set; }


        public VacationRequestState[] VacationRequestStates { get; set; }

        public bool IsRequestClosed { get; set; }
    }
}
