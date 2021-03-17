using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PlantApp.Model
{
    public class SoilHumidity
    {
        [JsonProperty("_id")]
        public string SoilId { get; set; }
        [JsonProperty("humidity")]
        public string Humidity { get; set; }

        [JsonProperty("plantId")]
        public string PlantId { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

    }
}
