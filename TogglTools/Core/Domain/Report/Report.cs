using System;
using System.Collections.Generic;
using System.Data;
using CoreySutton.Utilities;
using Newtonsoft.Json;

namespace CoreySutton.TogglTools.Core
{
    public partial class Report
    {
        #region Instance Members

        [JsonProperty("total_grand")]
        public int? TotalGrand { get; set; }

        [JsonProperty("total_billable")]
        public object TotalBillable { get; set; }

        [JsonProperty("total_currencies")]
        public List<TotalCurrency> TotalCurrencies { get; set; }

        [JsonProperty("data")]
        public List<Data> Datas { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        #endregion

        public static Report ProcessJsonResponse(string response)
        {
            Argument.IsNotNull(response);

            var report = JsonConvert.DeserializeObject<Report>(response);
            if (report == null) throw new NoNullAllowedException("Could not deserialize response");

            return report;
        }
    }

    public class Item
    {
        [JsonProperty("title")]
        public Title2 Title { get; set; }

        [JsonProperty("time")]
        public int Time { get; set; }

        [JsonProperty("cur")]
        public object Cur { get; set; }

        [JsonProperty("sum")]
        public object Sum { get; set; }

        [JsonProperty("rate")]
        public object Rate { get; set; }
    }

    public class Data
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("pid")]
        public long? Pid { get; set; }

        [JsonProperty("title")]
        public Title Title { get; set; }

        [JsonProperty("time")]
        public int Time { get; set; }

        [JsonProperty("total_currencies")]
        public List<TotalCurrency> TotalCurrencies { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("tid")]
        public object Tid { get; set; }

        [JsonProperty("uid")]
        public int Uid { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        public DateTime End { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("dur")]
        public int Dur { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("use_stop")]
        public bool UseStop { get; set; }

        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("project_color")]
        public string ProjectColor { get; set; }

        [JsonProperty("project_hex_color")]
        public string ProjectHexColor { get; set; }

        [JsonProperty("task")]
        public object Task { get; set; }

        [JsonProperty("billable")]
        public object Billable { get; set; }

        [JsonProperty("is_billable")]
        public bool IsBillable { get; set; }

        [JsonProperty("cur")]
        public object Cur { get; set; }

        [JsonProperty("tags")]
        public List<object> Tags { get; set; }
    }

    public class Title
    {
        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("hexcolor")]
        public string HexColor { get; set; }
    }

    public class Title2
    {
        [JsonProperty("time_entry")]
        public string TimeEntry { get; set; }
    }

    public class TotalCurrency
    {
        [JsonProperty("currenct")]
        public object Currency { get; set; }

        [JsonProperty("amount")]
        public object Amount { get; set; }
    }
}