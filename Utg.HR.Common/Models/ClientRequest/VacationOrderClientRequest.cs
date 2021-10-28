using System;
using Utg.HR.Common.Helpers;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.Common.Models.ClientRequest
{
    public class VacationOrderClientRequest
    {
        public int Take { get; set; } = Const.PageSize;
        public int Skip { get; set; } = 0;
        public int? UserId { get; set; }
        public int? OrderPrinted { get; set; }
        public int? OrderRecived { get; set; }
        public int? OrderId { get; set; }

        public string Search { get; set; }

        public int? CompanyId { get; set; }
        public Role? Role { get; set; }
        public DateTime? FirstDate { get; set; }
        public DateTime? SecondDate { get; set; }
        public string OrderPrintedFilter { get; set; }
        public string OrderRecivedFilter { get; set; }
    }
}
