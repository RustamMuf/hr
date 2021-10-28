using System;
using System.ComponentModel.DataAnnotations;
using Utg.HR.Common.Models.Domain.Enum;

namespace Utg.HR.Common.Models.Domain
{
    public class Vacation
    {
        [Key]       
        public int Id { get; set; }
        public bool IsPayment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserProfileId { get; set; }
        public DateTime CreatedDate { get; set; }
        public VacationType VacationType { get; set; }
        public int? CompanyId { get; set; }
        public int? Days { get; set; }
        
    }
}
