namespace Utg.HR.Common.Models.ClientModel
{
    public class AdressbookRequest
    {
        // public int? Skip { get; set; }

        //public int? Take { get; set; }

        public string Search { get; set; }

        public string SortBy { get; set; }

        public string SortDirection { get; set; }
        public int? CompanyId { get; set; }

        public int? DepartmentId { get; set; }

        public int? PositionId { get; set; }

    }
}
