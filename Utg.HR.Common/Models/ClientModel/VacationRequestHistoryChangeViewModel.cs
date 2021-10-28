using System;
using System.Collections.Generic;
using System.Text;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.Common.Models.ClientModel
{
    public class VacationRequestHistoryChangeViewModel
    {
        public int VacationRequestHistoryChangeId { get; set; }
        public VacationRequestState State { get; set; }
        public int RequestId { get; set; }
        public string Comment { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
