using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utg.HR.Common.Models.ClientModel.Import
{
    public class ExternalVacationImportModel
    {
        [JsonProperty("userid")]
        public Guid UserProfileOuterId { get; set; }

        [JsonProperty("personid")]
        public Guid UserOuterId { get; set; }

        [JsonProperty("days")]
        public int Days { get; set; }

        [JsonProperty("statrdate")]
        public string ScheduleStart { get; set; }

        [JsonProperty("enddate")]
        public string ScheduleEnd { get; set; }

        [JsonProperty("shifteddays")]
        public int ShiftedDays { get; set; }

        [JsonProperty("shiftedstartdate")]
        public string ShiftedStart { get; set; }

        [JsonProperty("shiftedenddate")]
        public string ShiftedEnd { get; set; }
        
        public DateTime StartDate => DateTime.TryParse(ScheduleStart, out var date) ?
            date : DateTime.MinValue;

        public DateTime EndDate => DateTime.TryParse(ScheduleEnd, out var date) ?
            date : DateTime.MinValue;

        public DateTime ShiftedStartDate => DateTime.TryParse(ShiftedStart, out var date) ?
            date : DateTime.MinValue;

        public DateTime ShiftedEndDate => DateTime.TryParse(ShiftedEnd, out var date) ?
            date : DateTime.MinValue;
    }
}
