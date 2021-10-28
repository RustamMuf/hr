using Newtonsoft.Json;
using System;

namespace Utg.HR.Common.Models.ClientModel.Import
{
    public class ExternalBalanceVacationImportModel
    {
        [JsonProperty("userid")]
        public Guid UserProfileOuterId { get; set; }

        [JsonProperty("personid")]
        public Guid UserOuterId { get; set; }
        [JsonProperty("balance")]
        public decimal BalanceOfVacation { get; set; }

    }
}
