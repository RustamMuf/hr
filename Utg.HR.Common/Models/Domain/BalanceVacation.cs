using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Utg.HR.Common.Models.Domain
{
    public class BalanceVacation
    {
        [Key]
        public int BalanceId { get; set; }
        public int UserProfileId { get; set; }
        public decimal BalanceOfVacation { get; set; }
    }
}
