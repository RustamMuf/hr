using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.Common.Models.Domain
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public int VacationId { get; set; }
        [ForeignKey(nameof(VacationId))]
        public virtual Vacation Vacation { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool Readed { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ReadedDate { get; set; }
    }
}
