using System;
using Newtonsoft.Json;

namespace CoreySutton.TogglTools.Core
{
    public class Workspace
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("profile")]
        public int Profile { get; set; }

        [JsonProperty("premium")]
        public bool Premium { get; set; }

        [JsonProperty("admin")]
        public bool Admin { get; set; }

        [JsonProperty("default_hourly_rate")]
        public int DefaultHourlyRate { get; set; }

        [JsonProperty("default_currency")]
        public string DefaultCurrency { get; set; }

        [JsonProperty("only_admins_may_create_projects")]
        public bool OnlyAdminsMayCreateProjects { get; set; }

        [JsonProperty("only_admins_see_billable_rates")]
        public bool OnlyAdminsSeeBillableRates { get; set; }

        [JsonProperty("only_admins_see_team_dashboard")]
        public bool OnlyAdminsSeeTeamDashboard { get; set; }

        [JsonProperty("projects_billable_by_default")]
        public bool ProjectsBillableByDefault { get; set; }

        [JsonProperty("rounding")]
        public int Rounding { get; set; }

        [JsonProperty("rounding_minutes")]
        public int RoundingMinutes { get; set; }

        [JsonProperty("api_token")]
        public string ApiToken { get; set; }

        [JsonProperty("at")]
        public DateTime At { get; set; }

        [JsonProperty("ical_enabled")]
        public bool ICalEnabled { get; set; }
    }
}