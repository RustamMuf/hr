using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utg.HR.Common.Models.ClientModel.Import
{
   public class ExternalFactVacationImportModel
    {

        [JsonProperty("userid")]
        public Guid UserProfileOuterId { get; set; }

        [JsonProperty("statrdate")]
        public string ScheduleStart { get; set; }

        [JsonProperty("enddate")]
        public string ScheduleEnd { get; set; }

    }
}
