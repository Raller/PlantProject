using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PlantApp.Model
{
    class AirHumidity
    {
        [JsonProperty("_id")]
        public string HumId { get; set; }

        [JsonProperty("humidity")]
        public string humidity { get; set; }

        [JsonProperty("plantId")]
        public string plantId { get; set; }

        [JsonProperty("date")]
        public DateTime date { get; set; }


    }
}
