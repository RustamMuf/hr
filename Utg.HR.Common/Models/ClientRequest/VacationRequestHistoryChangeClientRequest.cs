using Utg.HR.Common.Helpers;

namespace Utg.HR.Common.Models.ClientRequest
{
    public class VacationRequestHistoryChangeClientRequest
    {
        public int Take { get; set; } = Const.PageSize;
        public int Skip { get; set; } = 0;
        public int RequestId { get; set; }
    }
}
