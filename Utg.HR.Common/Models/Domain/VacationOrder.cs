using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.Common.Models.Domain
{
    public class VacationOrder
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public OrderPrinted OrderPrinted { get; set; }
        public OrderRecived OrderRecived { get; set; }
        public int VacationId { get; set; }
        public bool IsPayment { get; set; }

        [ForeignKey(nameof(VacationId))]
        public virtual Vacation Vacation { get; set; }
    }
}
