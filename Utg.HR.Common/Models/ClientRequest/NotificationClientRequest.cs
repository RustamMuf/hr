using System;
using Utg.HR.Common.Helpers;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.Common.Models.ClientRequest
{
    public class NotificationClientRequest
    {
        public int Take { get; set; } = Const.PageSize;
        public int Skip { get; set; } = 0;
        public int UserId {get;set;}
        public string Search { get; set; }
        public DateTime? FirstDate { get; set; }
        public DateTime? SecondDate { get; set; }
        public string Readed { get; set; }
        public Role? Role { get; set; }
        public int? UserIdSearch { get; set; }
        public string NotificationTypeFilter { get; set; }
    }
}
