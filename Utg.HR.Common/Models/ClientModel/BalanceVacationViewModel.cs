using System;
using System.Collections.Generic;
using System.Text;

namespace Utg.HR.Common.Models.ClientModel
{
    public class BalanceVacationViewModel
    {
        public int BalanceId { get; set; }
        public int UserProfileId { get; set; }
        public decimal BalanceOfVacation { get; set; }
    }
}
