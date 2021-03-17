using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PlantApp.Model
{
    class SoilHumidity
    {
        [JsonProperty("_id")]
        public string soilId { get; set; }
        [JsonProperty("humidity")]
        public string humidity { get; set; }

        [JsonProperty("plantId")]
        public string plantId { get; set; }

        [JsonProperty("date")]
        public DateTime date { get; set; }

    }
}
