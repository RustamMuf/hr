using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.Common.Models.Domain
{
    public class VacationRequestHistoryChange
    {
        [Key]
        public int VacationRequestHistoryChangeId { get; set; }
        public VacationRequestState State { get; set; }
        public int RequestId { get; set; }
        [ForeignKey(nameof(RequestId))]
        public virtual VacationRequest VacationRequest { get; set; }
        public string Comment { get; set; }
        public string AnswerComment { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
